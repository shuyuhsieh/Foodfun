using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;

namespace foodfun.Areas.Admin.Controllers
{
    public class MemberBackController : Controller
    {
        // GET: Admin/MemberBack
        public ActionResult Index()
        { 
            using (GoPASTAEntities db = new GoPASTAEntities())
            
          
            {
               
                return View(db.Users.OrderBy(m => m.mno).ToList());

            }
        }

        public ActionResult Create()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                ViewBag.RoleDropdownList = Backend.RoleDropdownList();
                Users model = new Users();

                return View(model);

            }
        }

        [HttpPost]
        public ActionResult Create(Users model)
        {
            if (!ModelState.IsValid) return View(model);


            using (GoPASTAEntities db = new GoPASTAEntities())
            {

                db.Users.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Edit(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
               
                var models = db.Users.Where(m => m.mno == id).FirstOrDefault();
                return View(models);
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Edit(Users model)
        {

            if (!ModelState.IsValid) return View(model);
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                // var data = db.Users.Where(m => m.property_no == model.property_no).FirstOrDefault();
                var data = db.Users.Where(m => m.mno == model.mno).FirstOrDefault();
                
                data.mname = model.mname;
                data.account_name = model.account_name;
                data.password = model.password;
                data.id = model.id;
                data.birthday = model.birthday;
                data.phone = model.phone;
                data.address = model.address;
                data.email = model.email;
                data.role_no = model.role_no;
                data.varify_code = model.varify_code;
                data.isvarify = model.isvarify; 
                data.isvalidate = model.isvalidate;


                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        [HttpGet]

        public ActionResult Delete(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Users.Where(m => m.mno == id).FirstOrDefault();
                if (model != null)
                {
                    db.Users.Remove(model);
                    db.SaveChanges();
                }
                //return RedirectToAction("Index", "Product");    //Product為controller可略,會以目前controller為主
                return RedirectToAction("Index");
            }
        }
    }
}