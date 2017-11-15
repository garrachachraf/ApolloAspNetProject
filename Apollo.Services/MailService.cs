using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Services
{
    public class MailService : IMailService
    {
        public void SendMailInvoice(string to, string subject, string msg, string path, string filename)
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
            request.AddParameter("html", msg);
            request.AddFile("attachment", Path.Combine(path, filename));
            request.Method = Method.POST;
            client.Execute(request);
        }

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
            request.AddParameter("html", msg);
            request.Method = Method.POST;
            client.Execute(request);
        }
        public void SendMail(string to, string subject, string msg, string path, string filename, byte[] applicationPDFData)
        {
            MemoryStream stream = new MemoryStream(applicationPDFData);
            MailMessage o = new MailMessage("ayed.maissen@gmail.com", "ayed.maissen@gmail.com");
            o.BodyEncoding = Encoding.UTF8;
            o.IsBodyHtml = true;
            o.Subject = "Sending Email Using Asp.Net & C#";
            Attachment attachment = new Attachment(stream, "document.pdf");
            o.Attachments.Add(attachment);
            NetworkCredential netCred = new NetworkCredential("ayed.maissen@gmail.com", "maissenayedbrifry");
            SmtpClient smtpobj = new SmtpClient("smtp.gmail.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(o);
        }

    }
}
