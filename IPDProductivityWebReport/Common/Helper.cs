using IPDProductivityWebReport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IPDProductivityWebReport.Common
{
    public class Helper
    {
        private static string _wardCache = string.Empty;
        private static string _wardSQL = string.Empty;

        public static Dictionary<string, string> MappingsWard = new Dictionary<string, string>()
        {
            // cache (IPDCLSTM) , IPDCLSN (table Nurse(RN,PN)) }
            { "11W3B", "11W3B" },
            //{ "11W3P", "11W3R" }, close
            { "11W4L", "11W4A" },
            { "11W4R", "11W4R" },
            { "11W5L", "11W5A" },
            { "11W5R", "11W5B" },
            { "11W5C", "11W5R" },
            { "11W6L", "11W6A" },
            { "11W6R", "11W6B" },
            { "11W3P", "11W3R" },
            { "11NSY", "11NSY" },
            { "11NICU", "11NSY" },
            { "11NIM", "11NSY" },
            { "11ICU1", "11ICU" },
            { "11ICUC1", "11ICUC" }
        };

        public static string GetWardInCache()
        {
            if (string.IsNullOrEmpty(_wardCache))
            {
                _wardCache = string.Join(",", MappingsWard.Select(w => "'" + w.Key + "'").ToArray());
                return _wardCache;
            }
            return _wardCache;
        }

        public static string GetWardInSQL()
        {
            if (string.IsNullOrEmpty(_wardSQL))
            {
                _wardSQL = string.Join(",", MappingsWard.Select(w => "'" + w.Value + "'").ToArray());
                return _wardSQL;
            }
            return _wardSQL;
        }

        static decimal GetPercent(decimal totalCLS, decimal totalNurseX7)
        {
            return (totalCLS * 100) / (totalNurseX7 * 7);
        }

        public static ProductivityData DataTableToProductivityData(DataTable dtTotalCLS, DataTable dtProRNPN, string year)
        {
            ProductivityData proData = new ProductivityData();
            List<ProductivityRNPN> proRNPNList = new List<ProductivityRNPN>();
            ProductivityRNPN proRNPN = null;
            List<ProductivityWard> proWardList = new List<ProductivityWard>();
            ProductivityWard proWard = null;
            ProductCalc proCalcRN = null;
            ProductCalc proCalcPN = null;

            proData.Year = year;

            foreach(DataRow rowTotalCLS in dtTotalCLS.Rows)
            {
                proRNPN = new ProductivityRNPN();
                proCalcRN = new ProductCalc();
                proWard = new ProductivityWard();
                proCalcPN = new ProductCalc();

                string month = string.Empty;
                if(month != rowTotalCLS["Month"].ToString())
                {
                    proRNPN.Month = rowTotalCLS["Month"].ToString();
                }
                

                decimal totalCLSRN = Convert.ToDecimal(rowTotalCLS["TotalCLSRN"].ToString());
                decimal totalCLSPN = Convert.ToDecimal(rowTotalCLS["TotalCLSPN"].ToString());
                decimal totalNurseX7RN = 0;
                decimal totalNurseX7PN = 0;


                foreach(DataRow rowProRNPN in dtProRNPN.Rows)
                {
                    proCalcRN = new ProductCalc();
                    proCalcPN = new ProductCalc();
                    if (rowTotalCLS["Month"].ToString() == rowProRNPN["Month"].ToString())
                    {
                        if(rowTotalCLS["WRD"].ToString() == rowProRNPN["GWard"].ToString())
                        {

                            proWard.Ward = rowTotalCLS["WRD"].ToString();

                            // RN
                            totalNurseX7RN = Convert.ToDecimal(rowProRNPN["TotalNurseX7RN"].ToString());
                            proCalcRN.TotalCLS = totalCLSRN;
                            proCalcRN.TotalNurse = Convert.ToDecimal(rowProRNPN["TotalNurseRN"].ToString());
                            proCalcRN.TotalHourNurse = totalNurseX7RN;
                            proCalcRN.Percent = GetPercent(totalCLSRN, totalNurseX7RN);

                            // PN
                            totalNurseX7PN = Convert.ToDecimal(rowProRNPN["TotalNursePN"].ToString());
                            proCalcPN.TotalCLS = totalCLSPN;
                            proCalcPN.TotalNurse = Convert.ToDecimal(rowProRNPN["TotalNursePN"].ToString());
                            proCalcPN.TotalHourNurse = totalNurseX7PN;
                            proCalcPN.Percent = GetPercent(totalCLSPN, totalNurseX7PN);

                            proWard.ProductCalcRN = proCalcRN;
                            proWard.ProductCalcPN = proCalcPN;
                        }
                    }

                    proWardList.Add(proWard);
                }

                proRNPNList.Add(proRNPN);
            }

            proData.ProductivityRNPNList = proRNPNList;

            return proData;
        }

        public static DataTable MapWardDataTable(DataTable dt)
        {
            foreach(DataRow row in dt.Rows)
            {
                row["WRD"] = MapWardString(row["WRD"].ToString());
            }

            return dt;
        }

        static string MapWardString(string ward)
        {
            string _ward = string.Empty;

            switch (ward)
            {
                case "11W3B":
                    _ward = ward;
                    break;
                case "11W4L":
                    _ward = "11W4A";
                    break;
                case "11W4R":
                    _ward = ward;
                    break;
                case "11W5L":
                    _ward = "11W5A";
                    break;
                case "11W5R":
                    _ward = "11W5B";
                    break;
                case "11W5C":
                    _ward = "11W5R";
                    break;
                case "11W6L":
                    _ward = "11W6A";
                    break;
                case "11W6R":
                    _ward = "11W6B";
                    break;
                case "11W7R":
                    _ward = "11W7B";
                    break;
                case "11NSY":
                    _ward = "11NSY";
                    break;
                case "11NICU":
                    _ward = "11NSY";
                    break;
                case "11NIM":
                    _ward = "11NSY";
                    break;
                case "11ICU1":
                    _ward = "11ICU";
                    break;
                case "11ICUC1":
                    _ward = "11ICUC";
                    break;
                default:
                    break;
            }

            return _ward;
        }
    }
}