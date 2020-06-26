namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeLicense")]
    public partial class EmployeeLicense
    {
        [Key]
        public int LicenseID { get; set; }

        public int EmployeeID { get; set; }

        public int LicenseClassID { get; set; }

        public DateTime? RenewalDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual LicenseClass LicenseClass { get; set; }
    }
}
