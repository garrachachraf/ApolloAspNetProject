using Apollo.Domain.entities;
using Apollo.ServicePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Apollo.Data.Infrastructures;

namespace Apollo.Services
{
    public class GestionArtWork : Service<artwork>,IGestionArtWork
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        static UnitOfWork utw = new UnitOfWork(dbFactory);

        public GestionArtWork() : base(utw)
        {

        }

        public IEnumerable<artwork> ListerProductByCategoryIn2008(string name)
        {
            return utw.GetRepository<artwork>().FindByCondition(x=>x.title==name);
        }
    }
}
