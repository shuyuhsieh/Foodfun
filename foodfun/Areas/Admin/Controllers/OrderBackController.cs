using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace foodfun.Areas.Admin.Controllers
{
    public class OrderBackController : Controller
    {
        // GET: Admin/HistoryOrder
        public ActionResult Index()
        {
            return View(StaffOrder.GetOrderList(DateTime.Today.AddDays(-1), true));
        }
    }
}