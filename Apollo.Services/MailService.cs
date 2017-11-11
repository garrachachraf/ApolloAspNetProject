﻿using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Services
{
    public class MailService : IMailService
    {
        public void SendThisMail(string to, string subject, string msg)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            "key-26abf6ebb7275b70149f28d674bca3e4");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "majouda.com", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "donot replay <donotreplay@majouda.com>");
            request.AddParameter("to", to);
       
            request.AddParameter("subject", subject);
            request.AddParameter("text", msg);
            request.Method = Method.POST;
            client.Execute(request);
        }
    }
}
