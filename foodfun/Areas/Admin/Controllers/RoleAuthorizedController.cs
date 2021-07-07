using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;

namespace foodfun.Areas.Admin.Controllers
{
    public class RoleAuthorizedController : Controller
    {
        public ActionResult Index()
        {
           
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                return View(db.Users.OrderBy(m => m.role_no).ToList());

            }
        }



        [HttpGet]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Edit(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                
                var models = db.Users.Where(m => m.role_no == id).FirstOrDefault();
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
               
                var data = db.Users.Where(m => m.role_no == model.role_no).FirstOrDefault();              
                data.role_no = model.role_no;
              

                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

    }
}