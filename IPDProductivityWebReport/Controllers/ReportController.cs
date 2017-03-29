using IPDProductivityWebReport.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPDProductivityWebReport.Controllers
{
    public class ReportController : Controller
    {

        ProductivityRepository proRepo = new ProductivityRepository();
        // GET: Report
        public ActionResult Index()
        {
            string year = "2016";

            return View(proRepo.GetProductivityData(year));
        }
    }
}