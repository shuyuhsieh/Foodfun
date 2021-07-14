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

    //使用者商品類別中文轉換
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
    //使用者角色代號中文轉換
    public static string GetCodeNameRole(string typeNo)
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



    //商品類別下拉選單
    public static List<SelectListItem> CtgryDropdownList()
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            
            List<SelectListItem> cty_no = new List<SelectListItem>();
           
            var datas = db.Categorys.Where(m => m.parentid != 0).OrderBy(m => m.category_no).ToList(); 
            if (datas != null)

            {
                foreach (var data in datas)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = data.category_no ;
                    item.Text = data.category_name;
                   
                    cty_no.Add(item);
                }
                cty_no.First().Selected = true;
              
            }

            return cty_no ;
        }

    }


    //商品類別parentid下拉選單
    public static List<SelectListItem> ParentIdDropdownList()
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {

            List<SelectListItem> parntid = new List<SelectListItem>();

            var datas = db.Categorys.OrderBy(m => m.category_name).ToList();
            if (datas != null)

            {
                foreach (var data in datas)
                {
                    SelectListItem item = new SelectListItem();                   

                   
                    item.Text = data.category_name;

                    parntid.Add(item);
                }
                parntid.First().Selected = true;

            }

            return parntid;
        }

    }


    //會員類別role下拉選單
    public static List<SelectListItem> RoleDropdownList()
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {

            List<SelectListItem> rolename = new List<SelectListItem>();

            var datas = db.Roles.OrderBy(m => m.remark).ToList();
            if (datas != null)

            {
                foreach (var data in datas)
                {
                    SelectListItem item = new SelectListItem();

                    item.Value = data.role_no;
                    item.Text = data.remark;

                    rolename.Add(item);
                }

                rolename.First().Selected = true;

            }

            return rolename;
        }

    }





}
