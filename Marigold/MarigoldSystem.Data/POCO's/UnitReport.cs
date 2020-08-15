using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarigoldSystem.Data.POCO_s
{
    public class UnitReport
    {
        public int CrewID { get; set; }
        public DateTime? Date { get; set; }
        public string Unit { get; set; }
        public int? KM_Start { get; set; }
        public int? KM_End { get; set; }
        public string Comment { get; set; }
    }
}
