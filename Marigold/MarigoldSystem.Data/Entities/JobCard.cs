namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JobCard")]
    public partial class JobCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobCard()
        {
            JobCardCrews = new HashSet<JobCardCrew>();
            SiteHazards = new HashSet<SiteHazard>();
        }

        public int JobCardID { get; set; }

        public int SiteID { get; set; }

        public int TaskID { get; set; }

        public TimeSpan? TimeOnSite { get; set; }

        public TimeSpan? TimeOffSite { get; set; }

        [StringLength(100)]
        public string ActionRequired { get; set; }

        public DateTime? ClosedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobCardCrew> JobCardCrews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiteHazard> SiteHazards { get; set; }

        public virtual Site Site { get; set; }

        public virtual Task Task { get; set; }
    }
}
