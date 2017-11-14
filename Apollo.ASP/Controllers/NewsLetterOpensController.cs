using Apollo.Data;
using Apollo.Domain.entities;
using Apollo.Services;
using System.Net;
using System.Web.Mvc;

namespace Apollo.ASP.Controllers
{
    public class NewsLetterOpensController : Controller
    {
        private JeeModel db = new JeeModel();
        private INewsLetterOpensService nlos = new NewsLettersOpensService();
        public ActionResult Opened(int? newsletterid , int? userid)
        {

            if (newsletterid == null || userid==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetter newsLetter = db.newsletter.Find(newsletterid);
            user user = db.user.Find(userid);

            if (newsLetter == null || user == null )
            {
                return HttpNotFound();
            }
            
            NewsLettersOpens open = new NewsLettersOpens();
            open.IdNewsletter = newsLetter;
            open.IdUser = user;
            if (!nlos.checkopenedornot(open))
            {
                db.newsletteropens.Add(open);
                db.SaveChanges();
            }
            

            return View( );
        }
    }
}