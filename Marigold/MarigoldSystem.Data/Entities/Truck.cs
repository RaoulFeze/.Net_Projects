namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Truck")]
    public partial class Truck
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Truck()
        {
            Crews = new HashSet<Crew>();
        }

        public int TruckID { get; set; }

        public int? CategoryID { get; set; }

        public int? YardID { get; set; }

        [Required]
        [StringLength(20)]
        public string TruckNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string TruckDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Crew> Crews { get; set; }

        public virtual TruckCategory TruckCategory { get; set; }

        public virtual Yard Yard { get; set; }
    }
}
