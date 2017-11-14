using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Apollo.Data;
using Apollo.Domain.entities;
using Rotativa;
using Rotativa.Options;

namespace Apollo.ASP.Controllers
{
    public class transportJobsController : Controller
    {
        private JeeModel db = new JeeModel();

        // GET: transportJobs
        public ActionResult Index()
        {
            /* if (Session["token"] != null)
             {
                 return View(db.TransportJobs.ToList());
             }
             else
             {
                 return RedirectToAction("Index", "Home");

             }*/
             
            var Order = db.orders.AsEnumerable();

            var transOrder = db.TransportJobs.Include(u => u.orders.artwork);
            ViewBag.transid = transOrder.ToList();
            return View(transOrder.ToList());
        }

        // GET: transportJobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transportJob transportJob = db.TransportJobs.Find(id);
            if (transportJob == null)
            {
                return HttpNotFound();
            }
            return View(transportJob);
        }

        // GET: transportJobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: transportJobs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,DateDeDebut,DateDeDefin,title,description")] transportJob transportJob)
        {
            if (ModelState.IsValid)
            {
                db.TransportJobs.Add(transportJob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transportJob);

        }





        [HttpPost]
        public JsonResult AcceptJob(int? id)
        {
            int iduser = Convert.ToInt32(Session["user"].ToString());
            transportJob transportJob = db.TransportJobs.Find(id);
            transportJob.Status = "Started";
            transportJob.DateDeDebut = DateTime.Now;
            transportJob.transporterID = iduser;
            db.TransportJobs.Attach(transportJob);
            db.Entry(transportJob).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges


            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

            }

            return Json(transportJob);


        }

        [HttpPost]
        public JsonResult CompleteJob(int? id)
        {
            int iduser = Convert.ToInt32(Session["user"].ToString());
            transportJob transportJob = db.TransportJobs.Find(id);
            if (transportJob.transporterID == iduser)
            {
                transportJob.Status = "Completed";
                transportJob.DateDeDefin= DateTime.Now;

                db.TransportJobs.Attach(transportJob);
                db.Entry(transportJob).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        System.Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                }

            }
            return Json(transportJob);
        }


        
        public ActionResult HtmlToPdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Configuration.LazyLoadingEnabled = true;
            //transportJob transportJob = db.TransportJobs.Find(id);
            transportJob transportJob = db.TransportJobs.Include(u => u.orders.artwork).SingleOrDefault(x => x.id == id);
            if (transportJob == null)
            {
                return HttpNotFound();
            }
            db.Configuration.LazyLoadingEnabled = true;
            //transportJob transportJob = db.TransportJobs.Find(id);
           
            var yo = transportJob;
            /* var balance = (from a in db.TransportJobs
                            join c in db.orders on a.orders equals c.Id
                            where c.ClientID == yourDescriptionObject.ClientID
                            select a.Balance)
               .SingleOrDefault();*/
            MailMessage o = new MailMessage("ayed.maissen@gmail.com", "ayed.maissen@gmail.com");
            o.BodyEncoding = Encoding.UTF8;
            o.IsBodyHtml = true;
            o.Subject = "Sending Email Using Asp.Net & C#";
            var fileName = yo.title;
            var filePath = Path.Combine(Server.MapPath("/Temp"), fileName);

            var myPDF = new ViewAsPdf("InvoicePrint",yo)
            {
                FileName = fileName,
                PageSize = Size.A5,
                SaveOnServerPath = filePath
            };
            byte[] applicationPDFData = myPDF.BuildPdf(ControllerContext);
            MemoryStream stream = new MemoryStream(applicationPDFData);
            Attachment attachment = new Attachment(stream, "document.pdf");
            o.Attachments.Add(attachment);
            NetworkCredential netCred = new NetworkCredential("ayed.maissen@gmail.com", "maissenayedbrifry");
            SmtpClient smtpobj = new SmtpClient("smtp.gmail.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(o);










            return new ViewAsPdf("InvoicePrint", yo);

        }




        public ActionResult MonthlyInvoice()
        {
            db.Configuration.LazyLoadingEnabled = true;
            //transportJob transportJob = db.TransportJobs.Find(id);
            var transportJob = db.TransportJobs.Include(u => u.orders.artwork);
            if (transportJob == null)
            {
                return HttpNotFound();
            }
            int iduser = Convert.ToInt32(Session["user"].ToString());
            float wage= 0;
            var yo = transportJob;
            foreach (var VARIABLE in yo)
            {
                if (VARIABLE.transporterID==iduser && VARIABLE.Status=="Completed")
                {   DateTime di = DateTime.Now.AddMonths(-1);
                    DateTime da = DateTime.Now.AddMonths(1);
                    if (VARIABLE.DateDeDefin>=di && VARIABLE.DateDeDefin<=da )
                    {
                        
                    
               

            foreach (artwork art in VARIABLE.orders.artwork)
            {
                wage = (art.price / 100);
            }

                }
                }

            }
            ViewBag.wage = wage;
               
            /* var balance = (from a in db.TransportJobs
                            join c in db.orders on a.orders equals c.Id
                            where c.ClientID == yourDescriptionObject.ClientID
                            select a.Balance)
               .SingleOrDefault();*/
           /* MailMessage o = new MailMessage("ayed.maissen@gmail.com", "ayed.maissen@gmail.com");
            o.BodyEncoding = Encoding.UTF8;
            o.IsBodyHtml = true;
            o.Subject = "Sending Email Using Asp.Net & C#";*/
           /* var fileName = yo.title;
            var filePath = Path.Combine(Server.MapPath("/Temp"), fileName);

            var myPDF = new ViewAsPdf("Invoicemonth", yo)
            {
                FileName = fileName,
                PageSize = Size.A5,
                SaveOnServerPath = filePath
            };
            byte[] applicationPDFData = myPDF.BuildPdf(ControllerContext);
            MemoryStream stream = new MemoryStream(applicationPDFData);
            Attachment attachment = new Attachment(stream, "document.pdf");
            o.Attachments.Add(attachment);
            NetworkCredential netCred = new NetworkCredential("ayed.maissen@gmail.com", "maissenayedbrifry");
            SmtpClient smtpobj = new SmtpClient("smtp.gmail.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(o);
            */









            return new ViewAsPdf("Invoicemonth",yo.ToList());

        }





        // GET: transportJobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transportJob transportJob = db.TransportJobs.Find(id);
            if (transportJob == null)
            {
                return HttpNotFound();
            }
            return View(transportJob);
        }

        // POST: transportJobs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,DateDeDebut,DateDeDefin,title,description")] transportJob transportJob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transportJob);
        }

        // GET: transportJobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transportJob transportJob = db.TransportJobs.Find(id);
            if (transportJob == null)
            {
                return HttpNotFound();
            }
            return View(transportJob);
        }

        // POST: transportJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            transportJob transportJob = db.TransportJobs.Find(id);
            db.TransportJobs.Remove(transportJob);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
