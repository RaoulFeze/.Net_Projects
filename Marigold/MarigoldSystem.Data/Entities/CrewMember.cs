namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CrewMember")]
    public partial class CrewMember
    {
        public int CrewMemberID { get; set; }

        public int EmployeeID { get; set; }

        public int CrewID { get; set; }

        public bool? Driver { get; set; }

        public virtual Crew Crew { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
