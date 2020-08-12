namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Crew")]
    public partial class Crew
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Crew()
        {
            CrewMembers = new HashSet<CrewMember>();
            JobCardCrews = new HashSet<JobCardCrew>();
            ToolsChecklists = new HashSet<ToolsChecklist>();
        }

        public int CrewID { get; set; }

        public DateTime CrewDate { get; set; }

        public int? TruckID { get; set; }

        public int? EquipmentID { get; set; }

        public bool? FLHA_CompletedBy { get; set; }

        public int? KM_Start { get; set; }

        public int? KM_End { get; set; }

        [StringLength(100)]
        public string AdditionalComments { get; set; }

        public virtual Equipment Equipment { get; set; }

        public virtual Truck Truck { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CrewMember> CrewMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobCardCrew> JobCardCrews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolsChecklist> ToolsChecklists { get; set; }
    }
}
