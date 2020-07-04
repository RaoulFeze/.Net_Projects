
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using MarigoldSystem.DAL;
using MarigoldSystem.Data.Entities;
using System.Data.Entity;
using COECommon.UserControls;
using MarigoldSystem.Data.DTO_s;
using MarigoldSystem.Data.POCO_s;
using System.ComponentModel;
#endregion

namespace MarigoldSystem.BLL
{
    [DataObject]
    public class CrewController
    {
        //This method Creates a Crew 
        public void CreateCrew(int unitId, int driverId)
        {
            using(var context = new MarigoldSystemContext())
            {
                Crew crew = (context.Crews
                                        .Where(x => x.TruckID == unitId && DbFunctions.TruncateTime(x.CrewDate) == DbFunctions.TruncateTime(DateTime.Today))
                                        .Select(x => x))
                                        .FirstOrDefault();

            // Check if the driver is already assigned in a different crew
                List<int> CrewMemberIDs = (context.CrewMembers
                                                        .Where(x => DbFunctions.TruncateTime(x.Crew.CrewDate) == DbFunctions.TruncateTime(DateTime.Today))
                                                        .Select(x => x.EmployeeID))
                                                        .ToList();

                if(CrewMemberIDs != null)
                {
                    foreach(int id in CrewMemberIDs)
                    {
                        if(id == driverId)
                        {
                            string name = context.Employees
                                                        .Where(x => x.EmployeeID == driverId)
                                                        .Select(x => x.FirstName + " " + x.LastName)
                                                        .FirstOrDefault();
                            throw new Exception (name +" is already assigned to a different crew");
                        }
                        
                    }
                }
            //End check

                if (crew == null)
                {
                    //Create a new Crew
                    crew = new Crew();
                    crew.TruckID = unitId;
                    crew.CrewDate = DateTime.Today;
                    context.Crews.Add(crew);

                    //Add the dirver as the first Crew Member
                    CrewMember member = new CrewMember();
                    member.EmployeeID = driverId;
                    member.Driver = true;
                    crew.CrewMembers.Add(member);
                }
                else
                {
                    throw new Exception("The selected truck is already assigned to a different Crew");
                }
                context.SaveChanges();
            }
        }

        //This Method adds a Crew Member to a given Crew
        public void AddCrewMember(int crewId, int memberId)
        {
            using(var context = new MarigoldSystemContext())
            {
                CrewMember newCrewMember = new CrewMember();
                // Check if the new crew member is  already assigned in a different crew
                List<int> CrewMemberIDs = (context.CrewMembers
                                                        .Where(x => DbFunctions.TruncateTime(x.Crew.CrewDate) == DbFunctions.TruncateTime(DateTime.Now))
                                                        .Select(x => x.EmployeeID))
                                                        .ToList();
                int count = context.Crews.Find(crewId).CrewMembers.Count();
                if(count == 5)
                {
                    throw new Exception("A crew cannot have more than five (5) crew members");
                }

                if (CrewMemberIDs != null)
                {
                    foreach (int id in CrewMemberIDs)
                    {
                        if (id == memberId)
                        {
                            string name = context.Employees
                                                        .Where(x => x.EmployeeID == memberId)
                                                        .Select(x => x.FirstName + " " + x.LastName)
                                                        .FirstOrDefault();
                            throw new Exception(name + " is already assigned to a different crew");
                        }

                    }
                }
                //End check
                newCrewMember.EmployeeID = memberId;
                context.Crews.Find(crewId).CrewMembers.Add(newCrewMember);
                context.SaveChanges();
            }
        }

        //This method retrieves all Current Crews
        public List<CurrentCrews> GetCurrentCrews(int yardId)
        {
            using (var context = new MarigoldSystemContext())
            {
                var crews = (from crew in context.Crews
                             where crew.Truck.YardID == yardId && DbFunctions.TruncateTime(crew.CrewDate) == DbFunctions.TruncateTime(DateTime.Today)
                             orderby crew.CrewID descending
                             select new CurrentCrews
                             {
                                 CrewID = crew.CrewID,
                                 Description = crew.Truck.TruckDescription,
                                 Crew = (from member in context.CrewMembers
                                         where member.CrewID == crew.CrewID
                                         orderby member.Employee.FirstName ascending
                                         select new Member
                                         {
                                             CrewMemberId = member.CrewMemberID,
                                             Name = member.Employee.FirstName + " " + member.Employee.LastName.Substring(0, 1) + "." + " " + (member.Driver == true ? "(D)" : " "),
                                             Phone = member.Employee.Phone
                                         }).ToList(),
                                 JobCards = (from job in context.JobCards
                                             where job.CrewID == crew.CrewID
                                             orderby job.Site.Pin ascending
                                             select new Job
                                             {
                                                 JobCardID = job.JobCardID,
                                                 Pin = job.Site.Pin,
                                                 Address = job.Site.StreetAddress
                                             }).ToList()
                             }).ToList();
                return crews;
            }
        }

        //This Method Remove a Crew Member from a given Crew
        public void RemoveCrewMember(int memberId, int crewId)
        {
            using(var context = new MarigoldSystemContext())
            {
                CrewMember member = context.CrewMembers.Find(memberId);
                context.CrewMembers.Remove(member);
                context.SaveChanges();
            }
        }
    }
}
