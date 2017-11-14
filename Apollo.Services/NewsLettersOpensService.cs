using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apollo.Domain.entities;
using Apollo.Data;
using Apollo.Data.Infrastructures;

namespace Apollo.Services
{
    public class NewsLettersOpensService : INewsLetterOpensService
    {
        private JeeModel db = new JeeModel();
        static DatabaseFactory dbFactory = new DatabaseFactory();
        static UnitOfWork utw = new UnitOfWork(dbFactory);

        public bool checkopenedornot(NewsLettersOpens nlo)
        {
            var query = from NewsLettersOpens in db.newsletteropens
                        where NewsLettersOpens.IdNewsletter.Id == nlo.IdNewsletter.Id & NewsLettersOpens.IdUser.id == nlo.IdUser.id 
                        select NewsLettersOpens;
            if (query.Count() != 0)
            {
                return true;
            }
            return false;
        }
    }
}
