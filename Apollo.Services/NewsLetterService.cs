using Apollo.Data;
using Apollo.Data.Infrastructures;
using Apollo.Domain.entities;
using Apollo.ServicePattern;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Services
{
    public class NewsLetterService : Service<NewsLetter>, INewsLetterService
    {
        private JeeModel db = new JeeModel();
        static DatabaseFactory dbFactory = new DatabaseFactory();
        static UnitOfWork utw = new UnitOfWork(dbFactory);
        static IMailService ms = new MailService();
        public NewsLetterService() : base(utw)
        {

        }
        public void SendEmailsTo(NewsLetter newsletter)
        {
            int nb = 0;
            getAll("artists").ForEach(u => {
                //Console.WriteLine(u.email);
                nb++;
                newsletter.msg = newsletter.msg + "<img src=http://wild.apollo-esprit.net:2752/NewsLetterOpens/Opened/?newsletterid=" + newsletter.Id+ "&userid="+u.id+ "  height=1 width=1>";
                ms.SendThisMail(u.email, newsletter.Subject, newsletter.msg);
            });
            
            db.newsletter.Attach(newsletter);
            newsletter.nbrecivers = nb;
            db.SaveChanges();

        }

        public int NbOpens(NewsLetter newsletter)
        {
            int counts = 0;
            var query = from NewsLettersOpens in db.newsletteropens
                        where NewsLettersOpens.IdNewsletter.Id == newsletter.Id
                        select NewsLettersOpens ;
            foreach (var item in query)
            {
                counts++;
            }
            return counts;
        }

        private List<user> getAll(String role)
        {
            string to = "";
            switch (role)
            {
                case "Artists": to = "artists";
                    break;
                case "GO": to = "gowners";
                    break;

            }
            var restClient = new RestClient("http://wild.apollo-esprit.net:18080/Apollo-web/app/Profile/"+to);
            var request = new RestRequest(Method.GET);
            
            var response = restClient.Execute<List<user>>(request);
            return response.Data;
        }


    }
}
