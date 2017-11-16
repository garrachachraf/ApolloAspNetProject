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
    public class artworkServeController : ApiController
    {
        private JeeModel db = new JeeModel();

        // GET: api/artworkServe
        public IQueryable<artwork> Getartwork()
        {
            return db.artwork;
        }

        // GET: api/artworkServe/5
        [ResponseType(typeof(artwork))]
        public IHttpActionResult Getartwork(int id)
        {
            artwork artwork = db.artwork.Find(id);
            if (artwork == null)
            {
                return NotFound();
            }

            return Ok(artwork);
        }

        // PUT: api/artworkServe/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putartwork(int id, artwork artwork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artwork.id)
            {
                return BadRequest();
            }

            db.Entry(artwork).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!artworkExists(id))
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

        // POST: api/artworkServe
        [ResponseType(typeof(artwork))]
        public IHttpActionResult Postartwork(artwork artwork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.artwork.Add(artwork);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (artworkExists(artwork.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = artwork.id }, artwork);
        }

        // DELETE: api/artworkServe/5
        [ResponseType(typeof(artwork))]
        public IHttpActionResult Deleteartwork(int id)
        {
            artwork artwork = db.artwork.Find(id);
            if (artwork == null)
            {
                return NotFound();
            }

            db.artwork.Remove(artwork);
            db.SaveChanges();

            return Ok(artwork);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool artworkExists(int id)
        {
            return db.artwork.Count(e => e.id == id) > 0;
        }
    }
}