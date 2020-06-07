namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DriverPermit")]
    public partial class DriverPermit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DriverPermit()
        {
            EmployeePermits = new HashSet<EmployeePermit>();
        }

        public int DriverPermitID { get; set; }

        [Required]
        [StringLength(20)]
        public string Description { get; set; }

        public int? RenewalCycle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePermit> EmployeePermits { get; set; }
    }
}
