namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Equipment")]
    public partial class Equipment
    {
        public int EquipmentID { get; set; }

        public int? CategoryID { get; set; }

        public int? YardID { get; set; }

        [Required]
        [StringLength(20)]
        public string EquipmentNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string Description { get; set; }

        public virtual EquipmentCategory EquipmentCategory { get; set; }

        public virtual Yard Yard { get; set; }
    }
}
