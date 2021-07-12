using foodfun.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}