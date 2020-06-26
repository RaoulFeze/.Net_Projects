namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YardEmployee")]
    public partial class YardEmployee
    {
        [Key]
        public int YardEmeployeeID { get; set; }

        public int EmployeeID { get; set; }

        public int YardID { get; set; }

        public DateTime AssignedDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Yard Yard { get; set; }
    }
}
