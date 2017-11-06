using Apollo.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apollo.ServicePattern;


namespace Apollo.Services
{
   public interface IGestionArtWork:IService<artwork>
    {
        IEnumerable<artwork> ListerProductByCategoryIn2008(String name);
    }
}
