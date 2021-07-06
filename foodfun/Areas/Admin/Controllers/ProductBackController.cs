using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;
using PagedList;



namespace foodfun.Areas.Admin.Controllers
{
    public class ProductBackController : Controller
    {
        GoPASTAEntities db = new GoPASTAEntities();
        public ActionResult Index()
        {
            // return View(repo_product.ReadAll().OrderBy(m => m.mno));

          var product=db.Products.OrderBy(m => m.product_no).ToList();

            var model = new List<ProductBackViewModel>();

            for (int i = 0; i < product.Count; i++)
            {
                model.Add(new ProductBackViewModel()
                {
                    products=product[i],
                    category_name = Backend.GetCodeName(product[i].category_no)
                });
            }
                return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                ViewBag.CtgryDropdownList = Backend.CtgryDropdownList();
                Products model = new Products();
                return View(model);

            }
        }

        [HttpPost]
        public ActionResult Create(Products model)
        {
            if (!ModelState.IsValid) return View(model);
           

            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                model.product_no = Backend.CreateProductNo(model.category_no);
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
       
        public ActionResult Edit(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                ViewBag.CtgryDropdownList = Backend.CtgryDropdownList();
                var model = db.Products.Where(m => m.product_no == id).FirstOrDefault();
                return View(model);
            }
        }



        [HttpPost]
        
        public ActionResult Edit(Products model)
        {
            if (!ModelState.IsValid) return View(model);
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var data = db.Products.Where(m => m.product_no == model.product_no).FirstOrDefault();
                data.product_no = model.product_no;
                data.product_name = model.product_name;
                data.product_spec = model.product_spec;
                data.category_no = model.category_no;
                data.price_avgcost = model.price_avgcost;
                data.price_sale = model.price_sale;
                data.discount_price = model.discount_price;
                data.stock_qty = model.stock_qty;
                data.image_path = model.image_path;
                data.description = model.description;
                data.browse_count = model.browse_count;
                data.ishot = model.ishot;
                data.issale = model.issale;
                data.istop = model.istop;
                data.remark = model.remark;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
        [HttpGet]
      
        public ActionResult Delete(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Products.Where(m => m.product_no == id).FirstOrDefault();
                if (model != null)
                {
                    db.Products.Remove(model);
                    db.SaveChanges();
                }
                //return RedirectToAction("Index", "Product");    //Product為controller可略,會以目前controller為主
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
                ImageService.ImageName = model.product_no;
                ImageService.ImageExtention = "jpg";
                ImageService.UploadImageMode = true;
                return RedirectToAction("UploadImage", "Image");
            }
        }




    }
}