using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace foodfun.Models
{

    [MetadataType(typeof(PropertysMetaData))]

    public partial class Propertys
    {
        private class PropertysMetaData
        {
            [Key]
            [Display(Name = "流水編號")]
            public int rowid { get; set; }


            [Required(ErrorMessage = "產品屬性不可空白")]
            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入產品屬性編號")]
            [Display(Name = "產品屬性")]

            public string property_no { get; set; }

            [Required(ErrorMessage = "產品屬性名稱不可空白")]
            [Display(Name = "產品屬性名稱")]

            public string property_name { get; set; }

            [Display(Name = "單項加價金額")]
            public Nullable<int> add_price { get; set; }


            [Required(ErrorMessage = "屬性等級不可空白")]
            [Display(Name = "屬性等級")]
            public string property_value { get; set; }


            [Display(Name = "備註")]
            public string remark { get; set; }
          
        }
    }
}