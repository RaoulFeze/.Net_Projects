namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hazard")]
    public partial class Hazard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hazard()
        {
            CorrectiveActions = new HashSet<CorrectiveAction>();
            SiteHazards = new HashSet<SiteHazard>();
        }

        public int HazardID { get; set; }

        public int? HazardCategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string HazardDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CorrectiveAction> CorrectiveActions { get; set; }

        public virtual HazardCategory HazardCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiteHazard> SiteHazards { get; set; }
    }
}
