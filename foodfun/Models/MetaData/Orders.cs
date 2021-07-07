using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    [MetadataType(typeof(OrdersMetaData))]
    public partial class Orders
    {
        public class OrdersMetaData
        {
            [Key]
            public int rowid { get; set; }

            [Display(Name = "訂單編號")]
            public string order_no { get; set; }

            [Display(Name = "訂單日期")]
            public Nullable<System.DateTime> order_date { get; set; }

            [Display(Name = "會員編號")]
            public string mno { get; set; }

            [Display(Name = "折扣")]
            public Nullable<decimal> discount_rate { get; set; }

            [Display(Name = "稅額")]
            public Nullable<decimal> taxs { get; set; }


            [DisplayFormat(DataFormatString =("{0:C0}"))]
            [Display(Name = "總計")]
            public Nullable<decimal> total { get; set; }

            [Display(Name = "訂單狀態編碼")]
            public string orderstatus_no { get; set; }

            [Required]
            [Display(Name = "用餐方式編碼")]
            public string mealservice_no { get; set; }


             [DisplayFormat(DataFormatString =("{0:yyyy/MM/dd HH:mm}"))]
            [Display(Name = "取餐時間")]
            public Nullable<System.DateTime> SchedulOrderTime { get; set; }

            [Display(Name = "桌號")]
            public string table_no { get; set; }

            [Display(Name ="付款方式編碼")]
            public string paid_no { get; set; }

            [Display(Name = "收件人姓名")]
            public string receive_name { get; set; }

            [Display(Name = "收件人電話")]
            public string receive_phone { get; set; }

            [Display(Name = "收件地址")]
            public string receive_address { get; set; }

            [Display(Name = "訂單是否結案")]
            public Nullable<bool> isclosed { get; set; }

            [Display(Name = "是否結帳")]
            public Nullable<bool> ispaided { get; set; }

            [Display(Name = "取消訂單")]
            public Nullable<bool> cancelorder { get; set; }
            [Display(Name = "取消訂單原因")]
            public string cancelreason { get; set; }
            [Display(Name = "備註")]
            public string remark { get; set; }

        }
    }
}