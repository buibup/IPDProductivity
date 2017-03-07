using IPDProductivityLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDProductivityUI.Comman
{
    public class Constants
    {
        public static string Cache89ConnectionString = ConfigurationManager.ConnectionStrings["Cache89ConnectionString"].ToString();
        public static string Cache112ConnectionString = ConfigurationManager.ConnectionStrings["Cache112ConnectionString"].ToString();
        public static string Svh21CHKConnectionString = ConfigurationManager.ConnectionStrings["svh21-chk"].ToString();

        public static string RN = ConfigurationManager.AppSettings["RN"];
        public static string NA = ConfigurationManager.AppSettings["NA"];

        public static List<SetWardModel> listSetWardModel = new List<SetWardModel>();
    }
}
