using LS.Framework.Data;
using LS.Framework.Models;
using LS.Framework.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LS.Framework;

namespace PinChe.DataServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TestRepository test = new TestRepository();
            test.InsertTest();
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public string Register(string username, string password, string code)
        {
            return new AjaxResult { State = password.ToString(), Message = username }.ToJson();
            //LogEntity logEntity = new LogEntity();
            //logEntity.F_ModuleName = "注册";
            //logEntity.F_Type = DbLogType.Login.ToString();
            //try
            //{
            //    UserEntity userEntity = new UserApp().Regster(username, password);
            //    if (userEntity != null)
            //    {
            //        logEntity.F_Account = userEntity.F_Account;
            //        logEntity.F_NickName = userEntity.F_RealName;
            //        logEntity.F_Result = true;
            //        logEntity.F_Description = "注册成功";
            //        new LogApp().WriteDbLog(logEntity);
            //    }
            //    return new AjaxResult { state = ResultType.success.ToString(), message = "注册成功。" }.ToJson();
            //}
            //catch (Exception ex)
            //{
            //    logEntity.F_Account = username;
            //    logEntity.F_NickName = username;
            //    logEntity.F_Result = false;
            //    logEntity.F_Description = "注册失败，" + ex.Message;
            //    new LogApp().WriteDbLog(logEntity);
            //    return new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson();
            //}
        }
    }
}
