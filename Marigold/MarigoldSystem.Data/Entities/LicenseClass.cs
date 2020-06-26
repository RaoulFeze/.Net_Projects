namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LicenseClass")]
    public partial class LicenseClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LicenseClass()
        {
            EmployeeLicenses = new HashSet<EmployeeLicense>();
            TruckLicenses = new HashSet<TruckLicense>();
        }

        public int LicenseClassID { get; set; }

        [Required]
        [StringLength(20)]
        public string Description { get; set; }

        public int? RenewalCycle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeLicense> EmployeeLicenses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TruckLicense> TruckLicenses { get; set; }
    }
}
