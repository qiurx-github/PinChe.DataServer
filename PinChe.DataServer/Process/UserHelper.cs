using LS.Framework;
using LS.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinChe.DataServer.Process
{
    public static class UserHelper
    {
        public static string _CookieDomain = "";
        public static string _CheckMd5Key = "qiurx^_^";

        public static string _CookieUId = "u_id";
        public static string _CookieCheckMd5 = "u_checkmd5";

        //是否发布（暂时不用）

        public static void SaveLogin(this User user, bool isRemember = false)
        {
            SaveLogin(user.Id.ToString(), isRemember);
        }

        public static void SaveLogin(string uid,bool isRemember)
        {
            if (isRemember)
            {
                CookieHelper.SetCookieValue(uid, _CookieUId, DateTime.Now.AddDays(30),_CookieDomain);
            }
            else
            {
                CookieHelper.SetCookieValue(uid, _CookieUId, _CookieDomain);
            }
        }

        public static int GetLoginId()
        {
            string uid = CookieHelper.GetCookieValue(_CookieUId);
            return string.IsNullOrWhiteSpace(uid) ? 0 : Convert.ToInt32(uid);
        }

        public static void ExitLogin()
        {
            CookieHelper.RemoveCookie(_CookieUId, "");
        }
    }
}