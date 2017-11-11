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
using Apollo.Services;

namespace Apollo.ASP.Controllers
{
    public class NewsLettersController : Controller
    {
        private JeeModel db = new JeeModel();
        private INewsLetterService ns = new NewsLetterService();
        // GET: NewsLetters
        public ActionResult Index()
        {
            return View(db.newsletter.ToList());
        }

        // GET: NewsLetters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetter newsLetter = db.newsletter.Find(id);
            if (newsLetter == null)
            {
                return HttpNotFound();
            }
            ViewBag.views = ns.NbOpens(newsLetter);
            return View(newsLetter);
        }
       
      
        public ActionResult SendEmails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetter newsLetter = db.newsletter.Find(id);
            if (newsLetter == null)
            {
                return HttpNotFound();
            }

            ns.SendEmailsTo(newsLetter);
            return View();
        }
        // GET: NewsLetters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsLetters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,To,Subject,msg,nbrecivers")] NewsLetter newsLetter)
        {
            if (ModelState.IsValid)
            {
                db.newsletter.Add(newsLetter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsLetter);
        }

        // GET: NewsLetters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetter newsLetter = db.newsletter.Find(id);
            if (newsLetter == null)
            {
                return HttpNotFound();
            }
            return View(newsLetter);
        }

        // POST: NewsLetters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,To,Subject,msg,nbrecivers")] NewsLetter newsLetter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsLetter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsLetter);
        }

        // GET: NewsLetters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetter newsLetter = db.newsletter.Find(id);
            if (newsLetter == null)
            {
                return HttpNotFound();
            }
            return View(newsLetter);
        }

        // POST: NewsLetters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsLetter newsLetter = db.newsletter.Find(id);
            db.newsletter.Remove(newsLetter);
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
