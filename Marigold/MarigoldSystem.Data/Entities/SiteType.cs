namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiteType")]
    public partial class SiteType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SiteType()
        {
            Sites = new HashSet<Site>();
        }

        public int SiteTypeID { get; set; }

        [Required]
        [StringLength(1)]
        public string SiteTypeDescription { get; set; }

        public int NumberOfCyle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Site> Sites { get; set; }
    }
}
