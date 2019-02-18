using System;

namespace LS.Framework
{
    [Serializable]
    public class AjaxResult
    {
        /// <summary>
        /// 操作结果类型
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 获取 返回数据
        /// </summary>
        public object data { get; set; }

        public AjaxResult()
        {

        }

        public static AjaxResult Info(object data, string message, ResultType state)
        {
            return new AjaxResult { state = (int)state, message = message, data = data };
        }

        public static AjaxResult Info(string message, object data = null)
        {
            return Info(data, message, ResultType.Info);
        }
        public static AjaxResult Success(string message, object data = null)
        {
            return Info(data, message, ResultType.Success);
        }
        public static AjaxResult Error(string message, object data = null)
        {
            return Info(data, message, ResultType.Error);
        }

    }
    /// <summary>
    /// 表示 ajax 操作结果类型的枚举
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// 异常结果类型
        /// </summary>
        Error = -1,
        /// <summary>
        /// 成功结果类型
        /// </summary>
        Success = 0,
        /// <summary>
        /// 警告结果类型
        /// </summary>
        Warning,
        /// <summary>
        /// 消息结果类型
        /// </summary>
        Info,
        /// <summary>
        /// 未登录
        /// </summary>
        Nologin,
        /// <summary>
        /// 没有权限
        /// </summary>
        Nopermission

    }
}


