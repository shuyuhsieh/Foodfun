using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;

namespace foodfun.Areas.Staff.Controllers
{
    public class StaffOrderController : Controller
    {

        [LoginAuthorize(RoleList = "Admin/Staff")]
        // GET: Staff/StaffOrder
        public ActionResult OnlineOrder()
        {

            return View(StaffOrder.GetOrderList(DateTime.Today, false));
        }


        [LoginAuthorize(RoleList = "Admin/Staff")]
        public ActionResult ReserveOrder()
        {


            return View(StaffOrder.GetOrderList(DateTime.Today, true));

        }


        [LoginAuthorize(RoleList = "Admin/Staff")]
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            int result = 0;
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var data = db.Orders.Where(m => m.order_no == id).FirstOrDefault();
                if (data.orderstatus_no == "TBP")
                {
                    data.orderstatus_no = "ALC";
                }
                else if (data.orderstatus_no == "ALC")
                {
                    data.orderstatus_no = "ALD";
                    if (data.ispaided == true)
                    {
                        data.isclosed = true;
                        db.SaveChanges();
                        //回傳0 移除這筆資料
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
                db.SaveChanges();
                StaffOrderViewModel thisData = new StaffOrderViewModel();
                thisData.orders = data;
                thisData.change_status = "出餐完成";
                thisData.orderstatus_name = db.OrderStatus.Where(m => m.orderstatus_no == data.orderstatus_no).FirstOrDefault().orderstatus_name;

                return Json(thisData, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult GetOrderDedail(string id) {

            using (GoPASTAEntities db = new GoPASTAEntities()) 
            {
                var data = db.OrdersDetails.Where(m => m.order_no == id).ToList();

                for (int i = 0; i < data.Count(); i++)
                {
                    data[i].product_no = Shop.GetProductName(data[i].product_no);
                }
              
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        
        [LoginAuthorize(RoleList = "Admin/Staff")]
        public ActionResult TodayHistory()
        {


            return View(StaffOrder.GetOrderList(DateTime.Today, true));

        }


    }
}