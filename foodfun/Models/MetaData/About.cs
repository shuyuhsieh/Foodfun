using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    [MetadataType(typeof(AboutMetaData))]
    public partial class About    {
        
        private class AboutMetaData
        {
            [Key]
            public int rowid { get; set; }

            [Display(Name = "項次")]
            [Required(ErrorMessage = "項次不可空白")]
            public Nullable<int> sortid { get; set; }
            [Display(Name = "理念")]
            [Required(ErrorMessage = "理念標題不可空白")]
            public string corevalue_title { get; set; }
            [Display(Name = "理念說明")]
            [Required(ErrorMessage = "理念說明不可空白")]
            public string corevalue_descpt { get; set; }
            [Display(Name = "頁面圖片")]           
            public string about_path { get; set; }
            [Display(Name = "理念圖片")]
            public string corevalue_path { get; set; }
        }
    }
}