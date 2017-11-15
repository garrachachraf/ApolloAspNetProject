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
    public class MessagesUIController : Controller
    {
        private JeeModel db = new JeeModel();

        // GET: MessagesUI
        public ActionResult Index()
        {
            var messages = db.messages.Include(m => m.Conversation).Include(m => m.Sender);
            return View(messages.ToList());
        }

        // GET: MessagesUI/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: MessagesUI/Create
        public ActionResult Create()
        {
            ViewBag.ConversationID = new SelectList(db.conversations, "Id", "Object");
            ViewBag.SenderID = new SelectList(db.user, "id", "role");
            return View();
        }

        // POST: MessagesUI/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SenderID,date,ConversationID,Content")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConversationID = new SelectList(db.conversations, "Id", "Object", message.ConversationID);
            ViewBag.SenderID = new SelectList(db.user, "id", "role", message.SenderID);
            return View(message);
        }

        // GET: MessagesUI/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConversationID = new SelectList(db.conversations, "Id", "Object", message.ConversationID);
            ViewBag.SenderID = new SelectList(db.user, "id", "role", message.SenderID);
            return View(message);
        }

        // POST: MessagesUI/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SenderID,date,ConversationID,Content")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConversationID = new SelectList(db.conversations, "Id", "Object", message.ConversationID);
            ViewBag.SenderID = new SelectList(db.user, "id", "role", message.SenderID);
            return View(message);
        }

        // GET: MessagesUI/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: MessagesUI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.messages.Find(id);
            db.messages.Remove(message);
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
