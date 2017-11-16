using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Domain.entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("planification")]
    public class planification
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime startDate { get; set; }
        [Required(ErrorMessage = "Finish Date is required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime finishDate { get; set; }
        //[Required(ErrorMessage = "Material cost is required")]
        [StringLength(255)]
        public String description { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Income Source is required")]
        public String incomeSource { get; set; }
        public int amount { get; set; }
        [StringLength(255)]
        public String period { get; set; }
        [DisplayFormat(DataFormatString = "{0:R}")]
        public DateTime updateDate { get; set; }
        public Nullable<int> financerId { get; set; }
        public user financer { get; set; }
    }
}
