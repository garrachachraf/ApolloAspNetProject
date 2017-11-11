using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Services
{
    public interface IMailService
    {
        void SendThisMail(string to , string subject , string msg);
    }
}
