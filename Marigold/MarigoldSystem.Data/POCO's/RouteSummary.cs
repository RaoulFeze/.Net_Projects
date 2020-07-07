using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarigoldSystem.Data.POCO_s
{
	public class RouteSummary
	{
		public int SiteID { get; set; }
		public int Pin { get; set; }
		public string Community { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public int Area { get; set; }
		public int Count { get; set; }
		public DateTime? LastDate { get; set; }
	}
}
