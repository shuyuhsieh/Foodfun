using foodfun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace foodfun.Areas.Admin.Controllers
{
    public class AboutController : Controller
    {
        GoPASTAEntities db = new GoPASTAEntities();
        // GET: Admin/About
        public ActionResult Index()
        {
            var data = db.About.OrderBy(m => m.rowid).ToList();

            return View(data);

        }

        [HttpGet]
        public ActionResult Create()
        {
            About model = new About();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(About model)
        {
            if (!ModelState.IsValid) return View(model);

            db.About.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]

        public ActionResult Edit(int id)
        {
            var model = db.About.Where(m => m.sortid == id).FirstOrDefault();
            return View(model);
        }


        [HttpPost]

        public ActionResult Edit(About model)
        {
            if (!ModelState.IsValid) return View(model);

            var data = db.About.Where(m => m.sortid == model.sortid).FirstOrDefault();
            data.rowid = model.rowid;
            data.corevalue_title = model.corevalue_title;
            data.corevalue_descpt = model.corevalue_descpt;

            db.SaveChanges();

            return RedirectToAction("Index");

        }
        [HttpGet]

        public ActionResult Delete(int id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.About.Where(m => m.sortid == id).FirstOrDefault();
                if (model != null)
                {
                    db.About.Remove(model);
                    db.SaveChanges();
                }
                //return RedirectToAction("Index", "Product");    //Product為controller可略,會以目前controller為主
                return RedirectToAction("Index");
            }

        }

        public ActionResult Upload(string sortid,string info)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                int int_sortid = Convert.ToInt32(sortid);
                var model = db.About.Where(m => m.sortid == int_sortid).FirstOrDefault();

                ImageService.ReturnAction("", "About", "Index");
                ImageService.ImageTitle = string.Format(" 圖片上傳");
                ImageService.ImageFolder = "../../img";
                ImageService.ImageSubFolder = "about";
                ImageService.ImageName =  info+sortid;
                ImageService.ImageExtention = "jpg";
                ImageService.UploadImageMode = true;
                return RedirectToAction("UploadImage", "Image");
            }
        }


    }
}