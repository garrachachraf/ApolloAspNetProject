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
using System.Web.Script.Serialization;

namespace Apollo.ASP.Controllers
{
    public class ConversationsController : Controller
    {
        private JeeModel db = new JeeModel();

        // GET: Conversations
        public ActionResult Index()
        {
            var conversations = db.conversations.Include(c => c.Admin).Include(c => c.Client).Include(m => m.Messages);
            return View(conversations.ToList());
        }
        // GET: Conversations
        public ActionResult Error()
        {
            var conversations = db.conversations.Include(c => c.Admin).Include(c => c.Client);
            return View(conversations.ToList());
        }

        // GET: Conversations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conversation conversation = db.conversations.Include(m => m.Messages).Include(c => c.Client).SingleOrDefault(x => x.Id == id);
            if (conversation == null)
            {
                return HttpNotFound();
            }
            return View(conversation);
        }

        // GET: Conversations/Create
        public ActionResult Create()
        {
            ViewBag.AdminID = new SelectList(db.user, "id", "role");
            ViewBag.ClientID = new SelectList(db.user, "id", "role");
            return View();
        }

        // POST: Conversations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientID,AdminID,Object")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                db.conversations.Add(conversation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminID = new SelectList(db.user, "id", "role", conversation.AdminID);
            ViewBag.ClientID = new SelectList(db.user, "id", "role", conversation.ClientID);
            return View(conversation);
        }

        // GET: Conversations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conversation conversation = db.conversations.Find(id);
            if (conversation == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminID = new SelectList(db.user, "id", "role", conversation.AdminID);
            ViewBag.ClientID = new SelectList(db.user, "id", "role", conversation.ClientID);
            return View(conversation);
        }

        // POST: Conversations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientID,AdminID,Object")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conversation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminID = new SelectList(db.user, "id", "role", conversation.AdminID);
            ViewBag.ClientID = new SelectList(db.user, "id", "role", conversation.ClientID);
            return View(conversation);
        }

        // GET: Conversations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conversation conversation = db.conversations.Find(id);
            if (conversation == null)
            {
                return HttpNotFound();
            }
            return View(conversation);
        }

        // POST: Conversations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Conversation conversation = db.conversations.Find(id);
            db.conversations.Remove(conversation);
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
