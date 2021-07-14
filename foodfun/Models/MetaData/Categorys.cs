using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace foodfun.Models
{
    [MetadataType(typeof(CategorysMetaData))]

    public partial class Categorys
    {
        private class CategorysMetaData
        {
            [Key]
            [Display(Name = "流水編號")]
            public int rowid { get; set; }


            [Required(ErrorMessage = "主類別不可空白")]
            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入主類別編號")]
            [Display(Name = "主類別")]
            public Nullable<int> parentid { get; set; }

            [Required(ErrorMessage = "類別編號不可空白")]
            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入類別編號")]
            [Display(Name = "類別編號")]
            public string category_no { get; set; }

            [Required(ErrorMessage = "類別名稱不可空白")]
            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入類別名稱編號")]
            [Display(Name = "類別名稱")]
            public string category_name { get; set; }
          
        }

    }

}