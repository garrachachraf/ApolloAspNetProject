using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apollo.Data;
using Apollo.Domain.entities;
using RestSharp;

namespace Apollo.ASP.Controllers
{
    public class HomeController : Controller
    {
        private JeeModel db = new JeeModel();
        public ActionResult Index()
        {
            return View();
        }
        // POST: users/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "password,userName")] user user)
        {
           
                var client = new RestClient("http://localhost:18080/Apollo-web/app/");
                var request = new RestRequest("users/login", Method.POST);
                request.AddHeader("Content-type", "application/x-www-form-urlencoded");
                request.AddParameter("login", user.userName);
                request.AddParameter("password", user.password);
                IRestResponse response = client.Execute(request);
                var code = response.StatusCode;
                var token = response.Content;
                string authorizationHeader = response.Headers.ToList()
                    .Find(x => x.Name == "Authorization").Value.ToString();
                Session["token"] = authorizationHeader;
                ViewBag.role = token;
            if (token =="Admin")
            {
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index", "transportJobs"); ;
           
        }









        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}