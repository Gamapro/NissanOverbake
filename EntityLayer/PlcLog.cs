using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class PlcLog
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public int PlcId { get; set; }
        public string Message { get; set; }
        public string PlcName { get; set;}
    }
}
