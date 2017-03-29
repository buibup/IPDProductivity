using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDProductivityWebReport.Models
{
    public class ProductivityData
    {
        public string Year { get; set; }
        public List<ProductivityRNPN> ProductivityRNPNList { get; set; }
    }
}