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
        // GET: Admin/Admin
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Index()
        {
            ViewBag.TodayRevenue = Order.GetRevenue(DateTime.Today, DateTime.Today);
            ViewBag.TodaySaleItemNum = Order.GetSaleItemNum(DateTime.Today, DateTime.Today);
            ViewBag.TodayNumOfCust = Order.GetNumOfCust(DateTime.Today, DateTime.Today);
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


        public List<string> GetYearMonthNameList()
        {


            List<string> nameList = new List<string>();
            List<string> monthList = new List<string>() { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二" };
            string str_name = "";
            for (int i = 0; i < 12; i++)
            {
                str_name = string.Format("{0}月", monthList[i]);
                nameList.Add(str_name);
            }
            return nameList;
        }











    }
}