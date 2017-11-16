using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apollo.Domain.entities;

namespace Apollo.Data.Infrastructures
{
    public class UnitOfWork
    {
        private DatabaseFactory dbfactory;
        public JeeModel ctx { get; set; }
        public UnitOfWork(DatabaseFactory dbFactory)
        {
            this.dbfactory = dbFactory;
            ctx = dbfactory.Mycontext;

        }

        public void commit()
        {
            ctx.SaveChanges();
        }

        public RepositoryBase<T> GetRepository<T>() where T : class
        {
            return new RepositoryBase<T>(dbfactory);
        }
    }
}

