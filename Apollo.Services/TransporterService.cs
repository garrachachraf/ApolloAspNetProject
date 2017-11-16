using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apollo.Data.Infrastructures;
using Apollo.Domain.entities;
using Apollo.ServicePattern;

namespace Apollo.Services
{
   public  class TransporterService : Service<transportJob>,ITransporterService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        static UnitOfWork utw = new UnitOfWork(dbFactory);

        public TransporterService() : base(utw)
        {
        }

        public IQueryable<transportJob> GetallArtWork()
        {
            return utw.GetRepository<transportJob>().QueryObjectGraph(job => job.orders.artwork);
        }

      
    }
}
