namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrailerOperator")]
    public partial class TrailerOperator
    {
        [Key]
        public int OperatorID { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? TrainedDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
