namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Site")]
    public partial class Site
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Site()
        {
            JobCards = new HashSet<JobCard>();
            Site_Audit_Trail = new HashSet<Site_Audit_Trail>();
        }

        public int SiteID { get; set; }

        public int? SiteTypeID { get; set; }

        public int? YardID { get; set; }

        public int? CommunityID { get; set; }

        public int Pin { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(35)]
        public string StreetAddress { get; set; }

        public int Area { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        public int? Grass { get; set; }

        public bool? Watering { get; set; }

        public bool? Planting { get; set; }

        public virtual Community Community { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobCard> JobCards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Site_Audit_Trail> Site_Audit_Trail { get; set; }

        public virtual SiteType SiteType { get; set; }

        public virtual Yard Yard { get; set; }
    }
}
