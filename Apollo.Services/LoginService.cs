using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apollo.Data.Infrastructures;
using Apollo.Domain.entities;
using Apollo.ServicePattern;
using RestSharp;

namespace Apollo.Services
{
   public class LoginService : Service<user>, ILoginService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        static UnitOfWork utw = new UnitOfWork(dbFactory);


        public LoginService() : base (utw)
        { }

        public IRestResponse<user> login(user user)
        {

            var client = new RestClient("http://localhost:18080/Apollo-web/app/");
            var request = new RestRequest("users/login", Method.POST);
            request.AddHeader("Content-type", "application/x-www-form-urlencoded");
            request.AddParameter("login", user.userName);
            request.AddParameter("password", user.password);
            IRestResponse response = client.Execute(request);
            IRestResponse<user> response2 = client.Execute<user>(request);
            return response2;

        }
       







    }
}
