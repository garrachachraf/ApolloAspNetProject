using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;//permer de declare les expression l'ambda 
namespace Apollo.Data.Infrastructures
{
    interface IRespositoryBase<T> where T : class
    {
        void Create(T entity);
        void remove(T entity);
        T FindById(int id);
        T FindById(String id);
        T FindById(int? id);
        void Update(T entity);
        //IEnumerable<T> FindAll();
        //n'est pas nesscaire parque on find by condtion fait l'affaire ila condtion null
        void Remove(Expression<Func<T, bool>> condition);//prend en pram un expression lambda
                                                         //c'est le type de l'expression lambda
        IEnumerable<T> FindByCondition(
            Expression<Func<T, bool>> condition = null,
            Expression<Func<T, bool>> orederby = null);
        IQueryable<T> QueryObjectGraph(Expression<Func<T, object>> filter);
    }
   
}
