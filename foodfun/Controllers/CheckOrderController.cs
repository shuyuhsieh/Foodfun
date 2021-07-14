using foodfun.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;

namespace foodfun.Controllers
{
    public class CheckOrderController : Controller
    {
        // GET: CheckOrder
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CustomerConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [order_no],[order_date],[mno],[total],[mealservice_no] FROM [dbo].[Orders] WHERE [orderstatus_no] = 'TBC'", connection))
                {
                    command.Notification = null;
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed) connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    var listCus = reader.Cast<IDataRecord>()
                        .Select(x => new
                        {
                            order_no = (string)x["order_no"]/*,*/
                            //order_date = (DateTime)x["order_date"],
                            //mno = (string)x["mno"],
                            //total = (decimal)x["total"],
                            //mealservice_no = (string)x["mealservice_no"],
                        }).ToList();

                    return Json(new { listCus = listCus }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            CusHub.Show();
        }



        public ActionResult GetNewOrder(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var orderInfo = db.Orders.Where(m => m.order_no == id).FirstOrDefault();
                var orderInfoList = db.OrdersDetails.Where(m => m.order_no == id).ToList();
                OrdersViewModel order = new OrdersViewModel()
                {
                    order_no = id,
                    total = orderInfo.total,
                    SchedulOrderTime = (DateTime)orderInfo.SchedulOrderTime,
                    ispaided = orderInfo.ispaided,
                    mealservice_name = db.MealService.Where(m => m.mealservice_no == orderInfo.mealservice_no).Select(m => m.mealservice_name).FirstOrDefault(),
                    orderDetails = orderInfoList
                };

                return View(order);
            }

        }

        public JsonResult ConfirmOrder(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var data = db.Orders.Where(m => m.order_no == id).FirstOrDefault();
                data.orderstatus_no = "TBP";
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }


    }
}