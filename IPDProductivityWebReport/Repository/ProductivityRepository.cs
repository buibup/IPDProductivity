using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPDProductivityWebReport.Models;
using System.Data.SqlClient;
using IPDProductivityWebReport.Common;

namespace IPDProductivityWebReport.Repository
{
    public class ProductivityRepository : IProductivityRepository
    {
        string conString = Constants.Svh21CHKConnectionString;
        public ProductivityData GetProductivityData(string year)
        {
            ProductivityData prod = new ProductivityData();

            prod = GetData.GetProductivityData(year);

            return prod;
        }
    }
}