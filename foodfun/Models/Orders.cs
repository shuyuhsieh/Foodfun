//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace foodfun.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        public int rowid { get; set; }
        public string order_no { get; set; }
        public Nullable<System.DateTime> order_date { get; set; }
        public string mno { get; set; }
        public Nullable<decimal> discount_rate { get; set; }
        public Nullable<decimal> taxs { get; set; }
        public Nullable<decimal> total { get; set; }
        public string orderstatus_no { get; set; }
        public string mealservice_no { get; set; }
        public Nullable<System.DateTime> SchedulOrderTime { get; set; }
        public string table_no { get; set; }
        public string paid_no { get; set; }
        public string receive_name { get; set; }
        public string receive_phone { get; set; }
        public string receive_address { get; set; }
        public Nullable<bool> isclosed { get; set; }
        public Nullable<bool> ispaided { get; set; }
        public string order_guid { get; set; }
        public Nullable<bool> cancelorder { get; set; }
        public string cancelreason { get; set; }
        public string remark { get; set; }
    }
}
