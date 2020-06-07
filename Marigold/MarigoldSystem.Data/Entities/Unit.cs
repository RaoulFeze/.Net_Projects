namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Unit")]
    public partial class Unit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unit()
        {
            Crews = new HashSet<Crew>();
        }

        public int UnitID { get; set; }

        public int YardID { get; set; }

        [Required]
        [StringLength(20)]
        public string UnitNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string UnitDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Crew> Crews { get; set; }

        public virtual Yard Yard { get; set; }
    }
}
