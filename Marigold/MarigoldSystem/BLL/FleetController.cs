using MarigoldSystem.DAL;
using MarigoldSystem.Data.Entities;
using MarigoldSystem.Data.POCO_s;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MarigoldSystem.BLL
{
    [DataObject]
    public class FleetController
    {
        public List<Truck> GetUnits(int yardId)
        {
            using(var context = new MarigoldSystemContext())
            {
                var units = context.Trucks
                                        .Where(x => x.YardID == yardId)
                                        .Select(x => x).ToList();
                return units;
            }
        }

        public List<Equipment> GetEquipments(int yardId)
        {
            using(var context = new MarigoldSystemContext())
            {
                var equipmemts = context.Equipments
                                                .Where(x => x.YardID == yardId)
                                                .Select(x => x).ToList();
                return equipmemts;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<TruckDriver> GetTruckDrivers(int yardId, int unitId)
        {
            using (var context = new MarigoldSystemContext())
            {
                int? categoryId = 0;
                List<TruckDriver> Drivers = new List<TruckDriver>();
                categoryId = (from x in context.Trucks
                             where x.TruckID == unitId
                             select x.CategoryID).FirstOrDefault();

                //retrieve all licenses allowed to drive a given Unit
                List<int> allLicenses = context.TruckLicenses
                                                    .Where(x => x.CategoryID == categoryId)
                                                    .Select(x => x.LicenseClass.LicenseClassID)
                                                    .ToList();

                //Retrieves all drivers allowed to drive a given Truck 
                foreach (int id in allLicenses)
                {
                    var result = (from yardemp in context.YardEmployees
                                  join license in context.EmployeeLicenses
                                  on yardemp.EmployeeID equals license.EmployeeID
                                  where license.LicenseClassID == id && yardemp.YardID == yardId
                                  orderby yardemp.Employee.FirstName
                                  select new TruckDriver
                                  {
                                      Name = yardemp.Employee.FirstName + " " + yardemp.Employee.LastName,
                                      Phone = yardemp.Employee.Phone,
                                      License = license.LicenseClass.Description,
                                      Trailer = (context.TrailerOperators.Where(x => x.EmployeeID == yardemp.EmployeeID).Select(x => x)).FirstOrDefault().Equals(null) ? false : true
                                  }).ToList();
                    Drivers.AddRange(result);
                }
                return Drivers;
            }

        }
    }
}
