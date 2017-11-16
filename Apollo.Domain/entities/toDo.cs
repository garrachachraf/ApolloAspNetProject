using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Domain.entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("toDo")]
    public class toDo
    {
        public int id { get; set; }
        [Required(ErrorMessage = "What to do ?")]
        [StringLength(255)]
        public String toDoStr { get; set; }
        [Required(ErrorMessage = "Deadline Date is required")]
        public DateTime deadlineDate { get; set; }
        public Nullable<int> financerId { get; set; }
        public user financer { get; set; }
        public int status { get; set; }

    }
}
