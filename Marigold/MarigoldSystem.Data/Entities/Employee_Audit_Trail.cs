namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee_Audit_Trail
    {
        [Key]
        public int AuditTrailID { get; set; }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(10)]
        public string ColumnAffected { get; set; }

        public int ChangedBy { get; set; }

        public DateTime ChangedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string OldValue { get; set; }

        [Required]
        [StringLength(50)]
        public string NewValue { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
