using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;

namespace foodfun.Areas.Admin.Controllers
{
    public class CategorysController : Controller
    {
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Index(int id = 0)
        {
            SetSessionValue(id);
            return View();
        }


        public ActionResult Create()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {


                Categorys model = new Categorys();

                return View(model);

            }
        }

        [HttpPost]
        public ActionResult Create(Categorys model)
        {
            if (!ModelState.IsValid) return View(model);


            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                db.Categorys.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = model.parentid });
            }
        }



        public JsonResult GetDataList()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                int int_pid = int.Parse(Session["CategoryID"].ToString());
                var models = db.Categorys
                    .Where(m => m.parentid == int_pid)
                    .OrderBy(m => m.category_no).ToList();
                return Json(new { data = models }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
              //  ViewBag.ParentIdDropdownList = Backend.ParentIdDropdownList();
                if (id == 0)
                {

                    Categorys new_model = new Categorys();
                    return View(new_model);
                }

                var models = db.Categorys.Where(m => m.rowid == id).FirstOrDefault();
                return View(models);
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Edit(Categorys models)
        {

            if (ModelState.IsValid)
            {
                using (GoPASTAEntities db = new GoPASTAEntities())
                {
                    if (models.rowid > 0)
                    {
                        //Edit 
                        var Categorys = db.Categorys.Where(m => m.rowid == models.rowid).FirstOrDefault();
                        if (Categorys != null)
                        {
                            Categorys.category_no = models.category_no;
                            Categorys.category_name = models.category_name;
                        }
                    }
                    else
                    {
                        //Save
                        if (Session["CategoryID"] == null)
                        { models.parentid = 0; }
                        else
                        { models.parentid = int.Parse(Session["CategoryID"].ToString()); }
                        db.Categorys.Add(models);
                    }
                    db.SaveChanges();

                }
            }
            return RedirectToAction("Index", new { id = models.parentid });
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Delete(int id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Categorys.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        //[ActionName("Delete")]
        [LoginAuthorize(RoleList = "Admin")]
        public JsonResult DeleteData(int id)
        {
            bool status = false;
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Categorys.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    db.Categorys.Remove(model);
                    db.SaveChanges();
                    status = true;
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
            //return new JsonResult { Data = new { status = status } };
        }

        public ActionResult ReturnToParent()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                int int_pid = int.Parse(Session["CategoryID"].ToString());
                var model = db.Categorys.Where(m => m.rowid == int_pid).FirstOrDefault();
                if (model != null)
                {
                    int_pid = model.parentid.GetValueOrDefault();
                }
                return RedirectToAction("Index", "Categorys", new { id = int_pid });
            }
        }

        private void SetSessionValue(int id = 0)
        {
            Session["CategoryID"] = id;
            Session["CategoryNo"] = "";
            Session["CategoryName"] = "";
            Session["ParentID"] = "";
            Session["ParentName"] = "";
            int int_pid = 0;
            string str_parent_id = id.ToString();
            string str_parent_name = "";
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Categorys.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    Session["CategoryNo"] = model.category_no;
                    Session["CategoryName"] = model.category_name;
                    int_pid = model.parentid.GetValueOrDefault();
                    str_parent_name = model.category_name;

                    while (int_pid > 0)
                    {
                        var parent = db.Categorys.Where(m => m.rowid == int_pid).FirstOrDefault();
                        if (parent == null) break;

                        int_pid = parent.parentid.GetValueOrDefault();
                        str_parent_id = parent.rowid.ToString() + "," + str_parent_id;
                        str_parent_name = parent.category_name + "," + str_parent_name;
                    }
                }
            }
            Session["ParentID"] = str_parent_id;
            Session["ParentName"] = str_parent_name;
        }
    }
}