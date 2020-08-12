using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarigoldSystem.Data.POCO_s
{
	public class JobCardStatus
	{
		public int JobCardID { get; set; }
		public int Pin { get; set; }
		public string Community { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public DateTime AssignedDate { get; set; }
		public DateTime? CompletedDate { get; set; }
	}
}
