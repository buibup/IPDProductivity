using IPDProductivityWebReport.DA;
using IPDProductivityWebReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDProductivityWebReport.Common
{
    public class GetData
    {
        static string conString = Constants.Svh21CHKConnectionString;
        public static ProductivityData GetProductivityData(string year)
        {
            ProductivityData result = new ProductivityData();

            var dtTotalCLS = SqlServerDA.DataTableBindDataCommand(QueryString.GetTotalCLS(year), conString);
            var dtCLSRNMapWard = Helper.MapWardDataTable(dtTotalCLS);

            var dtProRNPN = SqlServerDA.DataTableBindDataCommand(QueryString.GetProductivityRNPN(year), conString);

            result = Helper.DataTableToProductivityData(dtCLSRNMapWard, dtProRNPN, year);

            return result;
        }

    }
}