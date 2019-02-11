using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Framework.Web
{
    [Serializable]
    public class GenericResult
    {
        public GenericResult()
        {
            ResultCode = "1";
            ResultMessage = "调用成功";
            Timestamp = (int)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public long Timestamp { get; set; }
        public object Data { get; set; }
    }
}
