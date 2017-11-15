using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Apollo.Data;
using Apollo.Domain.entities;
using Apollo.Services;

namespace Apollo.ASP.Controllers
{
    public class MessagesController : ApiController
    {
        private JeeModel db = new JeeModel();

        // GET: api/Messages
        public IQueryable<Message> Getmessages()
        {
            return db.messages;
        }

        // GET: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessage(int id)
        {
            Message message = db.messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.Id)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            message.date = DateTime.Now;
            db.messages.Add(message);
            db.SaveChanges();
            user u =db.user.Find(message.SenderID);
            if(u.role == "Admin") // =! admin TODO
            {
                CommonTools.sendPushNotif(u.userName + " : " + message.Content, "role", "Admin");
            }
            return CreatedAtRoute("DefaultApi", new { id = message.Id }, message);
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult DeleteMessage(int id)
        {
            Message message = db.messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            db.messages.Remove(message);
            db.SaveChanges();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.messages.Count(e => e.Id == id) > 0;
        }
    }
}