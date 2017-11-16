using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Domain.entities
{
    public class transportJob
    {
        public int id { get; set; }
        public Nullable<int> transporterID { get; set; }
        public  user transporter { get; set; }
        public Nullable<int> ordersID { get; set; }
        public orders orders { get; set; }
        public DateTime DateDeDebut { get; set; }
        public DateTime DateDeDefin { get; set; }
        [StringLength(255)]
        public String title { get; set; }
        [StringLength(255)]
        public String  description { get; set; }
        [StringLength(255)]
        public String Status { get; set; }


    }
}
