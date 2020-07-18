using MarigoldSystem.DAL;
using MarigoldSystem.Data.Entities;
using MarigoldSystem.Data.POCO_s;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
        public List<Truck> GetTrucks(int yardId)
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
        public List<Driver> GetTruckDrivers(int yardId, int unitId, int type)
        {
            using (var context = new MarigoldSystemContext())
            {
                int? categoryId = 0;
                switch (type)
                {
                    case 1:
                        categoryId = (from equipment in context.Equipments
                                      where equipment.EquipmentID == unitId
                                      select equipment.CategoryID).First();

                       var Operators = (from operators in context.YardEmployees
                                        join permit in context.OperatorPermits
                                        on operators.EmployeeID equals permit.EmployeeID
                                        where operators.YardID == yardId && permit.CategoryID == categoryId
                                        select new Driver
                                        {
                                            EmployeeID = operators.EmployeeID,
                                            Name = operators.Employee.FirstName + " " + operators.Employee.LastName,
                                            Phone = operators.Employee.Phone,
                                            Trailer = (context.TrailerOperators.Where(x => x.EmployeeID == operators.EmployeeID).Select(x => x)).FirstOrDefault().Equals(null) ? false : true
                                        }).ToList();
                        return Operators;

                    case 2:
                        List<Driver> Drivers = new List<Driver>();
                        categoryId = (from truck in context.Trucks
                                      where truck.TruckID == unitId
                                      select truck.CategoryID).FirstOrDefault();

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
                                          select new Driver
                                          {
                                              EmployeeID = yardemp.Employee.EmployeeID,
                                              Name = yardemp.Employee.FirstName + " " + yardemp.Employee.LastName,
                                              Phone = yardemp.Employee.Phone,
                                              License = license.LicenseClass.Description,
                                              Trailer = (context.TrailerOperators.Where(x => x.EmployeeID == yardemp.EmployeeID).Select(x => x)).FirstOrDefault().Equals(null) ? false : true
                                          }).ToList();
                            Drivers.AddRange(result);
                        }
                        return Drivers;
                    default:
                        return null;
                }
            }

        }
        
        public bool FoundUnit(int unitId, string category)
        {
            using(var context = new MarigoldSystemContext())
            {
                switch (category)
                {
                    case "Equipments":
                        Crew crew = context.Crews
                                                .Where(x => DbFunctions.TruncateTime(x.CrewDate) == DbFunctions.TruncateTime(DateTime.Today) && x.EquipmentID == unitId)
                                                .Select(x => x)
                                                .FirstOrDefault();
                        if (crew != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        //break;
                    case "Trucks":
                        Crew crews = context.Crews
                                                .Where(x => DbFunctions.TruncateTime(x.CrewDate) == DbFunctions.TruncateTime(DateTime.Today) && x.TruckID == unitId)
                                                .Select(x => x)
                                                .FirstOrDefault();
                        if (crews != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        //break;
                    default:
                        return false;
                }
            }
        }

        public string GetUnitDescription(int crewId)
        {
            using (var context = new MarigoldSystemContext())
            {
                if(context.Crews.Find(crewId).EquipmentID != null)
                {
                    return context.Crews.Find(crewId).Equipment.Description;
                }
                else
                {
                    return context.Crews.Find(crewId).Truck.TruckDescription;
                }
            }
        }
    }
}
