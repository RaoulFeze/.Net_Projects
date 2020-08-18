
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
        public void CreateCrew(int unitId, int driverId, string category)
        {
            using (var context = new MarigoldSystemContext())
            {
                // Check if the driver is already assigned in a different crew
                List<int> CrewMemberIDs = (context.CrewMembers
                                                        .Where(x => DbFunctions.TruncateTime(x.Crew.CrewDate) == DbFunctions.TruncateTime(DateTime.Today))
                                                        .Select(x => x.EmployeeID))
                                                        .ToList();

                if (CrewMemberIDs != null)
                {
                    foreach (int id in CrewMemberIDs)
                    {
                        if (id == driverId)
                        {
                            string name = context.Employees
                                                        .Where(x => x.EmployeeID == driverId)
                                                        .Select(x => x.FirstName + " " + x.LastName)
                                                        .FirstOrDefault();
                            throw new Exception(name + " is already assigned to a different crew");
                        }

                    }
                }
                //End check

                Crew crew = new Crew();
                switch (category)
                {
                    case "Equipments":
                        crew = (context.Crews
                                               .Where(x => x.EquipmentID == unitId && DbFunctions.TruncateTime(x.CrewDate) == DbFunctions.TruncateTime(DateTime.Today))
                                               .Select(x => x))
                                               .FirstOrDefault();
                        break;
                    case "Trucks":
                        crew = (context.Crews
                                               .Where(x => x.TruckID == unitId && DbFunctions.TruncateTime(x.CrewDate) == DbFunctions.TruncateTime(DateTime.Today))
                                               .Select(x => x))
                                               .FirstOrDefault();
                        break;
                }


                if (crew == null)
                {
                    //Create a new Crew
                    crew = new Crew();
                    if (category == "Equipments")
                    {
                        crew.EquipmentID = unitId;
                    }
                    else if (category == "Trucks")
                    {
                        crew.TruckID = unitId;
                    }
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
                    throw new Exception("The selected Unit is already assigned to a different Crew");
                }
                context.SaveChanges();
            }
        }

        //This Method adds a Crew Member to a given Crew
        public void AddCrewMember(int crewId, int memberId)
        {
            using (var context = new MarigoldSystemContext())
            {
                CrewMember newCrewMember = new CrewMember();
                // Check if the new crew member is  already assigned in a different crew
                List<int> CrewMemberIDs = (context.CrewMembers
                                                        .Where(x => DbFunctions.TruncateTime(x.Crew.CrewDate) == DbFunctions.TruncateTime(DateTime.Now))
                                                        .Select(x => x.EmployeeID))
                                                        .ToList();
                int count = context.Crews.Find(crewId).CrewMembers.Count();
                if (count == 5)
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
                             where (((crew.Truck.YardID == yardId || crew.Equipment.YardID == yardId) && DbFunctions.TruncateTime(crew.CrewDate) == DbFunctions.TruncateTime(DateTime.Today)) && (crew.JobCardCrews
                                                                                                                                                                                                            .Where(y => y.JobCard.ClosedDate == null)
                                                                                                                                                                                                            .Select(y => y))
                                                                                                                                                                                                            .Count() > 0 || crew.JobCardCrews.Count() == 0)
                             orderby crew.CrewID descending
                             select new CurrentCrews
                             {
                                 CrewID = crew.CrewID,
                                 Description = crew.Truck.TruckID.Equals(null) ? crew.Equipment.Description : crew.Truck.TruckDescription,
                                 Crew = (from member in context.CrewMembers
                                         where member.CrewID == crew.CrewID
                                         orderby member.Employee.FirstName ascending
                                         select new Member
                                         {
                                             CrewMemberId = member.CrewMemberID,
                                             Name = member.Employee.FirstName + " " + member.Employee.LastName.Substring(0, 1) + "." + " " + (member.Driver == true ? "(D)" : " "),
                                             Phone = member.Employee.Phone
                                         }).ToList(),
                                 CardCrew = (from job in context.JobCardCrews
                                             where job.CrewID == crew.CrewID && job.JobCard.ClosedDate == null
                                             orderby job.JobCard.Site.Pin
                                             select new Job
                                             {
                                                 JobCardCrewID = job.JobCardCrewID,
                                                 Pin = job.JobCard.Site.Pin,
                                                 Address = job.JobCard.Site.StreetAddress
                                             }).ToList()
                             }).ToList();
                return crews;
            }
        }

        //This Method Remove a Crew Member from a given Crew
        public void RemoveCrewMember(int memberId, int crewId)
        {
            using (var context = new MarigoldSystemContext())
            {
                CrewMember member = context.CrewMembers.Find(memberId);
                context.CrewMembers.Remove(member);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method deletes a crew
        ///     It deletes all the Crew members associated to the crew
        ///     It deletes all the JobCards assigned to the Crew
        ///     It deletes the crew table
        /// </summary>
        /// <param name="crewId"></param>
        public void DeleteCrew(int crewId)
        {
            using (var context = new MarigoldSystemContext())
            {
                Crew crew = context.Crews.Find(crewId);
                if (crew == null)
                {
                    throw new Exception("This Crew  no longer exists");
                }
                else
                {
                    List<CrewMember> crewMembers = crew.CrewMembers.Select(x => x).ToList();
                    List<JobCardCrew> jobCardCrews = crew.JobCardCrews.Select(x => x).ToList();
                    if (crewMembers.Count() > 0)
                    {
                        foreach (CrewMember cm in crewMembers)
                        {
                            context.CrewMembers.Remove(cm);
                        }
                    }

                    if (jobCardCrews.Count() > 0)
                    {
                        foreach (JobCardCrew jobCardCrew in jobCardCrews)
                        {
                            int jobCardId = jobCardCrew.JobCardID;
                            int count = context.JobCardCrews
                                                        .Where(x => x.JobCardID == jobCardId)
                                                        .Select(x => x)
                                                        .Count();
                            if (count > 1)
                            {
                                context.JobCardCrews.Remove(jobCardCrew);
                            }
                            else
                            {
                                JobCard jobCard = context.JobCards.Find(jobCardId);
                                context.JobCardCrews.Remove(jobCardCrew);
                                context.JobCards.Remove(jobCard);
                            }
                        }
                    }
                }
                context.Crews.Remove(crew);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method add a JobCard to a crew
        ///     It verifies the same site is not assigned twice to the same crew
        ///     It notifies the user when a Jobcard is assigned to more than one crew
        ///     It adds a Jobcard to a crew.
        /// </summary>
        /// <param name="crewId"></param>
        /// <param name="siteId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public string AddJobCard(int crewId, int siteId, int taskId)
        {
            string message = "";
            using (var context = new MarigoldSystemContext())
            {
                JobCardCrew jobCardCrew = context.JobCardCrews
                                            .Where(x => x.CrewID == crewId && x.JobCard.SiteID == siteId)
                                            .Select(x => x)
                                            .FirstOrDefault();
                if (jobCardCrew != null)
                {
                    //Check if the same site is not already assigned to the current crew
                    throw new Exception("This site is already assigned to the current Crew");
                }
                else
                {
                    List<JobCardCrew> jobCardCrews = context.JobCardCrews
                                                        .Where(x => x.JobCard.SiteID == siteId && DbFunctions.TruncateTime(x.Crew.CrewDate) == DbFunctions.TruncateTime(DateTime.Today))
                                                        .Select(x => x)
                                                        .ToList();

                    //Notifies the users that existing crew(s) are also assigned to work on the same site.
                    if (jobCardCrews.Count > 0)
                    {
                        foreach (JobCardCrew job in jobCardCrews)
                        {
                            string unit = job.Crew.EquipmentID == null ? job.Crew.Truck.TruckDescription : job.Crew.Equipment.Description;
                            message += unit + ", ";
                        }
                        jobCardCrew = new JobCardCrew();
                        jobCardCrew.CrewID = crewId;
                        jobCardCrew.JobCardID = jobCardCrews[0].JobCardID;
                        context.JobCardCrews.Add(jobCardCrew);
                    }
                    else
                    {
                        JobCard jobCard = new JobCard();
                        jobCard.SiteID = siteId;
                        jobCard.TaskID = taskId;
                        context.JobCards.Add(jobCard);

                        jobCardCrew = new JobCardCrew();
                        jobCardCrew.CrewID = crewId;

                        jobCard.JobCardCrews.Add(jobCardCrew);
                    };
                    context.SaveChanges();
                }
                return message;
            }
        }

        /// <summary>
        /// This method deletes a jobCard
        /// </summary>
        /// <param name="jobCardId"></param>
        public void DeleteJobCardCrew(int jobCardCrewId)
        {
            using (var context = new MarigoldSystemContext())
            {
                JobCardCrew jobCardCrew = context.JobCardCrews.Find(jobCardCrewId);
                int count = context.JobCardCrews
                                            .Where(x => x.JobCardID == jobCardCrew.JobCardID)
                                            .Select(x => x)
                                            .Count();
                if (count > 1)
                {
                    context.JobCardCrews.Remove(jobCardCrew);
                }
                else
                {
                    int jobCardId = jobCardCrew.JobCardID;
                    JobCard jobCard = context.JobCards.Find(jobCardId);
                    context.JobCardCrews.Remove(jobCardCrew);
                    context.JobCards.Remove(jobCard);
                }
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<JobCardStatus> Get_JobCardStatus()
        {
            using (var context = new MarigoldSystemContext())
            {
                return context.JobCards
                                .Where(x => x.ClosedDate == null)
                                .Select(job => new JobCardStatus
                                {
                                    JobCardID = job.JobCardID,
                                    AssignedDate = job.JobCardCrews.FirstOrDefault().Crew.CrewDate,
                                    Pin = job.Site.Pin,
                                    Community = job.Site.Community.Name,
                                    Description = job.Site.Description,
                                    Address = job.Site.StreetAddress,
                                    Task = job.Task.Description,
                                    CompletedDate = DateTime.Today
                                }).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateJobCard(int jobCardId, string completedDate)
        {
            using(var context = new MarigoldSystemContext())
            {
                JobCard jobCard = context.JobCards.Find(jobCardId);
                jobCard.ClosedDate = DateTime.Parse(completedDate);
                context.Entry(jobCard).Property(x => x.ClosedDate).IsModified = true;
                context.Entry(jobCard).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnitReport> GetUnitReports(int yardId)
        {
            using(var context = new MarigoldSystemContext())
            {
                return context.Crews
                                .Where(x => (x.KM_Start == null || x.KM_End == null) && (x.Truck.YardID == yardId || x.Equipment.YardID == yardId))
                                .Select(x => new UnitReport
                                {
                                    CrewID = x.CrewID,
                                    Date = DbFunctions.TruncateTime(x.CrewDate),
                                    Unit = x.TruckID == null ? x.Equipment.Description : x.Truck.TruckDescription,
                                    KM_Start = x.KM_Start,
                                    KM_End = x.KM_End,
                                    Comment = x.AdditionalComments
                                }).ToList();
            }
        }

        public void UpdateCrew(int crewId, int KmStart, int KmEnd, string comment)
        {
            using(var context = new MarigoldSystemContext())
            {
                Crew crew = context.Crews.Find(crewId);
                if(crew == null)
                {
                    throw new Exception("This crew does not exist anymore");
                }
                else
                {
                    string message = "";
                    if (KmStart == 0)
                    {
                        message += "KM Start cannot be equal to 0. ";
                    }
                    if (KmEnd == 0)
                    {
                        message += " KM End cannot be equal to 0. ";
                    }

                    if(KmStart > KmEnd)
                    {
                        message += " KM Start cannot be greater than KM End";
                    }

                    if (string.IsNullOrEmpty(message))
                    {
                        crew.KM_Start = KmStart;
                        context.Entry(crew).Property(x => x.KM_Start).IsModified = true;
                        crew.KM_End = KmEnd;
                        context.Entry(crew).Property(x => x.KM_End).IsModified = true;

                        if (!string.IsNullOrEmpty(comment))
                        {
                            crew.AdditionalComments = comment;
                            context.Entry(crew).Property(x => x.AdditionalComments).IsModified = true;
                        }

                        context.Entry(crew).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(message);
                    }

                }
            }
        }
    }
}
