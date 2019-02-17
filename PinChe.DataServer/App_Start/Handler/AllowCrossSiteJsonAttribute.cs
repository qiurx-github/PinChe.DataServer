﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PinChe.DataServer.App_Start.Handler
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        private static readonly string[] domains = ConfigurationManager.AppSettings["site"].Split(';');
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
        //    base.OnActionExecuting(filterContext);
        //}

        //指定域名
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (var item in domains)
            {
                if (item.Contains(filterContext.RequestContext.HttpContext.Request.UrlReferrer.Host))
                {
                    filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", item);
                    filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");
                    break;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}