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
    public class toDoesController : Controller
    {
        private JeeModel db = new JeeModel();

        // GET: toDoes
        public ActionResult Index()
        {
            int idcurrent = Convert.ToInt32(Session["user"].ToString());
            var toDoes = db.toDoes.Include(t => t.financer);
            return View(toDoes.ToList());
        }
        
        public ActionResult Create()
        {
            ViewBag.financerId = new SelectList(db.user, "id", "role");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,toDoStr,deadlineDate,financerId")] toDo toDo)
        {
            if (ModelState.IsValid)
            {
                toDo.financerId= Convert.ToInt32(Session["user"].ToString());
                db.toDoes.Add(toDo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.financerId = new SelectList(db.user, "id", "role", toDo.financerId);
            return View(toDo);
        }
        
        public ActionResult SwitchTask(int? id)
        {
            if (ModelState.IsValid)
            {
                toDo task = db.toDoes.Find(id); 
                if (task.status == 0)
                {
                    task.status = 1;
                }
                else
                {
                    task.status = 0;
                }
                db.SaveChanges();
            }
            return View() ;
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
