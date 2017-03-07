using InterSystems.Data.CacheClient;
using System;
using System.Data;

namespace SendEmailCSI.DA.InterSystems
{
    #region IntersystemsDa class use for access data from cache intersystems
    public class InterSystemsDA
    {

        #region return data table from command string : require(command string, connection string)
        public static DataTable DataTableBindDataCommand(string cmdString, string connectionString)
        {
            DataTable dt = new DataTable();

            using(CacheConnection con = new CacheConnection(connectionString))
            {
                con.Open();
                    using (CacheDataAdapter da = new CacheDataAdapter(cmdString,con))
                    {
                        da.Fill(dt);
                    }
            }

            return dt;
        }
        #endregion

        #region return data set from command string : require(command string, connection string)
        public static DataSet DataSetBindDataCommand(string cmdString, string connectionString)
        {
            DataSet ds = new DataSet();

            using (CacheConnection con = new CacheConnection(connectionString))
            {
                using(CacheDataAdapter adt = new CacheDataAdapter(cmdString,con))
                {
                    con.Open();
                    adt.Fill(ds);
                }
            }

            return ds;
        }
        #endregion

        public static string ExecuteScalarBindDataCommand(string cmdString, string connectionString)
        {
            string result = string.Empty;

            using(var con = new CacheConnection(connectionString))
            {
                using(var cmd = new CacheCommand(cmdString, con))
                {
                    con.Open();
                    result = Convert.ToString(cmd.ExecuteScalar());
                }
            }

            return result;
        }
    }
    #endregion
}
