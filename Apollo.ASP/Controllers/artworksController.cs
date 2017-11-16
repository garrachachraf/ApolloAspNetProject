using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using Apollo.Data;
using Apollo.Domain.entities;
using Apollo.Services;

namespace Apollo.ASP.Controllers
{
    public class artworksController : Controller
    {
        private JeeModel db = new JeeModel();
        private GestionArtWork ga=new GestionArtWork();
        

        // GET: artworks
        public ActionResult Index()
        {  
            var artwork = ga.FindByCondition();
            return View(artwork.ToList());
        }

        // GET: artworks/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var artwork = ga.FindById(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            return View(artwork);
        }

        // GET: artworks/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: artworks/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,category,descreption,mediaPath,price,releaseDate,title,uploadDate,artist_id")] artwork artwork)
        {
            if (ModelState.IsValid)
            {
               ga.Create(artwork);
             
                return RedirectToAction("Index");
            }

          
            return View(artwork);
        }

        // GET: artworks/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artwork artwork = ga.FindById(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
           
            return View(artwork);
        }

        // POST: artworks/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,category,descreption,mediaPath,price,releaseDate,title,uploadDate,artist_id")] artwork artwork)
        {
            if (ModelState.IsValid)
                
            {

                artwork Yartwork = ga.FindById(artwork.id);
                Yartwork.title = artwork.title;
                Yartwork.descreption = artwork.descreption;
                Yartwork.price = artwork.price;
                

                ga.Update(Yartwork);
               
                return RedirectToAction("Index");
            }
         
            return View(artwork);
        }

        // GET: artworks/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artwork artwork = ga.FindById(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            return View(artwork);
        }

        // POST: artworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            artwork artwork = ga.FindById(id);
            ga.remove(artwork);

            return RedirectToAction("Index");
        }

       /* protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ga.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
