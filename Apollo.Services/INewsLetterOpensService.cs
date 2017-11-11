using Apollo.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Services
{
    public interface INewsLetterOpensService
    {
        bool checkopenedornot(NewsLettersOpens nlo);
    }
}
