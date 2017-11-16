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

namespace Apollo.ASP.Controllers
{
    public class Conversations1Controller : ApiController
    {
        private JeeModel db = new JeeModel();

        // GET: api/Conversations1
        public IQueryable<Conversation> Getconversations()
        {
            return db.conversations;
        }

        // GET: api/Conversations1/5
        [ResponseType(typeof(Conversation))]
        public IHttpActionResult GetConversation(int id)
        {
            Conversation conversation = db.conversations.Find(id);
            if (conversation == null)
            {
                return NotFound();
            }

            return Ok(conversation);
        }

        // PUT: api/Conversations1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConversation(int id, Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conversation.Id)
            {
                return BadRequest();
            }

            db.Entry(conversation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversationExists(id))
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

        // POST: api/Conversations1
        [ResponseType(typeof(Conversation))]
        public IHttpActionResult PostConversation(Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.conversations.Add(conversation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = conversation.Id }, conversation);
        }

        // DELETE: api/Conversations1/5
        [ResponseType(typeof(Conversation))]
        public IHttpActionResult DeleteConversation(int id)
        {
            Conversation conversation = db.conversations.Find(id);
            if (conversation == null)
            {
                return NotFound();
            }

            db.conversations.Remove(conversation);
            db.SaveChanges();

            return Ok(conversation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConversationExists(int id)
        {
            return db.conversations.Count(e => e.Id == id) > 0;
        }
    }
}