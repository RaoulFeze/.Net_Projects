using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MarigoldSystem.Data.Entities;

namespace MarigoldSystem.DAL
{
   
    public partial class MarigoldSystemContext : DbContext
    {
        public MarigoldSystemContext()
            : base("name=COE_DB")
        {
        }

        public virtual DbSet<Community> Communities { get; set; }
        public virtual DbSet<CorrectiveAction> CorrectiveActions { get; set; }
        public virtual DbSet<Crew> Crews { get; set; }
        public virtual DbSet<CrewMember> CrewMembers { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<DriverPermit> DriverPermits { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Employee_Audit_Trail> Employee_Audit_Trail { get; set; }
        public virtual DbSet<EmployeePermit> EmployeePermits { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public virtual DbSet<EmployeeStatu> EmployeeStatus { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Hazard> Hazards { get; set; }
        public virtual DbSet<HazardCategory> HazardCategories { get; set; }
        public virtual DbSet<JobCard> JobCards { get; set; }
        public virtual DbSet<OperatorPermit> OperatorPermits { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<Site_Audit_Trail> Site_Audit_Trail { get; set; }
        public virtual DbSet<SiteHazard> SiteHazards { get; set; }
        public virtual DbSet<SiteType> SiteTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Tool> Tools { get; set; }
        public virtual DbSet<ToolsChecklist> ToolsChecklists { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Yard> Yards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Community>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Community>()
                .HasMany(e => e.Sites)
                .WithRequired(e => e.Community)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CorrectiveAction>()
                .Property(e => e.CorrectiveActionDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Crew>()
                .Property(e => e.AdditionalComments)
                .IsUnicode(false);

            modelBuilder.Entity<Crew>()
                .HasMany(e => e.CrewMembers)
                .WithRequired(e => e.Crew)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Crew>()
                .HasMany(e => e.JobCards)
                .WithRequired(e => e.Crew)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Crew>()
                .HasMany(e => e.ToolsChecklists)
                .WithRequired(e => e.Crew)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<District>()
                .Property(e => e.DistrictName)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Yards)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DriverPermit>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<DriverPermit>()
                .HasMany(e => e.EmployeePermits)
                .WithRequired(e => e.DriverPermit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CrewMembers)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Employee_Audit_Trail)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeePermits)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OperatorPermits)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee_Audit_Trail>()
                .Property(e => e.ColumnAffected)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Audit_Trail>()
                .Property(e => e.OldValue)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Audit_Trail>()
                .Property(e => e.NewValue)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeRole>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeRole>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.EmployeeRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeStatu>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeStatu>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.EmployeeStatu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.OperatorPermits)
                .WithRequired(e => e.Equipment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hazard>()
                .Property(e => e.HazardDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Hazard>()
                .HasMany(e => e.CorrectiveActions)
                .WithRequired(e => e.Hazard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hazard>()
                .HasMany(e => e.SiteHazards)
                .WithRequired(e => e.Hazard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HazardCategory>()
                .Property(e => e.HazardCategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<JobCard>()
                .Property(e => e.ActionRequired)
                .IsUnicode(false);

            modelBuilder.Entity<JobCard>()
                .HasMany(e => e.SiteHazards)
                .WithRequired(e => e.JobCard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.StreetAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.JobCards)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.Site_Audit_Trail)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site_Audit_Trail>()
                .Property(e => e.ColumnAffected)
                .IsUnicode(false);

            modelBuilder.Entity<Site_Audit_Trail>()
                .Property(e => e.OldValue)
                .IsUnicode(false);

            modelBuilder.Entity<Site_Audit_Trail>()
                .Property(e => e.NewValue)
                .IsUnicode(false);

            modelBuilder.Entity<SiteType>()
                .Property(e => e.SiteTypeDescription)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SiteType>()
                .HasMany(e => e.Sites)
                .WithRequired(e => e.SiteType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.JobCards)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tool>()
                .Property(e => e.ToolDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Tool>()
                .HasMany(e => e.ToolsChecklists)
                .WithRequired(e => e.Tool)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.UnitNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.UnitDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Crews)
                .WithRequired(e => e.Unit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yard>()
                .Property(e => e.YardName)
                .IsUnicode(false);

            modelBuilder.Entity<Yard>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Yard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yard>()
                .HasMany(e => e.Sites)
                .WithRequired(e => e.Yard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yard>()
                .HasMany(e => e.Tools)
                .WithRequired(e => e.Yard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yard>()
                .HasMany(e => e.Units)
                .WithRequired(e => e.Yard)
                .WillCascadeOnDelete(false);
        }
    }
}
