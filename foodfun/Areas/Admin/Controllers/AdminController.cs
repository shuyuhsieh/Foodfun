using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;

namespace foodfun.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        GoPASTAEntities db = new GoPASTAEntities();

        List<string> TimeList = new List<string>();
        List<int> RevenueList = new List<int>();

        // GET: Admin/Admin
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Index()
        {
            ViewBag.TodayRevenue = Order.GetRevenue(DateTime.Today, DateTime.Today.AddDays(1));
            ViewBag.TodaySaleItemNum = Order.GetSaleItemNum(DateTime.Today, DateTime.Today.AddDays(1));
            ViewBag.TodayNumOfCust = Order.GetNumOfCust(DateTime.Today, DateTime.Today.AddDays(1));
            int TodaySalePerCust;
            if (ViewBag.TodayNumOfCust == 0)
            {
                TodaySalePerCust = 0;
            }
            else
            {
                TodaySalePerCust = ViewBag.TodayRevenue / ViewBag.TodayNumOfCust;
            }

            ViewBag.TodaySalePerCust = TodaySalePerCust;

            GetTodayHourList();
            ViewBag.TimeList = Newtonsoft.Json.JsonConvert.SerializeObject(TimeList);
            ViewBag.RevenueList = Newtonsoft.Json.JsonConvert.SerializeObject(RevenueList);

            return View();

        }
        [HttpGet]
        [LoginAuthorize(RoleList = "Admin,Staff")]
        public ActionResult UserInfo()
        {

            var model = db.Users.Where(m => m.account_name == UserAccount.UserNo).FirstOrDefault();
            return View(model);

        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Admin,Staff")]
        public ActionResult UserInfo(Users model)
        {

            if (!ModelState.IsValid) return View(model);


            Users user = db.Users.Where(m => m.mno == model.mno).FirstOrDefault();
            user.mname = model.mname;
            user.birthday = model.birthday;
            user.email = model.email;
            user.phone = model.phone;
            user.address = model.address;

            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Admin,Staff")]
        public ActionResult ResetPassword()
        {

            ResetPasswordViewModel model = new ResetPasswordViewModel();
            return View(model);

        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Admin,Staff")]
        public JsonResult ResetPassword(ResetPasswordViewModel model)
        {
            //if (!@ModelState.IsValid) return View(model);


            bool result = false;
            Users user = db.Users.Where(m => m.account_name == UserAccount.UserNo).FirstOrDefault();
            if (user != null)
            {
                if (model.NewPassword == user.password)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    user.password = model.NewPassword;
                    db.SaveChanges();
                    result = true;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);


        }


        public void GetTodayHourList()
        {
            TimeSpan opentime = (TimeSpan)db.Company.Where(m => m.rowid == 1).Select(m => m.opentime).FirstOrDefault();
            TimeSpan closetime = (TimeSpan)db.Company.Where(m => m.rowid == 1).Select(m => m.closetime).FirstOrDefault();

            int int_open = opentime.Hours;
            int int_close = closetime.Hours;
            DateTime TimeStart = DateTime.Today.AddHours(int_open);
            DateTime TimeEnd = TimeStart.AddHours(1);
            for (int i = int_open; i < int_close; i += 1)
            {
                int iplus = i + 1;
                int revenue = Order.GetRevenue(TimeStart, TimeEnd);
                TimeStart = TimeStart.AddHours(1);
                TimeEnd = TimeEnd.AddHours(1);
                TimeList.Add($"{i} - {iplus}");
                RevenueList.Add(revenue);
            }

        }











    }
}