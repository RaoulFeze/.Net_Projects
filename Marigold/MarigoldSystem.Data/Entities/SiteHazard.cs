namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiteHazard")]
    public partial class SiteHazard
    {
        public int SiteHazardID { get; set; }

        public int HazardID { get; set; }

        public int JobCardID { get; set; }

        public int? ReviewedBy { get; set; }

        public DateTime? ReviewedDate { get; set; }

        public virtual Hazard Hazard { get; set; }

        public virtual JobCard JobCard { get; set; }
    }
}
