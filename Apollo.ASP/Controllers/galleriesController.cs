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

namespace Apollo.ASP.Controllers
{
    public class galleriesController : Controller
    {
        private JeeModel db = new JeeModel();

        // GET: galleries
        public ActionResult Index()
        {
            var gallery = db.gallery.Include(g => g.user);
            return View(gallery.ToList());
        }

        // GET: galleries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gallery gallery = db.gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: galleries/Create
        public ActionResult Create()
        {
            ViewBag.galleryOwner_id = new SelectList(db.user, "id", "role");
            return View();
        }

        // POST: galleries/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,description,address,latitude,longitude,maxCapacity,name,cleaningFee,daily,hourly,minimumBooking,monthly,securityDeposit,weekly,surface,galleryOwner_id")] gallery gallery)
        {
            if (ModelState.IsValid)
            {
                db.gallery.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.galleryOwner_id = new SelectList(db.user, "id", "role", gallery.galleryOwner_id);
            return View(gallery);
        }

        // GET: galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gallery gallery = db.gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.galleryOwner_id = new SelectList(db.user, "id", "role", gallery.galleryOwner_id);
            return View(gallery);
        }

        // POST: galleries/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,description,address,latitude,longitude,maxCapacity,name,cleaningFee,daily,hourly,minimumBooking,monthly,securityDeposit,weekly,surface,galleryOwner_id")] gallery gallery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.galleryOwner_id = new SelectList(db.user, "id", "role", gallery.galleryOwner_id);
            return View(gallery);
        }

        // GET: galleries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gallery gallery = db.gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gallery gallery = db.gallery.Find(id);
            db.gallery.Remove(gallery);
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
