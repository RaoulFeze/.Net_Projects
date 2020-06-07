namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeePermit")]
    public partial class EmployeePermit
    {
        public int EmployeePermitID { get; set; }

        public int EmployeeID { get; set; }

        public int DriverPermitID { get; set; }

        public DateTime CompletionDate { get; set; }

        public DateTime? ExpiringDate { get; set; }

        public virtual DriverPermit DriverPermit { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
