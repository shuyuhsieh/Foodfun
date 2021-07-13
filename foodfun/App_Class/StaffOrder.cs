using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using foodfun.Models;


/// <summary>
/// 訂單類別
/// </summary>
public static class StaffOrder
{

    #region 公開屬性

    /// <summary>
    /// 又否有未結訂單
    /// </summary>




    #endregion

    #region 公用函數


    /// <summary>
    /// 取得訂單資訊
    /// </summary>
    public static List<StaffOrderViewModel> GetOrderList(DateTime date,bool isclosed)
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            List<Orders> orders = new List<Orders>();
            DateTime todayEnd = DateTime.Today.AddDays(1);

            if (date.Date == DateTime.Today.Date)
            {
                orders = db.Orders.Where(m => m.isclosed == isclosed)
                    .Where(m => m.SchedulOrderTime >= DateTime.Today && m.SchedulOrderTime < todayEnd)
                    .Where(m=>m.orderstatus_no != "TBC")
                    .OrderBy(m => m.SchedulOrderTime).ToList();
            }
            else if (date.Date > DateTime.Today.Date)
            {
                orders = db.Orders.Where(m => m.isclosed == isclosed)
                    .Where(m => m.SchedulOrderTime >= todayEnd)
                    .OrderBy(m => m.SchedulOrderTime).ToList();
            }


            List<StaffOrderViewModel> ordersViewModels = new List<StaffOrderViewModel>();

            if (orders != null)
            {
                int num = orders.Count();
                for (int i = 0; i < num; i++)
                {
                    string status_no = orders[i].orderstatus_no;
                    string meal_no = orders[i].mealservice_no;
                    string order_no = orders[i].order_no;
                    string paid_no = orders[i].paid_no;
                    string change_status = orders[i].orderstatus_no == "TBP" ? "可出餐" :"出餐";


                    ordersViewModels.Add(new StaffOrderViewModel()
                    {
                        orders = orders[i],
                        orderDetails = db.OrdersDetails.Where(m => m.order_no == order_no).OrderBy(m => m.rowid).ToList(),
                        orderstatus_name = db.OrderStatus.Where(m => m.orderstatus_no == status_no).FirstOrDefault().orderstatus_name,
                        change_status = change_status,
                        mealservice_name = db.MealService.Where(m => m.mealservice_no == meal_no).FirstOrDefault().mealservice_name,

                    }) ;
                    if (ordersViewModels[i].orders.ispaided==true)
                    {
                        ordersViewModels[i].paid_name = db.Payments.Where(m => m.paid_no == paid_no).FirstOrDefault().paid_name;
                    }
                }
            }

            return ordersViewModels;
        }
    }


    public static List<OrdersDetails> GetOrderDetails(string order_no)
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            var detailList = db.OrdersDetails.Where(m => m.order_no == order_no).ToList();
            return detailList;

        }
    }

    #endregion

    #region 私有函數


    #endregion



}
