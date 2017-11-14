using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Services
{
    public interface IMailService
    {
        void SendThisMail(string to, string subject, string msg);
        void SendMailInvoice(string to, string subject, string msg, string path , string filename);
    }
}
