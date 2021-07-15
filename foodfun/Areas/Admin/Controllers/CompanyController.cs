using foodfun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace foodfun.Areas.Admin.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Admin/Company
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Index()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                Company model = db.Company.OrderBy(m => m.rowid).FirstOrDefault();

                //if (model.rowid == 1)
                //{
                //    return View();
                //}
                //else
                    return View(model);
            }

        }
        

        [HttpPost]

        public ActionResult Index(Company model)
        {
            if (!ModelState.IsValid) return View(model);
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var models = db.Company.Where(m => m.rowid == 1).FirstOrDefault();
                models.company_id = model.company_id;
                models.brandname = model.brandname;
                models.tel = model.tel;
                models.fax = model.fax;
                models.address = model.address;
                models.opentime = model.opentime;
                models.closetime = model.closetime;
                models.public_holiday = model.public_holiday;
                models.description = model.description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Upload( string product_no)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Products.Where(m => m.product_no == product_no).FirstOrDefault();

                ImageService.ReturnAction("", "ProductBack", "Index");               
                ImageService.ImageTitle = string.Format("{0}  圖片上傳", model.product_name);
                ImageService.ImageFolder = "~/img/product";
                ImageService.ImageSubFolder = model.category_no;
                ImageService.ImageName = model.product_no; //給檔名
                ImageService.ImageExtention = "jpg";
                ImageService.UploadImageMode = true;
                return RedirectToAction("UploadImage", "Image");
            }
        }
    }
}