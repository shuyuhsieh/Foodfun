
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using foodfun.Models;

namespace foodfun.Controllers
{
    public class UserController : Controller
    {

        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            UserAccount.Logout();
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var data = db.Users
                    .Where(m => m.account_name == model.UserNo)
                    .Where(m => m.password == model.Password)
                    .Where(m => m.isvarify == true)
                    .FirstOrDefault();
                if (data == null)
                {
                    ModelState.AddModelError("UserNo", "登入帳號或密碼不合!!");
                    return View(model);
                }
                UserAccount.UserNo = model.UserNo;
                UserAccount.Login();
                return RedirectToAction("RedirectToUserPage");

            }
        }

        public ActionResult Logout()
        {
            UserAccount.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Guest")]
        public ActionResult Create()
        {
            string str_key = Guid.NewGuid().ToString().
                Replace("-", "").ToUpper().
                Substring(0, 20);

            Users model = new Users()
            {
                //birthday = DateTime.Now,
                isvarify = false,
                varify_code = str_key,
                role_no = "M"

            };

            return View(model);
        }
        [HttpPost]
        [LoginAuthorize(RoleList = "Guest")]
        public ActionResult Create(Users model)
        {

            if (!ModelState.IsValid) return View();

            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var temp = db.Users.Where(m => m.account_name == model.account_name).FirstOrDefault();
                if (temp !=null)
                {
                    ModelState.AddModelError("account_name", "帳號重複註冊");
                    return View(model);
                }
                temp = db.Users.Where(m => m.email == model.email).FirstOrDefault();
                if (temp != null)
                {
                    ModelState.AddModelError("email", "電子信箱重複註冊!!");
                    return View(model);
                }
                Users user = new Users();
                user.account_name = model.account_name;
                user.mname = model.mname;
                user.password = model.password;
                user.id = model.id;
                user.birthday = model.birthday;
                user.email = model.email;
                user.phone = model.phone;
                user.address = model.address;
                user.role_no = model.role_no;
                user.varify_code = model.varify_code;
                user.isvarify = model.isvarify;

                db.Users.Add(user);
                db.SaveChanges();

                using (AppMail mail = new AppMail())
                { mail.UserRegister(model.account_name); }

                TempData["HeaderText"] = "帳號註冊完成";
                TempData["MessageText"] = "帳號註冊成功，系統會發送一封認證信";
                return RedirectToAction("MessageText");
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Verify(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                bool verity = false;
                TempData["HeaderText"] = "使用者註冊電子信箱認證";
                var data = db.Users.Where(m => m.varify_code == id).FirstOrDefault();
                if (data == null)
                {
                    TempData["MessageText"] = "找不到驗證碼!!";
                    return RedirectToAction("MessageText");
                }
                verity = (bool)data.isvarify;
                if (verity == true)
                {
                    TempData["MessageText"] = "帳號電子信箱以驗證過，不可重複驗證!!";
                    return RedirectToAction("MessageText");
                }

                data.varify_code = "";
                data.isvarify = true;
                db.SaveChanges();
                TempData["MessageText"] = "帳號驗證成功!!";

                return RedirectToAction("MessageText");
            }

        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Member,Admin,Staff")]
        public ActionResult UserInfo(string no)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Users.Where(m => m.account_name == no).FirstOrDefault();
                return View(model);
            }

        }
        [HttpPost]
        [LoginAuthorize(RoleList = "Member")]
        public ActionResult UserInfo(Users model)
        {

            if (!ModelState.IsValid) return View(model);

            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                Users user = db.Users.Where(m => m.mno == model.mno).FirstOrDefault();
                user.mname = model.mname;
                user.password = model.password;
                user.birthday = model.birthday;
                user.email = model.email;
                user.phone = model.phone;
                user.address = model.address;

                db.SaveChanges();

                return View();
            }
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Member")]
        public ActionResult ResetPassword(string no)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                ResetPasswordViewModel model = new ResetPasswordViewModel();
                return View(model);
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Member")]
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



        [HttpGet]
        [LoginAuthorize(RoleList = "Guest")]
        public ActionResult ForgetPassword(string email)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {

                return View();
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Guest")]
        public ActionResult ForgetPassword(ForgetPasswordViewModel model)
        {

            if (!ModelState.IsValid) return View(model);

            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var data = db.Users.Where(m => m.email == model.email).FirstOrDefault();
                var userno = data.account_name;


                if (data == null)
                {
                    ModelState.AddModelError("email", "此電子郵件不存在!!");
                    return View(model);
                }
                else



                {
                    string allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ123456789";
                    int passwordLength = 6;//密碼長度
                    char[] chars = new char[passwordLength];
                    Random rd = new Random();

                    for (int i = 0; i < passwordLength; i++)
                    {
                        //allowedChars -> 這個String ，隨機取得一個字，丟給chars[i]
                        chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                    }

                    string password = new string(chars);

                    data.password = password;

                    db.SaveChanges();

                }
                using (AppMail mail = new AppMail())
                { mail.UserForget(userno); }

                TempData["HeaderText"] = "帳號及新密碼已發送至電子信箱";
                TempData["MessageText"] = "請登入後修改密碼!!";
                return RedirectToAction("MessageText");
            }
        }

        public ActionResult MessageText()
        {

            ViewBag.MessageText = TempData["MessageText"];
            ViewBag.HeaderText = TempData["HeaderText"];
            return View();
        }


        //後台登入設定
        public ActionResult RedirectToUserPage()
        {


            if (UserAccount.Role == EnumList.LoginRole.Admin) return RedirectToAction("OnlineOrder", "StaffOrder", new { area = "Staff" });
            if (UserAccount.Role == EnumList.LoginRole.Staff) return RedirectToAction("OnlineOrder", "StaffOrder", new { area = "Staff" });
            return RedirectToAction("Index", "Home");
        }

    }
}