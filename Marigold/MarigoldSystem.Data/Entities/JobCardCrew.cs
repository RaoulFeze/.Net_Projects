namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JobCardCrew")]
    public partial class JobCardCrew
    {
        public int JobCardCrewID { get; set; }

        public int CrewID { get; set; }

        public int JobCardID { get; set; }

        public virtual Crew Crew { get; set; }

        public virtual JobCard JobCard { get; set; }
    }
}
