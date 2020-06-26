namespace MarigoldSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TruckLicense")]
    public partial class TruckLicense
    {
        public int TruckLicenseID { get; set; }

        public int CategoryID { get; set; }

        public int LicenseClassID { get; set; }

        public virtual LicenseClass LicenseClass { get; set; }

        public virtual TruckCategory TruckCategory { get; set; }
    }
}
