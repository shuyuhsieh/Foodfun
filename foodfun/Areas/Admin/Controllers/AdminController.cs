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
        [LoginAuthorize(RoleList = "Admin,Staff")]
        public ActionResult ResetPassword()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                ResetPasswordViewModel model = new ResetPasswordViewModel();
                return View(model);
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Admin,Staff")]
        public JsonResult ResetPassword(ResetPasswordViewModel model)
        {
            //if (!@ModelState.IsValid) return View(model);

            using (GoPASTAEntities db = new GoPASTAEntities())
            {
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
        }

    }
}