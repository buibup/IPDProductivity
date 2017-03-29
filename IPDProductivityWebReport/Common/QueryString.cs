using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDProductivityWebReport.Common
{
    public class QueryString
    {
        public static string GetTotalCLS(string year)
        {
            string ward = Helper.GetWardInCache();
            string result = @"

                 Select [Year], [Month], WRD, sum(CLSRN) TotalCLSRN, sum(CLSPN) TotalCLSPN
                 From
                 (
	                select year(upd_dte) [Year],month(upd_dte) [Month], WRD,
	                (case cls when 1 then cls * 2.5
		                when 2 then cls * 4.5
		                when 3 then cls * 6
		                when 4 then cls * 7
		                when 5 then cls * 8 else 0 end) CLSRN,
				    (case cls when 1 then cls * 2
		                when 2 then cls * 2
		                when 3 then cls * 2
		                when 4 then cls * 3
		                when 5 then cls * 3 else 0 end) CLSPN
	                from ipdclstm 
	                where year(upd_dte) = '2016' and WRD in ({ward})
                    --and substring(WRD,1,2) = '{hos}'
                 )A
                 Group by [Year], [Month], WRD
                 order by [Month]

                ";

            result = result.Replace("{ward}", ward);
            result = result.Replace("{year}", year);

            return result;
        }

        public static string GetProductivityRNPN(string year)
        {
            string ward = Helper.GetWardInSQL();

            string result = @"

                SELECT [Year],[Month],GWard,TotalNurseRN,TotalNurseX7RN,TotalNursePN,TotalNurseX7PN
                  FROM [MEDTRAK_DATA].[dbo].[vIPDProductivityRNPN]
                  WHERE  [Year] = '{year}' And GWard in ({ward})


            ";

            result = result.Replace("{ward}", ward);
            result = result.Replace("{year}", year);

            return result;
        }

        public static string GetProductivityRN(string year)
        {
            string ward = Helper.GetWardInCache();

            string result = @"

                SELECT [Year],[Month],GWard,TotalNurse,TotalNurseX7
                  FROM [MEDTRAK_DATA].[dbo].[vIPDProductivityRN]
                  WHERE  [Year] = '{year}' And GWard in ({ward})


            ";

            result = result.Replace("{ward}", ward);
            result = result.Replace("{year}", year);

            return result;
        }

        public static string GetProductivityPN(string year)
        {
            string ward = Helper.GetWardInCache();

            string result = @"

                SELECT [Year],[Month],GWard,TotalNurse,TotalNurseX7
                  FROM [MEDTRAK_DATA].[dbo].[vIPDProductivityPN]
                  WHERE  [Year] = '{year}' And GWard in ({ward})


            ";

            result = result.Replace("{ward}", ward);
            result = result.Replace("{year}", year);

            return result;
        }
    }
}