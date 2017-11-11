using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Domain.entities
{
    public class NewsLettersOpens
    {
        public int ID { get; set; }
        public NewsLetter IdNewsletter { get; set; }
        public user IdUser { get; set; }
       

    }
}
