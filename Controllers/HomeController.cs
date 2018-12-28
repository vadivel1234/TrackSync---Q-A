using Syncfusion.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SearchCollection(DataManager dataManager, [Bind(Prefix = "$field")]string field,[Bind(Prefix = "$startdate")] string startDate, [Bind(Prefix = "$enddate")] string endDate)
        {
            this.TempData["customerStartDate"] = startDate;
            this.TempData["customerEndDate"] = endDate;

            QueryResultModel query = new QueryResultModel();
            var list = query.GetSerarchQuereies(dataManager, field, startDate, endDate);
            var customerCollection = Json(new { result = list, count = query.totalCount }, JsonRequestBehavior.AllowGet);
            customerCollection.MaxJsonLength = int.MaxValue;
            return customerCollection;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}