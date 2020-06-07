namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            CrewMembers = new HashSet<CrewMember>();
            Employee_Audit_Trail = new HashSet<Employee_Audit_Trail>();
            EmployeePermits = new HashSet<EmployeePermit>();
            OperatorPermits = new HashSet<OperatorPermit>();
        }

        public int EmployeeID { get; set; }

        public int YardID { get; set; }

        public int RoleID { get; set; }

        public int StatusID { get; set; }

        public int PayrollNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(40)]
        public string Email { get; set; }

        [StringLength(13)]
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CrewMember> CrewMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee_Audit_Trail> Employee_Audit_Trail { get; set; }

        public virtual EmployeeRole EmployeeRole { get; set; }

        public virtual EmployeeStatu EmployeeStatu { get; set; }

        public virtual Yard Yard { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePermit> EmployeePermits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperatorPermit> OperatorPermits { get; set; }
    }
}
