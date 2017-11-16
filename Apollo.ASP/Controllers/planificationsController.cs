using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apollo.Data;
using Apollo.Domain.entities;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using RestSharp;

namespace Apollo.ASP.Controllers
{
    public class planificationsController : Controller
    {
        private JeeModel db = new JeeModel();
        
        public ActionResult Index()
        {
            var planification = db.planifications.Include(p => p.financer);
            return View(planification.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planification planification = db.planifications.Find(id);
            if (planification == null)
            {
                return HttpNotFound();
            }
            return View(planification);
        }

        public ActionResult Create()
        {
            ViewBag.financerId = new SelectList(db.user, "id", "role");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,startDate,finishDate,description,incomeSource,amount,period,updateDate,financerId")] planification p)
        {
            string period = String.Empty;
            if (ModelState.IsValid)
            {
                var finishMonth = p.finishDate.Month;
                if (p.amount <= 0)
                {
                    ViewBag.DateErrorMessage = "The Plannified Amount must be stricktly positif ";
                    return View(p);
                }
                else if (p.startDate > p.finishDate)
                {
                    ViewBag.DateErrorMessage = "Finish date must be > than the Start Date ";
                    return View(p);
                }
                else if (p.startDate.Year != p.finishDate.Year)
                {
                    ViewBag.DateErrorMessage = "The maximum Period is 1 Year ";
                    return View(p);
                }
                else if (!p.startDate.Day.Equals(1))
                {
                    ViewBag.DateErrorMessage = "Start Date not matched for a beginning of month ";
                    return View(p);
                }
                else if (((finishMonth.Equals(1) || finishMonth.Equals(3) || finishMonth.Equals(5) ||
                           finishMonth.Equals(7) || finishMonth.Equals(8) || finishMonth.Equals(10) ||
                           finishMonth.Equals(12)) && !p.finishDate.Day.Equals(31))
                           ||
                           ((finishMonth.Equals(4) || finishMonth.Equals(6) || finishMonth.Equals(9) ||
                           finishMonth.Equals(11)) && !p.finishDate.Day.Equals(30))
                           ||
                           (finishMonth.Equals(2) && (!(p.finishDate.Day.Equals(28) || p.finishDate.Day.Equals(29))))
                        )
                {
                    ViewBag.DateErrorMessage = "Finish Date not matched for an ending of month";
                    return View(p);
                }
                else if (!(finishMonth.Equals(12) && p.startDate.Month.Equals(1)) && !(finishMonth == p.startDate.Month) && !(p.startDate.Year == p.finishDate.Year))
                {
                    ViewBag.DateErrorMessage = "Start & Finish Dates not matching for a [Year] or [Month] Period";
                    return View(p);
                }
                else if (finishMonth == p.startDate.Month)
                {
                    bool exist = false;
                    foreach (var planif in db.planifications.ToList())
                    {
                        if (planif.startDate == p.startDate && planif.incomeSource == p.incomeSource)
                        {
                            exist = true;
                        }
                    }
                    if (exist)
                    {
                        ViewBag.DateErrorMessage = "Planification Already Exist";
                        return View(p);
                    }
                    else
                    {
                        period = period + Convert.ToString(p.startDate.Year);
                        switch (p.startDate.Month)
                        {
                            case (1):
                                period = "Januaray " + period;
                                break;
                            case (2):
                                period = "February " + period; ;
                                break;
                            case (3):
                                period = "Mars " + period; ;
                                break;
                            case (4):
                                period = "April " + period; ;
                                break;
                            case (5):
                                period = "May " + period; ;
                                break;
                            case (6):
                                period = "June " + period; ;
                                break;
                            case (7):
                                period = "July " + period; ;
                                break;
                            case (8):
                                period = "August " + period; ;
                                break;
                            case (9):
                                period = "September " + period; ;
                                break;
                            case (10):
                                period = "October " + period; ;
                                break;
                            case (11):
                                period = "November " + period; ;
                                break;
                            case (12):
                                period = "December " + period; ;
                                break;
                        }
                    }
                }
                else
                {
                    bool exist = false;
                    foreach (var planif in db.planifications.ToList())
                    {
                        if (planif.startDate == p.startDate && planif.finishDate == p.finishDate && planif.incomeSource == p.incomeSource)
                        {
                            exist = true;
                        }

                    }
                    if (exist)
                    {
                        ViewBag.DateErrorMessage = "Annual Planification Already Exist";
                        return View(p);
                    }
                    else
                    {
                        period = Convert.ToString(p.startDate.Year) + "Full Year";
                    }
                 }
                p.period = period;
                int idcurrent = Convert.ToInt32(Session["user"].ToString());
                p.financerId = idcurrent;
                p.updateDate = DateTime.Now;
                db.planifications.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.financerId = new SelectList(db.user, "id", "role", p.financerId);
            return View(p);
        }

        public ActionResult IncomeChart()
        {
            List<ticket> tickets = AllTickets();
            var totalTickets = (float)0.0;
            foreach (var ticket in tickets)
            {
                totalTickets = (float)(totalTickets + ticket.price);
            }

            var totalOrders = (float)0.0;
            foreach (var order in db.orders.ToList())
            {
                totalOrders = totalOrders + order.totalAmount;
            }

            var plannedTickets = 0;
            var plannedOrders = 0;
            foreach (var planif in db.planifications.ToList())
            {
                if (planif.incomeSource == "Tickets")
                {
                    plannedTickets = plannedTickets + planif.amount;
                }else
                {
                    plannedOrders = plannedOrders + planif.amount;
                }
                
            }
            ViewBag.ticketsPlanif = plannedTickets;
            ViewBag.ordersPlanif = plannedOrders;
            ViewBag.ticketsIncome = totalTickets;
            ViewBag.ordersIncome = totalOrders;
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planification planifications = db.planifications.Find(id);
            if (planifications == null)
            {
                return HttpNotFound();
            }
            ViewBag.financerId = new SelectList(db.user, "id", "role", planifications.financerId);
            return View(planifications);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,startDate,finishDate,description,incomeSource,amount,period,updateDate,financerId")] planification p)
        {                        
            if (ModelState.IsValid)
            {
                if (p.amount <= 0)
                {
                    ViewBag.DateErrorMessage = "The Plannified Amount must be stricktly positif ";
                    return View(p);
                }
                int idcurrent = Convert.ToInt32(Session["user"].ToString());
                p.financerId = idcurrent;
                p.updateDate = DateTime.Now;
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.financerId = new SelectList(db.user, "id", "role", p.financerId);
            return View(p);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planification planification = db.planifications.Find(id);
            if (planification == null)
            {
                return HttpNotFound();
            }
            return View(planification);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            planification planification = db.planifications.Find(id);
            db.planifications.Remove(planification);
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

        public List<ticket> AllTickets()
        {
            var client = new RestClient("http://localhost:18080/Apollo-web/app/");
            var request = new RestRequest("tickets/", Method.GET);
            request.ToString();
            var response = client.Execute<List<ticket>>(request);
            return response.Data;
        }
        public ActionResult IncomeTicket()
        {
            var totalOrders = (float)0.0;
            foreach (var order in db.orders.ToList())
            {
                totalOrders = totalOrders + order.totalAmount;
            }
            ViewBag.ordersIncome = totalOrders;
            List<ticket> tickets = AllTickets();
            return View(tickets);
        }
        public ActionResult IncomeOrder()
        {
            List<ticket> tickets = AllTickets();
            var totalTickets = (float)0.0;
            foreach (var ticket in tickets)
            {
                totalTickets = (float)(totalTickets + ticket.price);
            }
            ViewBag.ticketsIncome = totalTickets;
            return View(db.orders.ToList());
        }

    }
}
