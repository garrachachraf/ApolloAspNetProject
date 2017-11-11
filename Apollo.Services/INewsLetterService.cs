using Apollo.Domain.entities;
using Apollo.ServicePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Services
{
    public interface INewsLetterService: IService<NewsLetter>
    {
       void SendEmailsTo(NewsLetter newsletter);
        int NbOpens(NewsLetter newsletter);
    }
}
