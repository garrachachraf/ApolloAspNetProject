using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apollo.Data;
using Apollo.Data.Infrastructures;
using Apollo.Domain.entities;
using Apollo.ServicePattern;



namespace Apollo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseFactory DBF = new DatabaseFactory();
            UnitOfWork UF = new UnitOfWork(DBF);
            JeeModel ctx = new JeeModel();
            artwork a=new artwork();
            
            ServicePattern.Service<artwork> SP = new Service<artwork>(UF);
            a=SP.FindById(1);
            System.Console.Out.WriteLine(a.title);
            a.title = "il3asba rabek";
            System.Console.Out.WriteLine(a.title);
            SP.Update(a);
            SP.remove(a);
            System.Console.Out.WriteLine(a.title+" after");

            System.Console.ReadKey();
        }
    }
}
