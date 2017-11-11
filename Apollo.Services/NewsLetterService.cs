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
            getAll(newsletter.To).ForEach(u => {
                ms.SendThisMail(u.email, newsletter.Subject, newsletter.msg);
            });
            
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
            
            var restClient = new RestClient("https://api.getdropbox.com");
            var request = new RestRequest(Method.GET);
            request.Resource = "Profile/{role}";
            request.AddParameter("role", role);
            var response = restClient.Execute<List<user>>(request);
            return response.Data;
        }
    }
}
