using MarigoldSystem.DAL;
using MarigoldSystem.Data.POCO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarigoldSystem.BLL
{
    public class EmployeeController
    {
        public int? GetYardID(int userId)
        {
            using(var context = new MarigoldSystemContext())
            {
                return context.YardEmployees
                                        .Where(x => x.EmployeeID == userId)
                                        .OrderBy(x => x.AssignedDate)
                                        .Select(x => x.YardID)
                                        .AsEnumerable()
                                        .Last();
                
            }
        }

        public string GetUserRole(int userId)
        {
            using(var context = new MarigoldSystemContext())
            {
                return context.EmployeeRoles
                              .Where(x => x.EmployeeID == userId)
                              .Select(x => x.Role.EmployeeRoles)
                              .ToString();
            }
        }

        public List<Driver> GetEmployees(int yardId)
        {
            using (var context = new MarigoldSystemContext())
            {
                var employees = (from yard in context.YardEmployees
                                 where yard.YardID == yardId
                                 select new Driver
                                 {
                                     EmployeeID = yard.Employee.EmployeeID,
                                     Name = yard.Employee.FirstName + " " + yard.Employee.LastName,
                                     Phone = yard.Employee.Phone
                                 }).ToList();
                return employees;
            }
        }
    }
}   
