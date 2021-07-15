using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using foodfun.Models;


/// <summary>
/// 訂單類別
/// </summary>
public static class Order
{

    #region 公開屬性

    /// <summary>
    /// 是否有未結訂單
    /// </summary>
    public static bool IsNowOrder { get { return GetNowOrder(); } }
    //今日銷售額



    #endregion

    #region 公用函數
    public static List<OrdersViewModel> GetOrderList(bool isclosed)
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            var order = db.Orders.Where(m => m.mno == UserAccount.UserNo && m.isclosed == isclosed)
                    .OrderBy(m => m.mno).ToList();
            int num = order.Count();

            List<OrdersViewModel> ordersViewModels = new List<OrdersViewModel>();
            for (int i = 0; i < num; i++)
            {
                string status_no = order[i].orderstatus_no;
                string meal_no = order[i].mealservice_no;
                string order_no = order[i].order_no;

                ordersViewModels.Add(new OrdersViewModel()
                {

                    order_no = order_no,
                    order_date = order[i].order_date,
                    total = order[i].total,
                    ispaided = order[i].ispaided,
                    orderDetails = db.OrdersDetails.Where(m => m.order_no == order_no).OrderBy(m => m.rowid).ToList(),
                    orderstatus_name = db.OrderStatus.Where(m => m.orderstatus_no == status_no).FirstOrDefault().orderstatus_name,
                    mealservice_name = db.MealService.Where(m => m.mealservice_no == meal_no).FirstOrDefault().mealservice_name,

                });
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


    //取區間銷售金額
    public static int GetRevenue(DateTime start, DateTime end)
    {

        int sum = 0;
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            var data_total = db.Orders.Where(m => m.SchedulOrderTime >= start && m.SchedulOrderTime < end).Select(m => m.total).ToList();
            sum = Convert.ToInt32(data_total.Sum());
        }
        return sum;
    }

    //取區間銷售數量
    public static int GetSaleItemNum(DateTime start, DateTime end)
    {
        int qty = 0;

        using (GoPASTAEntities db = new GoPASTAEntities())
        {

            var data_orderNo = db.Orders.Where(m => m.SchedulOrderTime >= start && m.SchedulOrderTime < end).Select(m => m.order_no).ToList();

            foreach (var item in data_orderNo)
            {
                var data_qty = db.OrdersDetails.Where(m => m.order_no == item).Select(m => m.qty).ToList();
                int order_qty = (int)data_qty.Sum();
                qty += order_qty;
            }
        }
        return qty;
    }


    //取區間來客
    public static int GetNumOfCust(DateTime start, DateTime end)
    {
        int sum = 0;
   
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            var data = db.Orders.Where(m => m.SchedulOrderTime >= start && m.SchedulOrderTime < end).ToList();
            sum = Convert.ToInt32(data.Count);
        }
        return sum;
    }








    #endregion

    #region 私有函數
    private static bool GetNowOrder()
    {
        bool now_order;
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            var data = db.Orders.Where(m => m.mno == UserAccount.UserNo && m.isclosed == false).ToList();
            int data_num = data.Count();

            now_order = data_num > 0 ? true : false;
        }
        return now_order;
    }


    #endregion



}
