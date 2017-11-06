using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apollo.Domain.entities;
using MyFinance.Data.Infrastructures;

namespace Apollo.Data.Infrastructures
{
    public class DatabaseFactory : Disposable,IDatebaseFactory
    {
        private JeeModel mycontext;
        public DatabaseFactory()
        {
            mycontext = new JeeModel();
        }
        public JeeModel Mycontext
        {
            get
            {
                return mycontext;
            }
        }
        protected override void DisposeCore()
        {
            if (Mycontext != null)
                Mycontext.Dispose();
        }
    }
}
