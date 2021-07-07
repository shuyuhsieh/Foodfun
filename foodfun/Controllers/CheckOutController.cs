using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;

namespace foodfun.Controllers
{
    public class CheckOutController : Controller
    {
        GoPASTAEntities db;

        public CheckOutController()
        {
            db = new GoPASTAEntities();
        }

        [AllowAnonymous]
        public ActionResult MealService()
        {
            MealServiceViewModel model = new MealServiceViewModel()
            {
                mealServiceList = db.MealService.OrderBy(m => m.mealservice_no).ToList()

            };
            return View(model);

        }

        // GET: CheckOut
        [AllowAnonymous]
        [HttpPost]
        public ActionResult MealService(MealServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.mealServiceList == null)
                {
                    model.mealServiceList = db.MealService.OrderBy(m => m.mealservice_no).ToList();
                }
                return View(model);
            }

            TempData["mealservice_no"] = model.mealservice_no;
            if (model.mealservice_no == "A")
            {
                TempData["Table_no"] = model.Table_no;
            }
            else if (model.mealservice_no == "C")
            {
                TempData["receive_address"] = model.receive_address;
            }

            TempData["SchedulOrderTime"] = model.SchedulOrderTime;
            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {

            ConfirmationViewModel orderInfoView = new ConfirmationViewModel()
            {
                Order = new Orders(),
                PaymentsList = db.Payments.OrderBy(m => m.paid_no).ToList()
            };

            orderInfoView.Order.total = Cart.Totals;
            Users userinfo = new Users();

            userinfo = db.Users.Where(m => m.account_name == UserAccount.UserNo).FirstOrDefault();

            if (UserAccount.IsLogin)
            {
                orderInfoView.Cart = db.Carts.Where(m => m.mno == UserAccount.UserNo).ToList();
                orderInfoView.Order.mno = UserAccount.UserNo;
                orderInfoView.Order.receive_name = userinfo.mname;
                orderInfoView.Order.receive_phone = userinfo.phone;
            }
            else
            {
                orderInfoView.Cart = db.Carts.Where(m => m.cart_lotno == Cart.LotNo).ToList();
            }
            orderInfoView.Order.mealservice_no = TempData["mealservice_no"].ToString();
            orderInfoView.Order.SchedulOrderTime = Convert.ToDateTime(TempData["SchedulOrderTime"]);
            string mealservice_no = orderInfoView.Order.mealservice_no;

            if (mealservice_no == "A")
            {
                orderInfoView.Order.table_no = TempData["Table_no"].ToString();

            }
            else if (mealservice_no == "C")
            {
                orderInfoView.Order.receive_address = TempData["receive_address"].ToString();
            }

            var mealservice = db.MealService.Where(m => m.mealservice_no == mealservice_no).FirstOrDefault();
            orderInfoView.mealservice_name = mealservice.mealservice_name;


            return View(orderInfoView);

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Confirmation(ConfirmationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.PaymentsList == null)
                {
                    model.PaymentsList = db.Payments.OrderBy(m => m.paid_no).ToList();
                }
                return View(model);
            }
            if (model.Order.paid_no == "P01") Cart.AddNewOrder(model, false);
            else Cart.AddNewOrder(model, true);

            Cart.AddNewOrder(model, true);
            string a = Cart.GetOrderNO();
            Cart.AddNewOrderDetail();



            return RedirectToAction("Index", "Home");
        }
    }
}