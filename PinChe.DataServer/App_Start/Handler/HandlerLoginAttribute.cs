﻿using LS.Framework;
using LS.Framework.Data;
using System;
using System.Web.Mvc;

namespace PinChe.DataServer
{
    public class HandlerLoginAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 登录过滤器，除了通有skip属性可以跳过登录验证的，其他操作都需要在已登录状态完成操作。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            //当有skip验证时，去除验证登录
            bool skipignore = filterContext.ActionDescriptor.GetCustomAttributes(typeof(SkipAttribute), false).Length == 1
                                       || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipAttribute), false);
            if (skipignore == true) return;

            OperatorProvider op = OperatorProvider.Provider;

            #region 判断用户是否登录|当前只可单个浏览器登录，即使是浏览器重开，也会导致sessionId变更，也就需要重新登录
            if (op.CurrentUser == null)
            {
                //StringBuilder sbScript = new StringBuilder();
                //sbScript.Append("<script type='text/javascript'>alert('请先登录!');top.location.href='/Login/Index?msg=noLogin'</script>");
                // filterContext.Result = new ContentResult() { Content = sbScript.ToString() };

                WebHelper.WriteCookie("ls_login_error", "Overdue");//登录已超时,请重新登录
                filterContext.Result = new RedirectResult("~/Login/Default");

            }
            else
            {

                //

                bool loginOnce = Convert.ToBoolean(Configs.GetValue("loginOnce"));

                if (loginOnce)
                {
                    //当前登录信息被覆盖，说明存在多个浏览器登录的情况
                    string userid = op.CurrentUser.UserId.ToString();
                    ICache cache = CacheFactory.Cache();
                    if (cache.GetCache<string>(userid) != op.Session.SessionID)
                    {
                        //清空Session
                        filterContext.HttpContext.Session.Remove(userid);
                        op.RemoveCurrent();
                        //跳转强制下线界面
                        this.HandleUnauthorizedRequest(filterContext);
                    }
                }
            }
            #endregion
        }

        /// <summary>
        ///   当重写验证返回为false时进入该重写方法
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext.HttpContext.Request.Url != null)
            {
                filterContext.Result = new RedirectResult("/Error/ReturnToLogin");
            }
        }
    }
}