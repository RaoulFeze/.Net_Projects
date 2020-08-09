using MarigoldSystem.DAL;
using MarigoldSystem.Data.Entities;
using MarigoldSystem.Data.POCO_s;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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
            using (var context = new MarigoldSystemContext())
            {
                var routeSummary = (from site in context.Sites
                                    where site.SiteTypeID == siteTypeId && site.Community.YardID == yardId
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

        public List<Data.Entities.Task> GetTasks()
        {
            using (var context = new MarigoldSystemContext())
            {
                return context.Tasks.ToList();
            }
        }

        public List<RouteStatus> Get_AB_Routes(int yardId, int siteTypeId, int taskId)
        {
            using (var context = new MarigoldSystemContext())
            {
                List<RouteStatus> routes = context.Sites
                                                    .Where(site => site.Community.YardID == yardId && site.SiteTypeID == siteTypeId)
                                                    .OrderBy(site => site.Community.Name)
                                                    .Select(site => new
                                                    {
                                                        SiteID = site.SiteID,
                                                        Pin = site.Pin,
                                                        SiteTypeID = site.SiteTypeID,
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
                                                        SiteID = site.SiteID,
                                                        Pin = site.Pin,
                                                        SiteTypeID = site.SiteTypeID,
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
                                            .Where(site => site.Community.YardID == yardId && site.Grass > 0)
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
                                                                        .Where(job => job.TaskID == 7 && ((DateTime)(job.ClosedDate)).Year == DateTime.Now.Year)
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
                                            .Where(site => site.Community.YardID == yardId && site.Planting == true)
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
                                                                        .Where(job => job.TaskID == 2 && ((DateTime)job.ClosedDate).Year == DateTime.Now.Year)
                                                                        .OrderBy(job => job.ClosedDate)
                                                                        .ThenBy(job => job)
                                                                        .Select(job => job.ClosedDate)
                                                                        .FirstOrDefault(),
                                                Uprooting = context.JobCards
                                                                        .Where(job => job.TaskID == 3 && ((DateTime)job.ClosedDate).Year == DateTime.Now.Year)
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
                                            .Where(site => site.Community.YardID == yardId && site.Watering == true)
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

        //Returns Community ID
        public int GetCommunityId(int siteId)
        {
            using (var context = new MarigoldSystemContext())
            {
                return context.Sites.Find(siteId).Community.CommunityID;
            }
        }

        //returns the list of community
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Community> Get_Communities()
        {
            using(var context = new MarigoldSystemContext())
            {
                return context.Communities.ToList();
            }
        }

        //Returns SiteType
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SiteType> GetSiteTypes()
        {
            using (var context = new MarigoldSystemContext())
            {
                return context.SiteTypes.Select(x => x).ToList();
            }
        }

        public bool CheckSite(int pin, int taskId)
        {
            Site site;
            using (var context = new MarigoldSystemContext())
            {
                if (taskId == 1)
                {
                    site = context.Sites
                                   .Where(x => x.Pin == pin)
                                   .Select(x => x)
                                   .FirstOrDefault();
                    if (site != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (taskId == 2)
                {
                    site = context.Sites
                                    .Where(x => x.Pin == pin && x.Planting == true)
                                    .Select(x => x)
                                    .FirstOrDefault();
                    if (site != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (taskId == 6)
                {
                    site = context.Sites
                                    .Where(x => x.Pin == pin && x.Watering == true)
                                    .Select(x => x)
                                    .FirstOrDefault();
                    if (site != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (taskId == 7)
                {
                    site = context.Sites
                                    .Where(x => x.Pin == pin && x.Grass > 0)
                                    .Select(x => x)
                                    .FirstOrDefault();
                    if (site != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        public bool CheckSite(string community, int taskId)
        {

            Site site;
            using (var context = new MarigoldSystemContext())
            {
                if (taskId == 1)
                {
                    site = context.Sites
                                   .Where(x => x.Community.Name == community)
                                   .Select(x => x)
                                   .FirstOrDefault();
                    if (site != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (taskId == 2)
                {
                    site = context.Sites
                                    .Where(x => x.Community.Name == community && x.Planting == true)
                                    .Select(x => x)
                                    .FirstOrDefault();
                    if (site != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (taskId == 6)
                {
                    site = context.Sites
                                    .Where(x => x.Community.Name == community && x.Watering == true)
                                    .Select(x => x)
                                    .FirstOrDefault();
                    if (site != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (taskId == 7)
                {
                    site = context.Sites
                                    .Where(x => x.Community.Name == community && x.Grass > 0)
                                    .Select(x => x)
                                    .FirstOrDefault();
                    if (site != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        //returns a Route given the pin
        public List<RouteStatus> Get_RouteByPin(int pin, int yardId, int taskId, int season)
        {
            using (var context = new MarigoldSystemContext())
            {
                if (CheckSite(pin, taskId))
                {
                    var routes = context.Sites
                                    .Where(site => site.Community.YardID == yardId && site.Pin == pin)
                                    .Select(site => new
                                    {
                                        SiteID = site.SiteID,
                                        Pin = site.Pin,
                                        SiteTypeID = site.SiteTypeID,
                                        Community = site.Community.Name,
                                        Description = site.Description,
                                        Address = site.StreetAddress,
                                        Area = site.Area,
                                        Notes = site.Notes,
                                        ClosedDate = site.JobCards
                                                            .Where(job => job.TaskID == taskId && ((DateTime)job.ClosedDate).Year == season)
                                                            .OrderByDescending(job => job.ClosedDate.HasValue)
                                                            .ThenBy(job => job.ClosedDate)
                                                            .Select(job => job.ClosedDate)
                                                            .Take(5)
                                                            .ToList()
                                    })
                                    .Select(site => new RouteStatus
                                    {
                                        SiteID = site.SiteID,
                                        Pin = site.Pin,
                                        SiteTypeID = site.SiteTypeID,
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
                else
                {
                    return null;
                }

            }
        }

        //returns Routes given the community
        public List<RouteStatus> GetRoutesByCommunity(string community, int yardId, int taskId, int season)
        {
            using (var context = new MarigoldSystemContext())
            {
                if (CheckSite(community, taskId))
                {

                    if (taskId == 1)
                    {
                        /*--------------SHRUB BED MAINTENANCE--------------------------------------*/
                        int communityId = context.Communities
                                                   .Where(com => com.Name == community)
                                                   .Select(com => com.CommunityID)
                                                   .FirstOrDefault();
                        List<RouteStatus> routes = context.Sites
                                            .Where(site => site.Community.YardID == yardId && site.CommunityID == communityId)
                                            .OrderBy(site => site.Community.Name)
                                            .Select(site => new
                                            {
                                                SiteID = site.SiteID,
                                                Pin = site.Pin,
                                                SiteTypeID = site.SiteTypeID,
                                                Community = site.Community.Name,
                                                Description = site.Description,
                                                Address = site.StreetAddress,
                                                Area = site.Area,
                                                Notes = site.Notes,
                                                ClosedDate = site.JobCards
                                                                    .Where(job => job.TaskID == taskId && ((DateTime)job.ClosedDate).Year == season)
                                                                    .OrderByDescending(job => job.ClosedDate.HasValue)
                                                                    .ThenBy(job => job.ClosedDate)
                                                                    .Select(job => job.ClosedDate)
                                                                    .Take(5)
                                                                    .ToList()
                                            })
                                            .Select(site => new RouteStatus
                                            {
                                                SiteID = site.SiteID,
                                                Pin = site.Pin,
                                                SiteTypeID = site.SiteTypeID,
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
                    else if (taskId == 2)
                    {
                        /*------------------------PLANTING--------------------------*/
                        int communityId = context.Communities
                                                   .Where(com => com.Name == community)
                                                   .Select(com => com.CommunityID)
                                                   .FirstOrDefault();
                        List<RouteStatus> routes = context.Sites
                                            .Where(site => site.Community.YardID == yardId && site.CommunityID == communityId && site.Planting == true)
                                            .OrderBy(site => site.Community.Name)
                                            .Select(site => new RouteStatus
                                            {
                                                SiteID = site.SiteID,
                                                Pin = site.Pin,
                                                SiteTypeID = site.SiteTypeID,
                                                Community = site.Community.Name,
                                                Description = site.Description,
                                                Address = site.StreetAddress,
                                                Area = site.Area,
                                                Notes = site.Notes,
                                                Planting = context.JobCards
                                                                            .Where(job => job.TaskID == 2 && ((DateTime)job.ClosedDate).Year == season)
                                                                            .OrderBy(job => job.ClosedDate)
                                                                            .ThenBy(job => job)
                                                                            .Select(job => job.ClosedDate)
                                                                            .FirstOrDefault(),
                                                Uprooting = context.JobCards
                                                                            .Where(job => job.TaskID == 3 && ((DateTime)job.ClosedDate).Year == season)
                                                                            .OrderBy(job => job.ClosedDate)
                                                                            .ThenBy(job => job)
                                                                            .Select(Job => Job.ClosedDate)
                                                                            .FirstOrDefault()
                                            }).ToList();
                        return routes;
                    }
                    else if (taskId == 6)
                    {

                        /*---------------------WATERING--------------------------------------*/
                        int communityId = context.Communities
                                                  .Where(com => com.Name == community)
                                                  .Select(com => com.CommunityID)
                                                  .FirstOrDefault();
                        List<RouteStatus> routes = context.Sites
                                            .Where(site => site.Community.YardID == yardId && site.CommunityID == communityId && site.Watering == true)
                                            .OrderBy(site => site.Community.Name)
                                            .Select(site => new RouteStatus
                                            {
                                                SiteID = site.SiteID,
                                                Pin = site.Pin,
                                                SiteTypeID = site.SiteTypeID,
                                                Community = site.Community.Name,
                                                Description = site.Description,
                                                Address = site.StreetAddress,
                                                Area = site.Area,
                                                Notes = site.Notes,
                                                Count = context.JobCards
                                                                        .Where(job => job.TaskID == 6 && ((DateTime)job.ClosedDate).Year == season)
                                                                        .Select(job => job)
                                                                        .Count(),
                                                Watering = context.JobCards
                                                                            .Where(job => job.TaskID == 6 && ((DateTime)job.ClosedDate).Year == season)
                                                                            .OrderByDescending(job => job.ClosedDate.HasValue)
                                                                            .ThenBy(job => job)
                                                                            .Select(job => job.ClosedDate)
                                                                            .FirstOrDefault()
                                            }).ToList();
                        return routes;
                    }
                    else if (taskId == 7)
                    {
                        /*------------------------GRASS-------------------------------------------------*/
                        int communityId = context.Communities
                                                  .Where(com => com.Name == community)
                                                  .Select(com => com.CommunityID)
                                                  .FirstOrDefault();
                        List<RouteStatus> routes = context.Sites
                                            .Where(site => site.Community.YardID == yardId && site.CommunityID == communityId && site.Grass > 0)
                                            .OrderBy(site => site.Community.Name)
                                            .Select(site => new RouteStatus
                                            {
                                                SiteID = site.SiteID,
                                                Pin = site.Pin,
                                                SiteTypeID = site.SiteTypeID,
                                                Community = site.Community.Name,
                                                Description = site.Description,
                                                Address = site.StreetAddress,
                                                Area = site.Area,
                                                Notes = site.Notes,
                                                Count = site.Grass,
                                                Trimming = context.JobCards
                                                                            .Where(job => job.TaskID == 7 && ((DateTime)(job.ClosedDate)).Year == season)
                                                                            .OrderBy(job => job.ClosedDate)
                                                                            .ThenBy(job => job)
                                                                            .Select(job => job.ClosedDate)
                                                                            .FirstOrDefault()
                                            }).ToList();
                        return routes;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
        }

        public List<RouteStatus> GetRoutesByDescription(string description, int yardId, int taskId, int season)
        {
            using (var context = new MarigoldSystemContext())
            {
                if (taskId == 1)
                {
                    List<RouteStatus> routes = context.Sites
                                                        .Where(site => site.Community.YardID == yardId && site.Description.Contains(description))
                                                        .OrderBy(site => site.Community.Name)
                                                        .ThenBy(site => site.Description)
                                                        .Select(site => new
                                                        {
                                                            SiteID = site.SiteID,
                                                            Pin = site.Pin,
                                                            SiteTypeID = site.SiteTypeID,
                                                            Community = site.Community.Name,
                                                            Description = site.Description,
                                                            Address = site.StreetAddress,
                                                            Area = site.Area,
                                                            Notes = site.Notes,
                                                            ClosedDate = site.JobCards
                                                                    .Where(job => job.TaskID == taskId && ((DateTime)job.ClosedDate).Year == season)
                                                                    .OrderByDescending(job => job.ClosedDate.HasValue)
                                                                    .ThenBy(job => job.ClosedDate)
                                                                    .Select(job => job.ClosedDate)
                                                                    .Take(5)
                                                                    .ToList()
                                                        })
                                            .Select(site => new RouteStatus
                                            {
                                                SiteID = site.SiteID,
                                                Pin = site.Pin,
                                                SiteTypeID = site.SiteTypeID,
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
                else if (taskId == 2)
                {
                    List<RouteStatus> routes = context.Sites
                                           .Where(site => site.Community.YardID == yardId && site.Description.Contains(description) && site.Planting == true)
                                           .OrderBy(site => site.Community.Name)
                                           .Select(site => new RouteStatus
                                           {
                                               SiteID = site.SiteID,
                                               Pin = site.Pin,
                                               SiteTypeID = site.SiteTypeID,
                                               Community = site.Community.Name,
                                               Description = site.Description,
                                               Address = site.StreetAddress,
                                               Area = site.Area,
                                               Notes = site.Notes,
                                               Planting = context.JobCards
                                                                           .Where(job => job.TaskID == 2 && ((DateTime)job.ClosedDate).Year == season)
                                                                           .OrderBy(job => job.ClosedDate)
                                                                           .ThenBy(job => job)
                                                                           .Select(job => job.ClosedDate)
                                                                           .FirstOrDefault(),
                                               Uprooting = context.JobCards
                                                                           .Where(job => job.TaskID == 3 && ((DateTime)job.ClosedDate).Year == season)
                                                                           .OrderBy(job => job.ClosedDate)
                                                                           .ThenBy(job => job)
                                                                           .Select(Job => Job.ClosedDate)
                                                                           .FirstOrDefault()
                                           }).ToList();
                    return routes;
                }
                else if (taskId == 6)
                {
                    List<RouteStatus> routes = context.Sites
                                            .Where(site => site.Community.YardID == yardId && site.Description.Contains(description) && site.Watering == true)
                                            .OrderBy(site => site.Community.Name)
                                            .Select(site => new RouteStatus
                                            {
                                                SiteID = site.SiteID,
                                                Pin = site.Pin,
                                                SiteTypeID = site.SiteTypeID,
                                                Community = site.Community.Name,
                                                Description = site.Description,
                                                Address = site.StreetAddress,
                                                Area = site.Area,
                                                Notes = site.Notes,
                                                Count = context.JobCards
                                                                        .Where(job => job.TaskID == 6 && ((DateTime)job.ClosedDate).Year == season)
                                                                        .Select(job => job)
                                                                        .Count(),
                                                Watering = context.JobCards
                                                                            .Where(job => job.TaskID == 6 && ((DateTime)job.ClosedDate).Year == season)
                                                                            .OrderByDescending(job => job.ClosedDate.HasValue)
                                                                            .ThenBy(job => job)
                                                                            .Select(job => job.ClosedDate)
                                                                            .FirstOrDefault()
                                            }).ToList();
                    return routes;
                }
                else if (taskId == 7)
                {
                    List<RouteStatus> routes = context.Sites
                                            .Where(site => site.Community.YardID == yardId && site.Description.Contains(description) && site.Grass > 0)
                                            .OrderBy(site => site.Community.Name)
                                            .Select(site => new RouteStatus
                                            {
                                                SiteID = site.SiteID,
                                                Pin = site.Pin,
                                                SiteTypeID = site.SiteTypeID,
                                                Community = site.Community.Name,
                                                Description = site.Description,
                                                Address = site.StreetAddress,
                                                Area = site.Area,
                                                Notes = site.Notes,
                                                Count = site.Grass,
                                                Trimming = context.JobCards
                                                                            .Where(job => job.TaskID == 7 && ((DateTime)(job.ClosedDate)).Year == season)
                                                                            .OrderBy(job => job.ClosedDate)
                                                                            .ThenBy(job => job)
                                                                            .Select(job => job.ClosedDate)
                                                                            .FirstOrDefault()
                                            }).ToList();
                    return routes;
                }
                else
                {
                    return null;
                }

            }
        }


        public List<RouteStatus> GetRoutesByAddress(string address, int yardId, int taskId, int season)
        {
            using (var context = new MarigoldSystemContext())
            {
                List<RouteStatus> routes = new List<RouteStatus>();
                if (taskId == 1)
                {
                    routes = context.Sites
                                        .Where(site => site.Community.YardID == yardId && site.StreetAddress.Contains(address))
                                        .OrderBy(site => site.Community.Name)
                                        .ThenBy(site => site.Description)
                                        .Select(site => new
                                        {
                                            SiteID = site.SiteID,
                                            Pin = site.Pin,
                                            SiteTypeID = site.SiteTypeID,
                                            Community = site.Community.Name,
                                            Description = site.Description,
                                            Address = site.StreetAddress,
                                            Area = site.Area,
                                            Notes = site.Notes,
                                            ClosedDate = site.JobCards
                                                    .Where(job => job.TaskID == taskId && ((DateTime)job.ClosedDate).Year == season)
                                                    .OrderByDescending(job => job.ClosedDate.HasValue)
                                                    .ThenBy(job => job.ClosedDate)
                                                    .Select(job => job.ClosedDate)
                                                    .Take(5)
                                                    .ToList()
                                        })
                                            .Select(site => new RouteStatus
                                            {
                                                SiteID = site.SiteID,
                                                Pin = site.Pin,
                                                SiteTypeID = site.SiteTypeID,
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
                }
                else if (taskId == 2)
                {
                    routes = context.Sites
                                        .Where(site => site.Community.YardID == yardId && site.StreetAddress.Contains(address) && site.Planting == true)
                                        .OrderBy(site => site.Community.Name)
                                        .Select(site => new RouteStatus
                                        {
                                            SiteID = site.SiteID,
                                            Pin = site.Pin,
                                            SiteTypeID = site.SiteTypeID,
                                            Community = site.Community.Name,
                                            Description = site.Description,
                                            Address = site.StreetAddress,
                                            Area = site.Area,
                                            Notes = site.Notes,
                                            Planting = context.JobCards
                                                                        .Where(job => job.TaskID == 2 && ((DateTime)job.ClosedDate).Year == season)
                                                                        .OrderBy(job => job.ClosedDate)
                                                                        .ThenBy(job => job)
                                                                        .Select(job => job.ClosedDate)
                                                                        .FirstOrDefault(),
                                            Uprooting = context.JobCards
                                                                        .Where(job => job.TaskID == 3 && ((DateTime)job.ClosedDate).Year == season)
                                                                        .OrderBy(job => job.ClosedDate)
                                                                        .ThenBy(job => job)
                                                                        .Select(Job => Job.ClosedDate)
                                                                        .FirstOrDefault()
                                        }).ToList();
                }
                else if (taskId == 6)
                {
                    routes = context.Sites
                                        .Where(site => site.Community.YardID == yardId && site.StreetAddress.Contains(address) && site.Watering == true)
                                        .OrderBy(site => site.Community.Name)
                                        .Select(site => new RouteStatus
                                        {
                                            SiteID = site.SiteID,
                                            Pin = site.Pin,
                                            SiteTypeID = site.SiteTypeID,
                                            Community = site.Community.Name,
                                            Description = site.Description,
                                            Address = site.StreetAddress,
                                            Area = site.Area,
                                            Notes = site.Notes,
                                            Count = context.JobCards
                                                                    .Where(job => job.TaskID == 6 && ((DateTime)job.ClosedDate).Year == season)
                                                                    .Select(job => job)
                                                                    .Count(),
                                            Watering = context.JobCards
                                                                        .Where(job => job.TaskID == 6 && ((DateTime)job.ClosedDate).Year == season)
                                                                        .OrderByDescending(job => job.ClosedDate.HasValue)
                                                                        .ThenBy(job => job)
                                                                        .Select(job => job.ClosedDate)
                                                                        .FirstOrDefault()
                                        }).ToList();
                }
                else if (taskId == 7)
                {
                    routes = context.Sites
                                        .Where(site => site.Community.YardID == yardId && site.StreetAddress.Contains(address) && site.Grass > 0)
                                        .OrderBy(site => site.Community.Name)
                                        .Select(site => new RouteStatus
                                        {
                                            SiteID = site.SiteID,
                                            Pin = site.Pin,
                                            SiteTypeID = site.SiteTypeID,
                                            Community = site.Community.Name,
                                            Description = site.Description,
                                            Address = site.StreetAddress,
                                            Area = site.Area,
                                            Notes = site.Notes,
                                            Count = site.Grass,
                                            Trimming = context.JobCards
                                                                        .Where(job => job.TaskID == 7 && ((DateTime)(job.ClosedDate)).Year == season)
                                                                        .OrderBy(job => job.ClosedDate)
                                                                        .ThenBy(job => job)
                                                                        .Select(job => job.ClosedDate)
                                                                        .FirstOrDefault()
                                        }).ToList();
                }

                if(routes == null)
                {
                    return null;
                }
                else 
                { 
                    return routes; 
                }

            }
        }



        //Updates Sites
        //public void UpdateSite(int siteId, int communityId, int siteTypeId, string description, string address, int area)
        //{
        //    using (var context = new MarigoldSystemContext())
        //    {
        //        Site site = context.Sites.Find(siteId);

        //        if (site == null)
        //        {
        //            throw new Exception("The site you are trying to update does not exist anymore");
        //        }
        //        else
        //        {

        //            if(site.Description != description)
        //            {
        //                site.Description = description;
        //                context.Entry(site).Property(x => x.Description).IsModified = true;
        //            }
        //            if(site.StreetAddress != address)
        //            {
        //                site.StreetAddress = address;
        //                context.Entry(site).Property(x => x.StreetAddress).IsModified = true;
        //            }
        //            if(site.Area != area)
        //            {
        //                site.Area = area;
        //                context.Entry(site).Property(x => x.Area).IsModified = true;
        //            }
        //        }
        //        //context.Entry(site).State = System.Data.Entity.EntityState.Modified;
        //        //context.SaveChanges();
        //    }
        //}

    }
}
