using MarigoldSystem.Data.POCO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarigoldSystem.Data.DTO_s
{
    public class CurrentCrews
	{
		public int CrewID { get; set; }
		public string Description { get; set; }
		public List<Member> Crew { get; set; }
		public List<Job> JobCards { get; set; }
	}
}
