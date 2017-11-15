using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apollo.Domain.entities;
using Apollo.ServicePattern;

namespace Apollo.Services
{
    public interface ITransporterService: IService<transportJob>
    {
        IQueryable<transportJob> GetallArtWork();
       
    }
}
