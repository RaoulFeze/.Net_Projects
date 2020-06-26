<Query Kind="Expression">
  <Connection>
    <ID>31702c39-067f-4f10-adee-28efe15fdcab</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>COE_DB</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

from site in Sites
orderby site.Community.Name ascending
where site.Season.SeasonYear == DateTime.Now.Year && site.Yard.YardID == 1 
let cycles = (from cs in CrewSites where cs.OperationID == 1 && cs.SiteID == site.SiteID select new { Date = cs.Crew.CrewDate })
select new
{
	Pin = site.Pin,
	Community = site.Community.Name,
	Neighbourhood = site.Neighbourhood,
	Address = site.StreetAddress,
	Area = site.Area,
	Notes = site.Notes,
	Cycle1 = cycles.OrderBy(x => x.Date).FirstOrDefault().Date.Equals(null) ? (DateTime?)null : cycles.OrderBy(x => x.Date).FirstOrDefault().Date,
	Cycle2 = cycles.OrderBy(x => x.Date).Skip(1).FirstOrDefault().Date.Equals(null) ? (DateTime?)null : cycles.OrderBy(x => x.Date).Skip(1).FirstOrDefault().Date,
	Cycle3 = cycles.OrderBy(x => x.Date).Skip(2).FirstOrDefault().Date.Equals(null) ? (DateTime?)null : cycles.OrderBy(x => x.Date).Skip(2).FirstOrDefault().Date,
	Cycle4 = cycles.OrderBy(x => x.Date).Skip(3).FirstOrDefault().Date.Equals(null) ? (DateTime?)null : cycles.OrderBy(x => x.Date).Skip(3).FirstOrDefault().Date,
	Cycle5 = cycles.OrderBy(x => x.Date).Skip(4).FirstOrDefault().Date.Equals(null) ? (DateTime?)null : cycles.OrderBy(x => x.Date).Skip(4).FirstOrDefault().Date,
	Pruning = ((from cs in CrewSites where cs.OperationID == 4 && cs.SiteID == site.SiteID select new { Date = cs.Crew.CrewDate }).OrderByDescending(x => x.Date).FirstOrDefault()).Equals(null)
			 ? (DateTime?)null : ((from cs in CrewSites where cs.OperationID == 4 && cs.SiteID == site.SiteID select new { Date = cs.Crew.CrewDate }).OrderByDescending(x => x.Date).FirstOrDefault()).Date,
	Mulching = ((from cs in CrewSites where cs.OperationID == 5 && cs.SiteID == site.SiteID select new { Date = cs.Crew.CrewDate }).OrderByDescending(x => x.Date).FirstOrDefault()).Equals(null)
			 ? (DateTime?)null : ((from cs in CrewSites where cs.OperationID == 5 && cs.SiteID == site.SiteID select new { Date = cs.Crew.CrewDate }).OrderByDescending(x => x.Date).FirstOrDefault()).Date,

}
