using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PinChe.DataServer.App_Start.Handler
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:8080");
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            base.OnActionExecuting(filterContext);
        }

        //指定域名
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var domains = new List<string> { "domain2.com", "domain1.com" };

        //    if (domains.Contains(filterContext.RequestContext.HttpContext.Request.UrlReferrer.Host))
        //    {
        //        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
        //    }

        //    base.OnActionExecuting(filterContext);
        //}
    }
}