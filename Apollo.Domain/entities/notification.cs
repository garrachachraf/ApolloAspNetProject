namespace Apollo.Domain.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("apollo.notification")]
    public partial class notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public DateTime? date { get; set; }

        [StringLength(255)]
        public string link { get; set; }

        [StringLength(255)]
        public string message { get; set; }

        [Column(TypeName = "bit")]
        public bool seen { get; set; }

        public int? user_id { get; set; }

        public virtual user user { get; set; }
    }
}
