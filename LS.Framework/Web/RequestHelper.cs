using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LS.Framework 
{
    public static class RequestHelper
    {
        public static HttpContext CurrentContext=> HttpContext.Current;
        public static string GetString(string key, string defaultValue, bool isFilterSql = true)
        {
            string text = CurrentContext.Request[key];
            string result;
            if (string.IsNullOrEmpty(text))
            {
                result = defaultValue;
            }
            else if (isFilterSql)
            {
                Console.WriteLine($"防SQL注入没做:LS.Framework => web =>RequestHelper.cs=>GetString(...)");
                result = text;
            }
            else
            {
                result = text;
            }
            return result;
        }
    }
}
