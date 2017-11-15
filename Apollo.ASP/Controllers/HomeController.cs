using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apollo.Data;
using Apollo.Domain.entities;
using RestSharp;
using System.Web.Script.Serialization;

namespace Apollo.ASP.Controllers
{
    public class HomeController : Controller
    {
        
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
           
                var client = new RestClient("http://127.0.0.1:18080/Apollo-web/app/");
                var request = new RestRequest("users/login", Method.POST);
                request.AddHeader("Content-type", "application/x-www-form-urlencoded");
                request.AddParameter("login", user.userName);
                request.AddParameter("password", user.password);
                IRestResponse response = client.Execute(request);
                var code = response.StatusCode;
                var token = response.Content;
            
            IRestResponse<user> response2 = client.Execute<user>(request);
            JavaScriptSerializer j = new JavaScriptSerializer();
            dynamic data = j.Deserialize(token, typeof(object));
            System.Diagnostics.Debug.WriteLine(token);
            Session["currentUser"] = "";
            if (data["role"] == "Admin")
            {
                var role = response2.Data.role;
                ViewBag.role = role;


                string authorizationHeader = response.Headers.ToList()
                        .Find(x => x.Name == "Authorization").Value.ToString();
                Session["token"] = authorizationHeader;

                Session["user"] = response2.Data.id;
                Session["currentUser"] = user.userName;
                return RedirectToAction("Index", "Conversations");
            }
            else 
            {
                return RedirectToAction("Error", "Conversations");
            }

           
        }

    }
}