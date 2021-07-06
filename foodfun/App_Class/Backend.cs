using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;


//自動編輯商品編號
public static class Backend
{
    public static string CreateProductNo(string id)
    {


        GoPASTAEntities db = new GoPASTAEntities();

        string productno;
        var P = db.Products.Where(m => m.product_no.StartsWith(id)).Select(m => m.product_no).Max();
        if (P != null)
        {
            int X = Convert.ToInt32(P.Substring(3));
            X += 1;

            productno = id + X.ToString("D4");
        }
        else
        {
            productno = id + "0001";

        }

        return productno;


    }

    //轉換中文
    public static string GetCodeName(string typeNo)
    {

        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            string str_name = "";
           

            //var data = repoAppCode.ReadSingle(m => m.type_no == typeNo && m.mno == codeNo);
            //var data = db.Products.Where(m => m.category_no == typeNo && m.product_no == codeNo);
            //var cc = db.Categorys.Where(m => m.category_name == cname).ToString();
            //if (data != null) str_name = data + cc;
            //return str_name;

            var model = db.Categorys.Where(m => m.category_no == typeNo).FirstOrDefault();
           
            if (model != null)
            {
                str_name = model.category_name;
               
            }
            return str_name;




        }


    }
    public static string GetCodeName1(string typeNo)
    {

        using (GoPASTAEntities db = new GoPASTAEntities())
        {

            string str1_name = "";
            var model_roleno = db.Roles.Where(m => m.role_no == typeNo).FirstOrDefault();

            if (model_roleno != null)
            {
               
                str1_name = model_roleno.remark;
            }
        
            return str1_name;

        }
    }


    //下拉選單
    public static List<SelectListItem> CtgryDropdownList()
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            List<SelectListItem> cty_no = new List<SelectListItem>();
            var datas = db.Categorys.OrderBy(m => m.category_no).ToList();
            if (datas != null)
            {
                foreach (var data in datas)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = data.category_no;
                    item.Text = data.category_name;

                    cty_no.Add(item);
                }
                cty_no.First().Selected = true;
            }
            return cty_no;
        }

    }



}
