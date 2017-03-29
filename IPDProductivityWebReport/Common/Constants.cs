using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace IPDProductivityWebReport.Common
{
    public class Constants
    {
        public static string Svh21CHKConnectionString = ConfigurationManager.ConnectionStrings["svh21-chk"].ToString();
    }
}