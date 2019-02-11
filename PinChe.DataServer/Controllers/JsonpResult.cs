using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PinChe.DataServer.Controllers
{
    public class JsonpResult : ActionResult
    {
        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public string JsonpHostPattern { get; set; }

        public object Data { get; set; }

        public JsonRequestBehavior JsonRequestBehavior { get; set; }

        public int? MaxJsonLength { get; set; }

        public int? RecursionLimit { get; set; }

        public JsonpResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet");
            }
            Controller marsController = (Controller)context.Controller;

            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            string text2 = context.HttpContext.Request["callback"];
            if (this.Data != null)
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                if (this.MaxJsonLength.HasValue)
                {
                    javaScriptSerializer.MaxJsonLength = this.MaxJsonLength.Value;
                }
                if (this.RecursionLimit.HasValue)
                {
                    javaScriptSerializer.RecursionLimit = this.RecursionLimit.Value;
                }
                response.Write(HttpUtility.UrlEncode(text2) + "(" + javaScriptSerializer.Serialize(this.Data) + ")");
            }
        }
    }

}