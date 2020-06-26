namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tool")]
    public partial class Tool
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tool()
        {
            ToolsChecklists = new HashSet<ToolsChecklist>();
        }

        public int ToolID { get; set; }

        public int? YardID { get; set; }

        [Required]
        [StringLength(30)]
        public string ToolDescription { get; set; }

        public virtual Yard Yard { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolsChecklist> ToolsChecklists { get; set; }
    }
}
