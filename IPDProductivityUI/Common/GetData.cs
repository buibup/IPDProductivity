using IPDProductivityLibrary;
using IPDProductivityUI.DA.SqlServer;
using SendEmailCSI.DA.InterSystems;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDProductivityUI.Common
{
    public class GetData
    {
        static DataTable dt;

        // Return [ward, room] with datatable from cache
        public static DataTable GetBedRoom(string wardCode)
        {
            dt = new DataTable();

            dt = InterSystemsDA.DataTableBindDataCommand(QueryString.GetBedRoomQueryString(wardCode), Constants.Cache89ConnectionString);

            return dt;
        }

        // Return [hn, episode] with tuple from cache
        public static Tuple<string, string> PatientHnAdm(string bedRowId)
        {
            dt = new DataTable();
            string papmi_No = "";
            string paadm_Admno = "";

            dt = InterSystemsDA.DataTableBindDataCommand(QueryString.GetPatientIPDQueryString(bedRowId), Constants.Cache89ConnectionString);

            if (dt.Rows.Count > 0)
            {
                papmi_No = dt.Rows[0]["RN_No"].ToString();
                paadm_Admno = dt.Rows[0]["EpisodeNo"].ToString();
            }

            return Tuple.Create(papmi_No, paadm_Admno);
        }

        public static string GetPatientClass(string papmiNo)
        {
            string patientClass = "";

            if (!string.IsNullOrEmpty(papmiNo))
            {
                string cmdString = QueryString.GetPatientClassQueryString(papmiNo);
                var value = SqlServerDA.ExecuteScalarBindDataCommand(cmdString, Constants.Svh21CHKConnectionString);
                patientClass = string.IsNullOrEmpty(value) ? "" : patientClass = value;
            }
            else
            {
                patientClass = "E";
            }

            
            return patientClass;
        }

        public static string GetStatReasonNotAvail(string bedRowId)
        {
            string result = string.Empty;
            string cmdString = QueryString.GetStatReasonNotAvailQueryString(bedRowId);
            var value = InterSystemsDA.ExecuteScalarBindDataCommand(cmdString, Constants.Cache89ConnectionString);

            if (!string.IsNullOrEmpty(value))
            {
                int intValue = 0;
                if(Int32.TryParse(value, out intValue))
                {
                    if(intValue == 3)
                    {
                        result = "W";
                    }else if(intValue == 6)
                    {
                        result = "R";
                    }else if(intValue == 7)
                    {
                        result = "O";
                    }else if(intValue > 0)
                    {
                        result = "C";
                    }
                }
            }

            return result;
        }

        public static decimal GetPatientKardex(string epi)
        {
            decimal result = 0;

            string cmdString = QueryString.GetKardexQueryString(epi);
            var value = SqlServerDA.ExecuteScalarBindDataCommand(cmdString, Constants.Svh21CHKConnectionString);

            if (!string.IsNullOrEmpty(value))
            {
                if(value.ToUpper().Trim() == "CP" || value.ToUpper().Trim() == "AP" || value.ToUpper().Trim() == "" || value.ToUpper().Trim() == "IP" || value.ToUpper().Trim() == "DP")
                {
                    result = 1;
                }
            }

            return result;
        }

        public static List<SetWardModel> GetSetWards()
        {
            List<SetWardModel> setWard = new List<SetWardModel>();

            string cmdString = QueryString.GetSetWardQueryString();

            var dt = SqlServerDA.DataTableBindDataCommand(cmdString, Constants.Svh21CHKConnectionString);
            setWard = Helper.DtToSetWardModelList(dt); 

            return setWard;
        }

        public static string GetBedType(string bedRowId)
        {
            string result = string.Empty;

            string cmdString = QueryString.GetBedTypeQueryString(bedRowId);
            result = InterSystemsDA.ExecuteScalarBindDataCommand(cmdString, Constants.Cache89ConnectionString);
            result = result.Substring(2, 1);

            return result.Trim();
        }
    }
}
