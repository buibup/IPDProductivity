using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDProductivityWebReport.Models
{
    public class ProductCalc
    {
        public decimal TotalCLS { get; set; }
        public decimal TotalNurse { get; set; }
        public decimal TotalHourNurse { get; set; }
        public decimal Percent { get; set; }
    }
}