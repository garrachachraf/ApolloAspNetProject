using Apollo.Domain.entities;
using Apollo.ServicePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apollo.Data.Infrastructures;

namespace Apollo.Services
{
    public class GestionMessage : Service<Message>, IGestionMessage
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        static UnitOfWork utw = new UnitOfWork(dbFactory);

        public GestionMessage() : base(utw)
        {
        }
    }
}
