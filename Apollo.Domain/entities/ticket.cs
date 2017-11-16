namespace Apollo.Domain.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("apollo.ticket")]
    public partial class ticket
    {
        public int id { get; set; }

        [StringLength(255)]
        public string note { get; set; }

        public DateTime? orderDate { get; set; }

        public float? price { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public int? event_id { get; set; }

        public virtual _event _event { get; set; }
    }
}
