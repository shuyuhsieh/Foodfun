using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    [MetadataType(typeof(PaymentsMetaData))]
    public partial class Payments
    {
        private class PaymentsMetaData
        {
            [Key]
            [Display(Name = "流水編號")]
            public int rowid { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入付款方式編碼")]
            [Required(ErrorMessage = "付款方式編碼不可空白")]
            [Display(Name = "付款方式編碼")]
            public string paid_no { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "付款方式")]
            [Required(ErrorMessage = "付款方式不可空白")]
            [Display(Name = "付款方式")]
            public string paid_name { get; set; }

          
            [Display(Name = "備註")]
            public string remark { get; set; }
        }
    }
}