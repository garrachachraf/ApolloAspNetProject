using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Data.Infrastructures
{
    interface IUnitOfWork
    {

        RepositoryBase<T> GetRepositoryBase<T>() where T : class;
        void commit();

    }
}
