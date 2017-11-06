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
        private JeeModel ctx;
        public UnitOfWork(DatabaseFactory dbFactory)
        {
            this.dbfactory = dbFactory;
            ctx = dbfactory.Mycontext;

        }
        public JeeModel Ctx
        {
            get { return Ctx; }
        }
        public void commit()
        {
            Ctx.SaveChanges();
        }

        public RepositoryBase<T> GetRepository<T>() where T : class
        {
            return new RepositoryBase<T>(dbfactory);
        }
    }
}

