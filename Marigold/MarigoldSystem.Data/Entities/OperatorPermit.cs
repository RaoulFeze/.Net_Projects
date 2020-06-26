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
        [Key]
        public int PermitID { get; set; }

        public int EmployeeID { get; set; }

        public int CategoryID { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime? RenewalDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EquipmentCategory EquipmentCategory { get; set; }
    }
}
