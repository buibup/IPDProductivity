using IPDProductivityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDProductivityUI.Common
{
    public class Data
    {
        public static string _11W4L { get { return "11W4L"; } }
        public static string _11W4R { get { return "11W4R"; } }

        public static Dictionary<string, string> WardsCache = new Dictionary<string, string>()
        {
            // { wardcode, warddesc }
            { "11W4L", "1-Fl 4 A" },
            { "11W4R", "1-Ward 4 Royal Wing" }
        };

        public static Dictionary<string, string> MapWardCacheWithSql = new Dictionary<string, string>()
        {
            // { Cache , SQL }
            { "11W3B", "11W3B" },
            //{ "11W3P", "11W3R" },
            { "11W4L", "11W4A" },
            { "11W4R", "11W4R" },
            { "11W5L", "11W5A" },
            { "11W5R", "11W5B" },
            { "11W5C", "11W5R" },
            { "11W6L", "11W6A" },
            { "11W6R", "11W6B" },
            { "11W7R", "11W7B" },
            { "11NSY", "11NSY" },
            { "11NICU", "11NSY" },
            { "11NIM", "11NSY" },
            { "11ICU1", "11ICU" },
            { "11ICUC1", "11ICUC" }
        };

        public static Dictionary<string, int> MapWardAdd = new Dictionary<string, int>()
        {
            // { wardcode , add after sum mul class }
            { "11W4L", 0 },
            { "11W4R", 14 }
        };

        
    }
}
