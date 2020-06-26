namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Site_Audit_Trail
    {
        [Key]
        public int AuditTrailID { get; set; }

        public int SiteID { get; set; }

        [Required]
        [StringLength(50)]
        public string ColumnAffected { get; set; }

        public int ChangedBy { get; set; }

        public DateTime ChangedDate { get; set; }

        [StringLength(100)]
        public string OldValue { get; set; }

        [StringLength(100)]
        public string NewValue { get; set; }

        public virtual Site Site { get; set; }
    }
}
