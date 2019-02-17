using LS.Framework;
using LS.Framework.Data;
using LS.Framework.Models;
using LS.Framework.Repository.Implementations;
using LS.Framework.Repository.Interface;
using PinChe.DataServer.App_Start.Handler;
using PinChe.DataServer.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PinChe.DataServer.Controllers
{
    [AllowCrossSiteJson]
    public class BaseController : Controller
    {
        private WorkContext _workContext;
        public WorkContext WorkContext
        {
            get
            {
                if (_workContext == null)
                {
                    _workContext = new WorkContext();
                }
                return _workContext;
            }
        }
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

            string m_id = RequestHelper.GetString("m_id", "");
            string m_checkmd5 = RequestHelper.GetString("m_token", "");
            if (!string.IsNullOrWhiteSpace(m_id)&&!string.IsNullOrWhiteSpace(m_checkmd5))
            {
                //WorkContext.UserID = UserHelper.GetLoginId();
            }
            WorkContext.UserID = UserHelper.GetLoginId();
        }

        public ActionResult JsonAndJsonP(object data)
        {
            string callback = RequestHelper.GetString("callback","");
            if (string.IsNullOrWhiteSpace(callback))
            {
                return Jsonp(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult JsonAndJsonP(AjaxResult ajaxResult, string message)
        {
            ajaxResult.Message = message;
            return JsonAndJsonP(ajaxResult);
        }
        #region Jsonp
        protected JsonpResult Jsonp(object data, string JsonpHostPattern = "")
        {
            return this.Jsonp(data, null, null, JsonRequestBehavior.DenyGet, JsonpHostPattern);
        }

        protected JsonpResult Jsonp(object data, string contentType, string JsonpHostPattern = "")
        {
            return this.Jsonp(data, contentType, null, JsonRequestBehavior.DenyGet, JsonpHostPattern);
        }

        protected virtual JsonpResult Jsonp(object data, string contentType, Encoding contentEncoding, string JsonpHostPattern = "")
        {
            return this.Jsonp(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet, JsonpHostPattern);
        }

        protected JsonpResult Jsonp(object data, JsonRequestBehavior behavior, string JsonpHostPattern = "")
        {
            return this.Jsonp(data, null, null, behavior, JsonpHostPattern);
        }

        protected JsonpResult Jsonp(object data, string contentType, JsonRequestBehavior behavior, string JsonpHostPattern = "")
        {
            return this.Jsonp(data, contentType, null, behavior, JsonpHostPattern);
        }

        protected virtual JsonpResult Jsonp(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior, string JsonpHostPattern = "")
        {
            return new JsonpResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                JsonpHostPattern = JsonpHostPattern
            };
        } 
        #endregion
    }

    /// <summary>
    /// Request 上下文
    /// </summary>
    public class WorkContext
    {
        private readonly IUserRepository _userService = new UserRepository();
        public bool IsLogin => UserID > 0;
        public bool IsNoLogin => UserID == 0;
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

                if (User.Mobile.Contains("1895056")) return true;

                string[] phones = { "18950569828", "15259166314", "18859350069" };

                return phones.Contains(User.Mobile);
            }
        }

        private User _user = null;
        public User User
        {
            get
            {
                if (_user != null) return _user;

                if (UserID == 0) return null;
                Console.WriteLine("PinChe.DataServer.Controllers=>BaseController=>WorkContext=>User");
                _user = _userService.FindEntity(u=>u.Id == UserID);

                return _user;
            }
        }
    }
}