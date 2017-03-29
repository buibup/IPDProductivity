using IPDProductivityWebReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDProductivityWebReport.Repository
{
    public interface IProductivityRepository
    {
        ProductivityData GetProductivityData(string year);
    }
}