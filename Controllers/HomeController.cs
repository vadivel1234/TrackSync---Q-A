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

        public JsonResult SearchCollection(DataManager dataManager)
        {
            var list = QueryResultModel.GetQueryResult("grid");
            var customerCollection = Json(new { result = list, count = list.Count }, JsonRequestBehavior.AllowGet);
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