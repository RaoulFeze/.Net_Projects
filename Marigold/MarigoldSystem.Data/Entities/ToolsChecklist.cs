namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ToolsChecklist")]
    public partial class ToolsChecklist
    {
        [Key]
        public int ToolCheckListID { get; set; }

        public int ToolID { get; set; }

        public int CrewID { get; set; }

        public int? Quantity { get; set; }

        public virtual Crew Crew { get; set; }

        public virtual Tool Tool { get; set; }
    }
}
