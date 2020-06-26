namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CorrectiveAction")]
    public partial class CorrectiveAction
    {
        public int CorrectiveActionID { get; set; }

        public int? HazardID { get; set; }

        [Required]
        [StringLength(500)]
        public string CorrectiveActionDescription { get; set; }

        public virtual Hazard Hazard { get; set; }
    }
}
