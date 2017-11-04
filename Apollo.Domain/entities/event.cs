namespace Apollo.Domain.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("apollo.event")]
    public partial class _event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public _event()
        {
            ticket = new HashSet<ticket>();
            gallery1 = new HashSet<gallery>();
        }

        public int id { get; set; }

        public int? capacity { get; set; }

        public DateTime? creationDate { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public DateTime? endDate { get; set; }

        [StringLength(255)]
        public string imagePath { get; set; }

        public DateTime? startDate { get; set; }

        public int? status { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public int? gallery_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ticket> ticket { get; set; }

        public virtual gallery gallery { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<gallery> gallery1 { get; set; }
    }
}
