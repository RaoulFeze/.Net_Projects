namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OperatorPermit")]
    public partial class OperatorPermit
    {
        public int OperatorPermitID { get; set; }

        public int EmployeeID { get; set; }

        public int EquipmentID { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime? RenewalDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
