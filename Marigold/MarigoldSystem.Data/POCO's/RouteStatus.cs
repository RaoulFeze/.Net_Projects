using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarigoldSystem.Data.POCO_s
{
	public class RouteStatus
	{
		public int SiteID { get; set; }

		public int Pin { get; set; }

		public int? SiteTypeID { get; set; }

		public string Community { get; set; }

		public string Description { get; set; }

		public string Address { get; set; }

		public int Area { get; set; }

		public int? Count { get; set; }

		public string Notes { get; set; }

		public DateTime? Cycle1 { get; set; }

		public DateTime? Cycle2 { get; set; }

		public DateTime? Cycle3 { get; set; }

		public DateTime? Cycle4 { get; set; }

		public DateTime? Cycle5 { get; set; }

		public DateTime? Pruning { get; set; }

		public DateTime? Mulching { get; set; }

		public DateTime? Planting { get; set; }

		public DateTime? Uprooting { get; set; }

		public DateTime? Trimming { get; set; }

		public DateTime? Watering { get; set; }

	}

}
