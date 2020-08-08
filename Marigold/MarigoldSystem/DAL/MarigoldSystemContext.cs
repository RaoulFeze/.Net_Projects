
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
            : base("name=COE_Db")
        {
        }

        public virtual DbSet<Community> Communities { get; set; }
        public virtual DbSet<CorrectiveAction> CorrectiveActions { get; set; }
        public virtual DbSet<Crew> Crews { get; set; }
        public virtual DbSet<CrewMember> CrewMembers { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Employee_Audit_Trail> Employee_Audit_Trail { get; set; }
        public virtual DbSet<EmployeeLicense> EmployeeLicenses { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public virtual DbSet<EmployeeStanding> EmployeeStandings { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public virtual DbSet<Hazard> Hazards { get; set; }
        public virtual DbSet<HazardCategory> HazardCategories { get; set; }
        public virtual DbSet<JobCard> JobCards { get; set; }
        public virtual DbSet<LicenseClass> LicenseClasses { get; set; }
        public virtual DbSet<OperatorPermit> OperatorPermits { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<Site_Audit_Trail> Site_Audit_Trail { get; set; }
        public virtual DbSet<SiteHazard> SiteHazards { get; set; }
        public virtual DbSet<SiteType> SiteTypes { get; set; }
        public virtual DbSet<Standing> Standings { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Tool> Tools { get; set; }
        public virtual DbSet<ToolsChecklist> ToolsChecklists { get; set; }
        public virtual DbSet<TrailerOperator> TrailerOperators { get; set; }
        public virtual DbSet<Truck> Trucks { get; set; }
        public virtual DbSet<TruckCategory> TruckCategories { get; set; }
        public virtual DbSet<TruckLicense> TruckLicenses { get; set; }
        public virtual DbSet<Yard> Yards { get; set; }
        public virtual DbSet<YardEmployee> YardEmployees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Community>()
                .Property(e => e.Name)
                .IsUnicode(false);

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
                .HasMany(e => e.EmployeeLicenses)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeRoles)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeStandings)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OperatorPermits)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Site_Audit_Trail)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ChangedByEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.YardEmployees)
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

            modelBuilder.Entity<Equipment>()
                .Property(e => e.EquipmentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EquipmentCategory>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EquipmentCategory>()
                .HasMany(e => e.OperatorPermits)
                .WithRequired(e => e.EquipmentCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hazard>()
                .Property(e => e.HazardDescription)
                .IsUnicode(false);

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

            modelBuilder.Entity<LicenseClass>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<LicenseClass>()
                .HasMany(e => e.EmployeeLicenses)
                .WithRequired(e => e.LicenseClass)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LicenseClass>()
                .HasMany(e => e.TruckLicenses)
                .WithRequired(e => e.LicenseClass)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.EmployeeRoles)
                .WithRequired(e => e.Role)
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

            modelBuilder.Entity<Standing>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Standing>()
                .HasMany(e => e.EmployeeStandings)
                .WithRequired(e => e.Standing)
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

            modelBuilder.Entity<Truck>()
                .Property(e => e.TruckNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Truck>()
                .Property(e => e.TruckDescription)
                .IsUnicode(false);

            modelBuilder.Entity<TruckCategory>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TruckCategory>()
                .HasMany(e => e.TruckLicenses)
                .WithRequired(e => e.TruckCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yard>()
                .Property(e => e.YardName)
                .IsUnicode(false);

            modelBuilder.Entity<Yard>()
                .HasMany(e => e.YardEmployees)
                .WithRequired(e => e.Yard)
                .WillCascadeOnDelete(false);
        }
    }
}
