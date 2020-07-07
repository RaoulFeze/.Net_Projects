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
									   Count = site.JobCards.Where(x => (x.Crew.CrewDate).Year == DateTime.Now.Year && x.ClosedDate.HasValue).Select(x => x).Count(),
                                       LastDate = site.JobCards.Where(x => (x.Crew.CrewDate).Year == DateTime.Now.Year && x.ClosedDate.HasValue).OrderByDescending(x => x.ClosedDate).Select(x => x.ClosedDate).FirstOrDefault()
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
    }
}
