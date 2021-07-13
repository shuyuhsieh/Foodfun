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
        // GET: Admin/Admin
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Admin,Staff")]
        public ActionResult UserInfo()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Users.Where(m => m.account_name == UserAccount.UserNo).FirstOrDefault();
                return View(model);
            }

        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Admin,Staff")]
        public ActionResult UserInfo(Users model)
        {

            if (!ModelState.IsValid) return View(model);

            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                Users user = db.Users.Where(m => m.mno == model.mno).FirstOrDefault();
                user.mname = model.mname;
                user.birthday = model.birthday;
                user.email = model.email;
                user.phone = model.phone;
                user.address = model.address;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                ResetPasswordViewModel model = new ResetPasswordViewModel();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {

            if (!ModelState.IsValid) return View(model);

            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                Users user = db.Users.Where(m => m.account_name == UserAccount.UserNo).FirstOrDefault();
                if (user != null)
                {
                    user.password = model.NewPassword;
                    db.SaveChanges();

                }
                TempData["HeaderText"] = "密碼變更完成";
                TempData["MessageText"] = "密碼已變更，下次請使用新密碼登入!!";
                return RedirectToAction("MessageText");
            }
        }

    }
}