using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDProductivityWebReport.Models
{
    public class ProductivityRNPN
    {
        public string Month { get; set; }
        public List<ProductivityWard> ProductivityWardList { get; set; }
    }
}