using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Domain.entities
{
    public class NewsLetter
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string msg { get; set; }
        public int nbrecivers { get; set; }

    }
}
