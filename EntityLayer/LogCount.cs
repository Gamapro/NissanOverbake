using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class LogCount
    {
        public int PlcId { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
        public string PlcName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
