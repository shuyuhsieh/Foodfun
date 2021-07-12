using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    [MetadataType(typeof(UsersMetaData))]
    public partial class Users
    {
        private class UsersMetaData
        {

            [Key]
            [Display(Name = "會員編號")]
            public string mno { get; set; }

            /// <summary>
            /// 姓名
            /// </summary>
            [Display(Name = "姓名")]
            [Required(ErrorMessage = "姓名不可空白")]

            public string mname { get; set; }

            [Display(Name = "帳號")]
            [Required(ErrorMessage = "帳號不可空白")]
            public string account_name { get; set; }

            [Display(Name = "密碼")]
            [Required(ErrorMessage = "密碼不可空白")]
            [DataType(DataType.Password)]
            public string password { get; set; }

            [Display(Name = "身分證字號")]
            public string id { get; set; }

            [Display(Name = "出生日期")]
            //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "yyyy/MM/dd")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
            public Nullable<System.DateTime> birthday { get; set; }

            [Required]
            [Display(Name = "手機號碼")]
            [RegularExpression(@"^09[0-9]{8}",ErrorMessage ="手機號碼格式錯誤")]
            public string phone { get; set; }

            [Display(Name = "地址")]
            public string address { get; set; }

            [Required(ErrorMessage = "電子信箱不可空白")]
            [EmailAddress(ErrorMessage = "電子信箱格式錯誤")]
            [Display(Name = "電子信箱")]
            public string email { get; set; }

            [Display(Name = "使用者角色")]
            public string role_no { get; set; }

            [Display(Name = "認證編號")]
            public string varify_code { get; set; }

            [Display(Name = "是否認證")]
            public Nullable<int> isvarify { get; set; }
        }
    }
}