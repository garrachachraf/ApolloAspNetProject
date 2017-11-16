using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Apollo.Data;
using Apollo.Domain.entities;
using Apollo.Services;
using RestSharp;
using System.Web.Script.Serialization;

namespace Apollo.ASP.Controllers
{
    public class HomeController : Controller
    {
         private LoginService login =new LoginService();

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

            IRestResponse<user> response = login.login(user);

            var code = response.StatusCode;
            var token = response.Content;
            var role = response.Data.role;

            string authorizationHeader = response.Headers.ToList()
                    .Find(x => x.Name == "Authorization").Value.ToString();
                Session["token"] = authorizationHeader;
                ViewBag.role = role;
                Session["user"] = response.Data.id;
                Session["currentUser"] = user.userName;
            if (role == "transporter")
            {

                return RedirectToAction("Index", "transportJobs"); ;
            }
            if (role == "Admin")
            {

                return RedirectToAction("Index", "NewsLetters"); ;
            }

            return RedirectToAction("Index");
        }



        public ActionResult Logout()
        {


            Session.Clear();
            Session.Abandon();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            try
            {
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                Response.Expires = -1000;
                Response.CacheControl = "no-cache";
                Session["user"] = null;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return RedirectToAction("");
        }




    }
}
