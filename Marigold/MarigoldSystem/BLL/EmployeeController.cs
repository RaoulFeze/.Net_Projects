using MarigoldSystem.DAL;
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
    }
}   
