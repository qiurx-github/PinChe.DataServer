using LS.Framework;
using LS.Framework.Models;
using LS.Framework.Repository.Implementations;
using LS.Framework.Repository.Interface;
using PinChe.DataServer.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PinChe.DataServer.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IUserRepository userService)
        {
            _userService = userService;
        }
        private readonly IUserRepository _userService;
        public ActionResult GetUserInfo()
        {
            if (!WorkContext.IsLogin)
            {
                return JsonAndJsonP(AjaxResult.Error("登入参数无效"));
            }
            WorkContext.User.SaveLogin(true);

            return JsonAndJsonP(AjaxResult.Success("登录成功",WorkContext.User.ToJson()));
        }
        public ActionResult Regist(string mobile, string smsCode, string password, string source = "", int inviteID = 0)
        {           

            //mobile = mobile.FilterSql();
            //password = password.FilterSql();

            //校验验证码
            //if (!SMSHelper.CheckValidateCode(mobile, smsCode))
            //{
            //    return JsonAndJsonP(objGenericResult, "验证码错误或失效");
            //}

            if ( string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(smsCode)   || string.IsNullOrWhiteSpace(password))
            {
                return JsonAndJsonP(AjaxResult.Error( "参数不完成"));
            }

            
            if (_userService.Any(u=>u.Mobile == mobile))
            {
                return JsonAndJsonP(AjaxResult.Error( "手机号已注册,请更换手机"));
            }

            //新增用户
            User user = new User();
            user.Avatar = "没有头像，哭唧唧~";
            user.Mobile = mobile;
            user.Password = password;
            user.InviteUserId = inviteID;
            user.IP = Net.Ip;
            //根据IP获取省市区
            //UserHelper.GetLocationForIP(user);
            user.AddTime = DateTime.Now;
            user.UpdateTime = DateTime.Now;
            user.Sex = 1;

            //switch (WorkContext.ComPT)
            //{
            //    case 1:
            //        user.Source = "wxapp";
            //        break;
            //    case 2:
            //        user.Source = "ios";
            //        break;
            //    case 3:
            //        user.Source = "android";
            //        break;
            //    default:
            //        user.Source = source;
            //        break;
            //}

            _userService.Insert(user);

            //try
            //{
            //    //邀请数量累计
            //    UserHelper.UpdateUserInviteCount(inviteID);
            //}
            //catch (Exception err) { }


            user.SaveLogin(true);
            //objGenericResult.Data = user.GetLoginResult();
            return JsonAndJsonP(AjaxResult.Success("注册成功",user));
        }

        public ActionResult Login(string unionID = "", string mobile = "", string password = "")
        {
            User user;
            if (!string.IsNullOrWhiteSpace(unionID))
            {
                user = _userService.FindEntity(u => u.WxUnionId == unionID);
            }
            else if (!string.IsNullOrWhiteSpace(mobile))
            {
                user = _userService.FindEntity(u => u.Mobile == mobile && u.Password == password);
            }
            else
            {
                return JsonAndJsonP(AjaxResult.Error("参数不完整"));
            }
            if (user == null)
            {
                return JsonAndJsonP(AjaxResult.Error("用户不存在或密码错误"));
            }

            user.SaveLogin(true);
            return JsonAndJsonP(AjaxResult.Success("登录成功", WorkContext.User.ToJson()));
        }

        public ActionResult ResetPwd(string password)
        {
            if (!WorkContext.IsLogin)
            {
                return JsonAndJsonP(AjaxResult.Error("登入参数无效"));
            }
            WorkContext.User.Password = password;
            _userService.Update(WorkContext.User);
            return JsonAndJsonP(AjaxResult.Success("修改成功"));
        }
    }
}