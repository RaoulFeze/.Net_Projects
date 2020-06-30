
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
#endregion

namespace MarigoldSystem.BLL
{
    public class CrewController
    {
        //This method create and 
        public void AddCrew(int unitId, int driverId, List<int> memberIds)
        {
            using(var context = new MarigoldSystemContext())
            {
                Crew crew = (context.Crews
                                        .Where(x => x.TruckID == unitId && DbFunctions.TruncateTime(x.CrewDate) == DbFunctions.TruncateTime(DateTime.Now))
                                        .Select(x => x))
                                        .FirstOrDefault();

                List<string> reasons = new List<string>();

            # region Check the new crew members are not already assigned in a different crew
                List<int> crewIds = (context.Crews
                                             .Where(x => x.TruckID == unitId && DbFunctions.TruncateTime(x.CrewDate) == DbFunctions.TruncateTime(DateTime.Now))
                                              .Select(x => x.CrewID))
                                              .ToList();
                var Ids = memberIds;
                    Ids.Add(driverId);

                if(crewIds != null)
                {
                    foreach(int id in crewIds)
                    {
                        foreach(int memid in Ids)
                        {
                            if((context.CrewMembers.Where(x=>x.CrewID == id && x.CrewMemberID == memid).Select(x=>x)) != null)
                            {
                                string name = context.Employees
                                                            .Where(x => x.EmployeeID == memid)
                                                            .Select(x => x.FirstName + "" + x.LastName)
                                                            .FirstOrDefault();
                                reasons.Add(name +" is already assigned to a different crew");
                            }
                        }
                    }
                }
                #endregion

                if (reasons.Count() == 0)
                {
                    if (crew == null)
                    {
                        //Create a new Crew
                        crew = new Crew();
                        crew.TruckID = unitId;
                        crew.CrewDate = DateTime.Now;
                        context.Crews.Add(crew);

                        if(memberIds.Count() > 5)
                        {
                            throw new Exception("The number crew member exceeds five (5). Please remove " + (memberIds.Count() - 5) + " Crew members");
                        }
                        else
                        {
                            bool findkDriver = false;
                            foreach(int id in memberIds)
                            {
                                if(id == driverId)
                                {
                                    findkDriver = true;
                                }
                            }

                            if (findkDriver)
                            {
                                string name = context.Employees
                                                            .Where(x => x.EmployeeID == driverId)
                                                            .Select(x => x.FirstName + "" + x.LastName)
                                                            .FirstOrDefault();
                                throw new Exception(name + " is assigned both as driver and as a different Crew Member. Please select another staff member to be in the crew");
                            }
                            else
                            {
                                //Add Crew Members
                                CrewMember member = new CrewMember();
                                member.EmployeeID = driverId;
                                member.Driver = true;
                                crew.CrewMembers.Add(member);

                                foreach (int id in memberIds)
                                {
                                    member.EmployeeID = id;
                                    crew.CrewMembers.Add(member);
                                }
                            }
                           
                        }
                        
                    }
                    else
                    {
                        int count = (context.CrewMembers
                                                    .Where(x => x.CrewID == crew.CrewID)
                                                    .Select(x => x))
                                                    .Count();
                        if (count + memberIds.Count() > 5)
                        {
                            throw new Exception("The number crew member exceeds five (5). Please remove " + (count + memberIds.Count() - 5) + " Crew members");
                        }
                        else
                        {
                            foreach (int id in memberIds)
                            {
                                CrewMember member = new CrewMember();
                                member.EmployeeID = id;
                                crew.CrewMembers.Add(member);
                            }
                        }

                    }
                }
                else
                {
                    throw new BusinessRuleException("Create Crew", reasons);
                }

                context.SaveChanges();
            }
        }
    }
}
