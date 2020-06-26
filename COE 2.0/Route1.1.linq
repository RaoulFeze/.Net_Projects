<Query Kind="Expression">
  <Connection>
    <ID>31702c39-067f-4f10-adee-28efe15fdcab</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>COE_DB</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

from crewSite in CrewSites
where crewSite.Site.YardID == 1
group crewSite by crewSite.Operation.OperationID into OpGr
from x in OpGr where OpGr.Key == 1 && x.Site.Season.SeasonYear == DateTime.Now.Year 
from y in OpGr where OpGr.Key == 4 
from z in OpGr where OpGr.Key == 5 
select new {
	Pin = x.Site.Pin,
	
}