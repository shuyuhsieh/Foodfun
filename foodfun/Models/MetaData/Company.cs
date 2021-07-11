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
            public int rowid { get; set; }
            public Nullable<int> company_id { get; set; }
            public string brandname { get; set; }
            public string tel { get; set; }
            public string fax { get; set; }
            public string address { get; set; }
            public Nullable<System.TimeSpan> opentime { get; set; }
            public Nullable<System.TimeSpan> closetime { get; set; }
            public string public_holiday { get; set; }
            public string logoimage_path { get; set; }
            public string indeximage_path { get; set; }
        }
    }
}
