﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IPDProductivityWebReport.DA
{
    public class SqlServerDA
    {
        public static DataTable DataTableBindDataCommand(string cmdString, string conString)
        {
            DataTable dt = new DataTable();

            using (var con = new SqlConnection(conString))
            {
                using (var adp = new SqlDataAdapter(cmdString, con))
                {
                    con.Open();
                    adp.Fill(dt);
                }
            }
            return dt;
        }

        public static DataSet DataSetBindDataCommand(string cmdString, string conString)
        {
            DataSet ds = new DataSet();

            using (var con = new SqlConnection(conString))
            {
                using (var adp = new SqlDataAdapter(cmdString, con))
                {
                    con.Open();
                    adp.Fill(ds);
                }
            }
            return ds;
        }

        public static string ExecuteScalarBindDataCommand(string cmdString, string conString)
        {
            string result = "";

            using (var con = new SqlConnection(conString))
            {
                using (var cmd = new SqlCommand(cmdString, con))
                {
                    con.Open();
                    result = Convert.ToString(cmd.ExecuteScalar());
                }
            }

            return result;
        }

        public static bool ExecuteNonQuery(string cmdString, string conString)
        {
            bool flag = false;
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = new SqlCommand(cmdString, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    flag = true;
                }
            }

            return flag;
        }
    }
}
