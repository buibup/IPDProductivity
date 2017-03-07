
using IPDProductivityLibrary;
using IPDProductivityUI.DA.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IPDProductivityUI.Comman
{
    public class Helper
    {

        public static WardProduct DtToWardProduct(DataTable dt, string wardCode)
        {
            List<PatientRoomClass> items = new List<PatientRoomClass>();
            List<string> listClass = new List<string>();

            StaffReal staffReal = new StaffReal();
            StaffData staffData = new StaffData();
            DataTable dtStaffReal = new DataTable();

            decimal sumStaffReal = 0;
            decimal sumOfMulClass = 0;
            decimal sumOfRealStaff = 0;
            decimal percentage = 0;
            decimal sumPatientKardex = 0;
            decimal sumOfMulClassmulClsx100 = 0;
            decimal sumPatientClass = 0;

            // set value to object PatientRoomClass
            foreach (DataRow row in dt.Rows)
            {
                int ptClass = 0;
                var item = new PatientRoomClass();
                Tuple<string, string> patient = GetData.PatientHnAdm(row["BED_RowID"].ToString());
                string papmiNo = patient.Item1;
                string epi = patient.Item2;
                item.BED_RowID = row["BED_RowID"].ToString();
                item.BED_Code = GetData.GetBedType(row["BED_RowID"].ToString()) + row["BED_Code"].ToString();
                item.Paadm_AdmNo = epi;
                sumPatientKardex += GetData.GetPatientKardex(epi);
                item.Papmi_No = papmiNo;
                item.PatientClass = GetData.GetPatientClass(papmiNo);
                if (!string.IsNullOrEmpty(papmiNo))
                {
                    sumPatientClass += 1;
                }
                if (Int32.TryParse(item.PatientClass, out ptClass))
                {
                    listClass.Add(item.PatientClass);
                }
                if (item.PatientClass == null)
                {
                    item.PatientClass = "??";
                }
                var reasonNotAvail = GetData.GetStatReasonNotAvail(row["BED_RowID"].ToString());
                item.PatientClass = string.IsNullOrEmpty(reasonNotAvail) ? item.PatientClass : reasonNotAvail;
                items.Add(item);
            }


            WardModel wardModel = new WardModel
            {
                PatientRoomClassList = items
            };

            string wardSql = string.Empty;


            if (Data.MapWardCacheWithSql.TryGetValue(wardCode, out wardSql))
            {
                dtStaffReal = SqlServerDA.DataTableBindDataCommand(QueryString.GetRealStaffQueryString(wardSql, DateTime.Now), Constants.Svh21CHKConnectionString);
            }

            // set staff real to object StaffReal
            foreach (DataRow row in dtStaffReal.Rows)
            {
                staffReal.DayRnReal = Convert.ToDecimal(row["DayRnReal"]);
                staffReal.DayNrReal = Convert.ToDecimal(row["DayNrReal"]);
                staffReal.EveRnReal = Convert.ToDecimal(row["EveRnReal"]);
                staffReal.EveNrReal = Convert.ToDecimal(row["EveNrReal"]);
                staffReal.NigRnReal = Convert.ToDecimal(row["NigRnReal"]);
                staffReal.NigNrReal = Convert.ToDecimal(row["NigNrReal"]);
            }

            sumStaffReal = Calculate.SumStaffReal(staffReal);

            string wardCase = string.Empty;
            int add = 0;
            if (wardCode.Contains("W"))
            {
                wardCase = "W";
                Data.MapWardAdd.TryGetValue(wardCode, out add);
                sumOfMulClass = Calculate.PatientClassCalc(listClass, wardCase, add);
                sumOfMulClassmulClsx100 = Calculate.GetClsMul100(sumOfMulClass);
                sumOfRealStaff = Calculate.GetSumOfRealStaff(sumStaffReal, wardCase);
                percentage = Calculate.GetPercentag(sumOfMulClassmulClsx100, sumOfRealStaff);


                staffData = Calculate.GetStaffData(sumOfMulClass, wardCase);

            }
            else if (wardCode.Contains("ICU"))
            {

            }
            else if (wardCode.Contains("N"))
            {

            }

            Staff staff = new Staff()
            {
                StaffReal = staffReal,
                StaffData = staffData
            };

            WardCalculate wardCalc = new WardCalculate
            {
                WardPercentage = percentage,
                NumPatientKardex = sumPatientKardex,
                NumPatientClass = sumPatientClass
            };

            WardProduct result = new WardProduct
            {
                WardCode = wardCode,
                WardModel = wardModel,
                WardCalculate = wardCalc,
                Staffs = staff
            };

            return result;
        }

        public static WardProduct DtToRoomClass(DataTable dtRoomClass, string ward)
        {
            var result = new WardProduct();

            foreach (DataRow row in dtRoomClass.Rows)
            {

            }

            return result;
        }

        public static List<SetWardModel> DtToSetWardModelList(DataTable dt)
        {
            List<SetWardModel> results = new List<SetWardModel>();


            foreach (DataRow row in dt.Rows)
            {
                SetWardModel ward = new SetWardModel();
                ward.WardCode = row["WardCode"].ToString();
                ward.WardDesc = row["WardDesc"].ToString();

                results.Add(ward);
            }

            return results;
        }

        public static void SetWardToDb(List<SetWardModel> listSetWard)
        {
            using (var con = new SqlConnection(Constants.Svh21CHKConnectionString))
            {
                con.Open();
                using (var scope = new TransactionScope())
                {
                    string sqlIns = QueryString.InsertSetWardQueryString();

                    SqlCommand cmdIns = new SqlCommand(sqlIns, con);
                    cmdIns.Parameters.Add("@WardCode", SqlDbType.VarChar);
                    cmdIns.Parameters.Add("@WardDesc", SqlDbType.VarChar);

                    foreach (SetWardModel item in listSetWard)
                    {
                        cmdIns.Parameters["@WardCode"].Value = item.WardCode;
                        cmdIns.Parameters["@WardDesc"].Value = item.WardDesc;
                        cmdIns.ExecuteNonQuery();
                    }
                    scope.Complete();
                }
            }
        }

        public static DataTable GetDTForDisplayOnDataGrid<T>(IEnumerable<T> list)
        {
            DataTable dt = new DataTable();

            Type type = typeof(T);
            var properties = type.GetProperties();

            foreach(PropertyInfo info in properties)
            {
                dt.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for(int i=0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dt.Rows.Add(values);
            }

            var dtResult = dt.DefaultView.ToTable(false, "BED_Code", "PatientClass");
            dtResult.Columns["BED_Code"].ColumnName = "room";
            dtResult.Columns["PatientClass"].ColumnName = "cls";

            return dtResult;
        }

        public static void GetCurrentSetWard()
        {
            Constants.listSetWardModel = GetData.GetSetWards();
        }
        

    }
}
