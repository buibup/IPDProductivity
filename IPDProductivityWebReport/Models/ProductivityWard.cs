using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDProductivityWebReport.Models
{
    public class ProductivityWard
    {
        public string Ward { get; set; }
        public ProductCalc ProductCalcRN { get; set; }
        public ProductCalc ProductCalcPN { get; set; }
    }
}