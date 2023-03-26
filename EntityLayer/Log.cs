﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public string SerialNumber { get; set; }
        public int PlcId { get; set; }
        public string PlcName { get; set; }
    }
}
