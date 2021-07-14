using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using foodfun.Models;

/// <summary>
/// 使用者資訊類別
/// </summary>
public static class UserAccount
{
    /// <summary>
    /// 登入使用者角色
    /// </summary>

    public static EnumList.LoginRole Role
    {
        get { return (HttpContext.Current.Session["Role"] == null) ? EnumList.LoginRole.Guest : (EnumList.LoginRole)HttpContext.Current.Session["Role"]; }
        set { HttpContext.Current.Session["Role"] = value; }
    }
    /// <summary>
    /// 登入使用者角色名稱
    /// </summary>
    public static string RoleName { get { return EnumList.GetRoleName(Role); } }
    /// <summary>
    /// 使用者帳號
    /// </summary>
    public static string UserNo
    {
        get { return (HttpContext.Current.Session["UserNo"] == null) ? "" : HttpContext.Current.Session["UserNo"].ToString(); }
        set { HttpContext.Current.Session["UserNo"] = value; }
    }

    /// <summary>
    /// 使用者名稱
    /// </summary>
    public static string UserName
    {
        get { return (HttpContext.Current.Session["UserName"] == null) ? "" : HttpContext.Current.Session["UserName"].ToString(); }
        set { HttpContext.Current.Session["UserName"] = value; }
    }
    /// <summary>
    /// 使用者電子信箱
    /// </summary>
    public static string UserEmail
    {
        get { return (HttpContext.Current.Session["UserEmail"] == null) ? "" : HttpContext.Current.Session["UserEmail"].ToString(); }
        set { HttpContext.Current.Session["UserEmail"] = value; }
    }
    /// <summary>
    /// 使用者是否已登入
    /// </summary>
    public static bool IsLogin
    {
        get { return (HttpContext.Current.Session["IsLogin"] == null) ? false : (bool)HttpContext.Current.Session["IsLogin"]; }
        set { HttpContext.Current.Session["IsLogin"] = value; }
    }

    public static void Login()
    {
        using ( GoPASTAEntities db = new GoPASTAEntities())
        {
            var data = db.Users.Where(m => m.account_name == UserNo).FirstOrDefault();
            if (data == null)
                Logout();
            else
            {
                IsLogin = true;
                UserName = data.mname;
                UserEmail = data.email;
                Role = EnumList.GetRoleType(data.role_no);
            }
        }
    }

    public static void Logout()
    {
        IsLogin = false;
        Role = EnumList.LoginRole.Guest;
        UserNo = "";
        UserName = "";
        UserEmail = "";
    }
}
