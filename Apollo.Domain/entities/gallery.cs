namespace Apollo.Domain.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("apollo.gallery")]
    public partial class gallery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public gallery()
        {
            _event = new HashSet<_event>();
            event1 = new HashSet<_event>();
            media = new HashSet<media>();
            schedule = new HashSet<schedule>();
            user1 = new HashSet<user>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        public double? latitude { get; set; }

        public double? longitude { get; set; }

        public int? maxCapacity { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public float? cleaningFee { get; set; }

        public float? daily { get; set; }

        public float? hourly { get; set; }

        public int? minimumBooking { get; set; }

        public float? monthly { get; set; }

        public float? securityDeposit { get; set; }

        public float? weekly { get; set; }

        public double? surface { get; set; }

        public int? galleryOwner_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<_event> _event { get; set; }

        public virtual user user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<_event> event1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<media> media { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<schedule> schedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user> user1 { get; set; }
    }
}
