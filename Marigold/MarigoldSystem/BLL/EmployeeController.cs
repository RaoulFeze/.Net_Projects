using MarigoldSystem.DAL;
using MarigoldSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarigoldSystem.BLL
{
    public class EmployeeController
    {
        public string GetUserRole(int? userId)
        {
            using(var context = new MarigoldSystemContext())
            {
                return context.Employees.Find(userId).EmployeeRole.Description;
            }
        }
    }
}
