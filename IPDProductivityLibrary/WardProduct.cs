using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDProductivityLibrary
{
    public class WardProduct 
    {
        public string WardCode { get; set; }
        public WardModel WardModel { get; set; }
        public WardCalculate WardCalculate { get; set; }
        public Staff Staffs { get; set; }
    }
}
