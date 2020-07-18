using MarigoldSystem.DAL;
using MarigoldSystem.Data.POCO_s;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarigoldSystem.BLL
{
    [DataObject]
    public class RouteController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<RouteSummary> GetRouteSummaries(int yardId, int siteTypeId)
        {
            using(var context = new MarigoldSystemContext())
            {
                var routeSummary = (from site in context.Sites
								   where site.SiteTypeID == siteTypeId && site.YardID == yardId
								   orderby site.Community.Name
								   select new RouteSummary
								   {
									   SiteID = site.SiteID,
									   Pin = site.Pin,
									   Community = site.Community.Name,
									   Description = site.Description,
									   Address = site.StreetAddress,
									   Area = site.Area,
									   Count = site.JobCards.Where(x => ((DateTime)x.ClosedDate).Year == DateTime.Now.Year).Select(x => x).Count(),
                                       LastDate = site.JobCards.Where(x => ((DateTime)x.ClosedDate).Year == DateTime.Now.Year).OrderByDescending(x => x.ClosedDate).Select(x => x.ClosedDate).FirstOrDefault()
								   }).ToList();
				return routeSummary;
			}
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Data.Entities.Task> GetTasks()
        {
            using (var context = new MarigoldSystemContext())
            {
                return context.Tasks.ToList();
            }
        }

		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<RouteStatus> Get_AB_Routes(int yardId, int siteTypeId, int taskId)
        {
            using(var context = new MarigoldSystemContext())
            {
                List<RouteStatus> routes = context.Sites
													.Where(site => site.YardID == yardId && site.SiteTypeID == siteTypeId)
													.OrderBy(site => site.Community.Name)
													.Select(site => new
													{
														Pin = site.Pin,
														Community = site.Community.Name,
														Description = site.Description,
														Address = site.StreetAddress,
														Area = site.Area,
														Notes = site.Notes,
														ClosedDate = site.JobCards
																			.Where(job => job.TaskID == taskId && ((DateTime)job.ClosedDate).Year == DateTime.Now.Year)
																			.OrderByDescending(job => job.ClosedDate.HasValue)
																			.ThenBy(job => job.ClosedDate)
																			.Select(job => job.ClosedDate)
																			.Take(5)
																			.ToList()
													})
													.Select(site => new RouteStatus
													{
														Pin = site.Pin,
														Community = site.Community,
														Description = site.Description,
														Address = site.Address,
														Area = site.Area,
														Notes = site.Notes,
														Cycle1 = site.ClosedDate.FirstOrDefault(),
														Cycle2 = site.ClosedDate.OrderByDescending(x => x.HasValue).ThenBy(x => x).Skip(1).FirstOrDefault(),
														Cycle3 = site.ClosedDate.OrderByDescending(x => x.HasValue).ThenBy(x => x).Skip(2).FirstOrDefault(),
														Cycle4 = site.ClosedDate.OrderByDescending(x => x.HasValue).ThenBy(x => x).Skip(3).FirstOrDefault(),
														Cycle5 = site.ClosedDate.OrderByDescending(x => x.HasValue).ThenBy(x => x).Skip(4).FirstOrDefault()
													}).ToList();
				return routes;
			}
        }

        //Grass List
        public List<RouteStatus> GrassRouteList(int yardId)
        {
            using (var context = new MarigoldSystemContext())
            {
                var RouteList = context.Sites
                                            .Where(site => site.YardID == yardId && site.Grass > 0)
                                            .OrderBy(site => site.Community.Name)
                                            .Select(site => new RouteStatus
                                            {
                                                Pin = site.Pin,
                                                Community = site.Community.Name,
                                                Description = site.Description,
                                                Address = site.StreetAddress,
                                                Area = site.Area,
                                                Notes = site.Notes,
                                                Count = site.Grass,
                                                Trimming = context.JobCards
                                                                        .Where(job => job.TaskID == 7 && ((DateTime) (job.ClosedDate)).Year == DateTime.Now.Year)
                                                                        .OrderBy(job => job.ClosedDate)
                                                                        .ThenBy(job => job)
                                                                        .Select(job => job.ClosedDate)
                                                                        .FirstOrDefault()
                                            });
                return RouteList.ToList();
            }
        }

        //Planting Routes
        public List<RouteStatus> PlantingList(int yardId)
        {
            using (var context = new MarigoldSystemContext())
            {
                var PlantingList = context.Sites
                                            .Where(site => site.YardID == yardId && site.Planting == true)
                                            .OrderBy(site => site.Community.Name)
                                            .Select(site => new RouteStatus
                                            {
                                                Pin = site.Pin,
                                                Community = site.Community.Name,
                                                Description = site.Description,
                                                Address = site.StreetAddress,
                                                Area = site.Area,
                                                Notes = site.Notes,
                                                Planting = context.JobCards     
                                                                        .Where(job => job.TaskID == 2 && ((DateTime) job.ClosedDate).Year == DateTime.Now.Year)
                                                                        .OrderBy(job => job.ClosedDate)
                                                                        .ThenBy(job => job)
                                                                        .Select(job => job.ClosedDate)
                                                                        .FirstOrDefault(),
                                                Uprooting = context.JobCards
                                                                        .Where(job => job.TaskID == 3 && ((DateTime) job.ClosedDate).Year == DateTime.Now.Year)
                                                                        .OrderBy(job => job.ClosedDate)
                                                                        .ThenBy(job => job)
                                                                        .Select(Job => Job.ClosedDate)
                                                                        .FirstOrDefault()
                                            });
                return PlantingList.ToList();
            }
        }

        //Watering Routes
        public List<RouteStatus> WateringList(int yardId)
        {
            using (var context = new MarigoldSystemContext())
            {
                var WateringList = context.Sites
                                            .Where(site => site.YardID == yardId && site.Watering == true)
                                            .OrderBy(site => site.Community.Name)
                                            .Select(site => new RouteStatus
                                             {
                                                 Pin = site.Pin,
                                                 Community = site.Community.Name,
                                                 Description = site.Description,
                                                 Address = site.StreetAddress,
                                                 Area = site.Area,
                                                 Notes = site.Notes,
                                                 Count = context.JobCards
                                                                    .Where(job => job.TaskID == 6 && ((DateTime)job.ClosedDate).Year == DateTime.Now.Year)
                                                                    .Select(job => job)
                                                                    .Count(),
                                                 Watering = context.JobCards
                                                                        .Where(job => job.TaskID == 6 && ((DateTime)job.ClosedDate).Year == DateTime.Now.Year)
                                                                        .OrderByDescending(job => job.ClosedDate.HasValue)
                                                                        .ThenBy(job => job)
                                                                        .Select(job => job.ClosedDate)
                                                                        .FirstOrDefault()

                                            });
                return WateringList.ToList();
            }
        }

    }
}
