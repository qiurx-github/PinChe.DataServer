using LS.Framework;
using LS.Framework.Data;
using LS.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PinChe.DataServer.Controllers
{
    public class BaseController : Controller
    {
        public WorkContext WorkContext => new WorkContext();
        public OperatorProvider op => OperatorProvider.Provider;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            WorkContext.IsHttpAjax = op.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            WorkContext.IP = Net.Ip;
            WorkContext.Url = op.Request.Url.ToString();
            WorkContext.UrlReferrer = op.Request.UrlReferrer == null ? string.Empty : op.Request.UrlReferrer.ToString();

            WorkContext.Controller = RouteData.Values["controller"].ToString().ToLower();
            WorkContext.Action = RouteData.Values["action"].ToString().ToLower();
        }

    }

    /// <summary>
    /// Request 上下文
    /// </summary>
    public class WorkContext
    {
        public bool IsLogin { get { return UserID > 0; } }
        public bool IsNoLogin { get { return UserID == 0; } }
        public bool IsHttpAjax;//当前请求是否为ajax请求

        public string Controller;//控制器

        public string Action;//动作方法

        public string IP;//用户IP

        public string Url;//当前url

        public string UrlReferrer;//上一次访问的url

        public int ComPT = 0;
        public int ComVer = 0;

        public int UserID { get; set; }

        public bool IsRoot
        {
            get
            {
                if (IsNoLogin) return false;

                if (KdUser.Mobile.Contains("1895056")) return true;

                string[] phones = { "18950569828", "15259166314", "18859350069" };

                return phones.Contains(KdUser.Mobile);
            }
        }

        private User _user = null;
        public User KdUser
        {
            get
            {
                if (_user != null) return _user;

                if (UserID == 0) return null;

                //_user = new BizKdUser().GetRecordCache(UserID);

                return _user;
            }
        }
    }
}