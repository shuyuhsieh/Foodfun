using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    [MetadataType(typeof(CompanyMetaData))]
    public partial class Company
    {        
        public partial class CompanyMetaData
        {
            [Key]
            public int rowid { get; set; }
            [Display(Name = "公司統編")]
            public Nullable<int> company_id { get; set; }
            [Display(Name = "公司名稱")]
            public string brandname { get; set; }
            [Display(Name = "電  話")]
            public string tel { get; set; }
            [Display(Name = "傳  真")]
            public string fax { get; set; }
            [Display(Name = "地  址")]
            public string address { get; set; }
            [Display(Name = "營業時間")]
            public Nullable<System.TimeSpan> opentime { get; set; }
            [Display(Name = "打烊時間")]
            public Nullable<System.TimeSpan> closetime { get; set; }
            [Display(Name = "公休日")]
            public string public_holiday { get; set; }
            [Display(Name = "logo圖檔")]
            public string logoimage_path { get; set; }
            [Display(Name = "首頁圖檔")]
            public string indeximage_path { get; set; }
            [Display(Name = "公司簡介")]
            public string description { get; set; }
        }
    }
}
