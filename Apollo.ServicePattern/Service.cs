using Apollo.Data.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Apollo.ServicePattern
{
    public class Service<T> : IService<T> where T : class
    {
        UnitOfWork utw;

        public Service(UnitOfWork utw)
        {
            this.utw = utw;
        }

        public void Commit()
        {
            utw.commit();
        }

        public virtual void Create(T entity)
        {
            utw.GetRepository<T>().Create(entity);
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> condition = null, Expression<Func<T, bool>> orederby = null)
        {
            return utw.GetRepository<T>().FindByCondition(condition, orederby);
        }

        public T FindById(string id)
        {
            return utw.GetRepository<T>().FindById(id);
        }

        public T FindById(int id)
        {
            return utw.GetRepository<T>().FindById(id);
        }

        public void Remove(Expression<Func<T, bool>> condition)
        {
             utw.GetRepository<T>().Remove(condition);
        }

        public void remove(T entity)
        {
            utw.GetRepository<T>().remove(entity);
        }

        public void Update(T entity)
        {
            utw.GetRepository<T>().Update(entity);
        }
    }
}
