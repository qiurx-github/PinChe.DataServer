using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LS.Framework
{
    public static class CookieHelper
    {
        public static string GetCookieValue(this string cookieName)
        {
            string result;
            if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.Cookies == null)
            {
                result = null;
            }
            else
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
                if (httpCookie != null)
                {
                    result = HttpUtility.UrlDecode(httpCookie.Value, Encoding.UTF8);
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }

        public static HttpCookie GetCookie(this string cookieName)
        {
            HttpCookie result;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies != null)
            {
                result = HttpContext.Current.Request.Cookies[cookieName];
            }
            else
            {
                result = null;
            }
            return result;
        }

        public static NameValueCollection GetCookieValues(this string cookieName)
        {
            NameValueCollection result;
            if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.Cookies == null)
            {
                result = null;
            }
            else
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
                if (httpCookie != null)
                {
                    NameValueCollection nameValueCollection = new NameValueCollection();
                    string[] allKeys = httpCookie.Values.AllKeys;
                    for (int i = 0; i < allKeys.Length; i++)
                    {
                        string name = allKeys[i];
                        nameValueCollection.Add(name, HttpUtility.UrlDecode(httpCookie.Values[name], Encoding.UTF8));
                    }
                    result = nameValueCollection;
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }

        public static string GetCookieValue(this string cookieName, string keyName)
        {
            string result;
            if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.Cookies == null)
            {
                result = null;
            }
            else
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
                if (httpCookie != null && httpCookie.Values.AllKeys.Contains(keyName))
                {
                    result = HttpUtility.UrlDecode(httpCookie[keyName], Encoding.UTF8);
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }

        public static void RemoveCookie(this string cookieName, string domains = "")
        {
            cookieName.RemoveCookie(domains, "");
        }

        public static void RemoveCookie(this string cookieName, string domains, string path = "")
        {
            string[] array = domains.Split(new char[]
            {
                ','
            });
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                HttpCookie httpCookie = new HttpCookie(cookieName);
                httpCookie.HttpOnly = true;
                httpCookie.Expires = DateTime.Now.AddYears(-1);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    httpCookie.Path = path;
                }
                if (!string.IsNullOrWhiteSpace(text))
                {
                    httpCookie.Domain = text;
                }
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
        }

        public static void RemoveCookieKey(this string cookieName, string keyName, DateTime expires, string domains = "")
        {
            cookieName.RemoveCookieKey(keyName, expires, domains, "");
        }

        public static void RemoveCookieKey(this string cookieName, string keyName, DateTime expires, string domains, string path = "")
        {
            string[] array = domains.Split(new char[]
            {
                ','
            });
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                if (cookieName.CookieRequestExists())
                {
                    HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
                    httpCookie.HttpOnly = true;
                    httpCookie.Values.Remove(keyName);
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        httpCookie.Path = path;
                    }
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        httpCookie.Domain = text;
                    }
                    if (!expires.Equals(DateTime.MinValue))
                    {
                        httpCookie.Expires = expires;
                    }
                    HttpContext.Current.Response.Cookies.Add(httpCookie);
                }
            }
        }

        public static bool CookieRequestExists(this string cookieName)
        {
            return HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookieName] != null;
        }

        public static bool CookieResponseExists(this string cookieName)
        {
            return HttpContext.Current != null && HttpContext.Current.Response != null && HttpContext.Current.Response.Cookies != null && HttpContext.Current.Response.Cookies[cookieName] != null;
        }

        public static void SetCookieValue(this string value, string cookieName, string domains = "")
        {
            value.SetCookieValue(cookieName, DateTime.MinValue, domains);
        }

        public static void SetCookieValue(this string value, string cookieName, DateTime expires, string domains = "")
        {
            value.SetCookieValue(cookieName, expires, domains, "");
        }

        public static void SetCookieValue(this string value, string cookieName, DateTime expires, string domains, string path = "")
        {
            string[] array = domains.Split(new char[]
            {
                ','
            });
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                HttpCookie httpCookie = new HttpCookie(cookieName);
                httpCookie.Value = HttpUtility.UrlEncode(value, Encoding.UTF8);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    httpCookie.Path = path;
                }
                if (!string.IsNullOrWhiteSpace(text))
                {
                    httpCookie.Domain = text;
                }
                if (!expires.Equals(DateTime.MinValue))
                {
                    httpCookie.Expires = expires;
                }
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
        }

        public static void SetCookieValue(this NameValueCollection KeyValue, string cookieName, string domains = "")
        {
            KeyValue.SetCookieValue(cookieName, DateTime.MinValue, domains);
        }

        public static void SetCookieValue(this NameValueCollection KeyValue, string cookieName, DateTime expires, string domains = "")
        {
            KeyValue.SetCookieValue(cookieName, expires, domains, "");
        }

        public static void SetCookieValue(this NameValueCollection KeyValue, string cookieName, DateTime expires, string domains, string path = "")
        {
            string[] array = domains.Split(new char[]
            {
                ','
            });
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                HttpCookie httpCookie = new HttpCookie(cookieName);
                string[] allKeys = KeyValue.AllKeys;
                for (int j = 0; j < allKeys.Length; j++)
                {
                    string text2 = allKeys[j];
                    httpCookie[text2] = HttpUtility.UrlEncode(KeyValue[text2], Encoding.UTF8);
                }
                if (!string.IsNullOrWhiteSpace(path))
                {
                    httpCookie.Path = path;
                }
                if (!string.IsNullOrWhiteSpace(text))
                {
                    httpCookie.Domain = text;
                }
                if (!expires.Equals(DateTime.MinValue))
                {
                    httpCookie.Expires = expires;
                }
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
        }

        public static void UpdateCookieKeyValue(this string cookieName, string keyName, string keyValue, DateTime expires, string domains = "")
        {
            cookieName.UpdateCookieKeyValue(keyName, keyValue, expires, domains, "");
        }

        public static void UpdateCookieKeyValue(this string cookieName, string keyName, string keyValue, DateTime expires, string domains, string path = "")
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies != null)
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
                if (httpCookie != null)
                {
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        httpCookie.Path = path;
                    }
                    if (!string.IsNullOrWhiteSpace(domains))
                    {
                        httpCookie.Domain = domains;
                    }
                    if (!expires.Equals(DateTime.MinValue))
                    {
                        httpCookie.Expires = expires;
                    }
                    httpCookie[keyName] = HttpUtility.UrlEncode(keyValue, Encoding.UTF8);
                    HttpContext.Current.Response.Cookies.Add(httpCookie);
                }
            }
        }

        public static void UpdateCookieValue(this string cookieName, string value, DateTime expires, string domains = "")
        {
            cookieName.UpdateCookieValue(value, expires, domains, "");
        }

        public static void UpdateCookieValue(this string cookieName, string value, DateTime expires, string domains, string path = "")
        {
            string[] array = domains.Split(new char[]
            {
                ','
            });
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies != null)
                {
                    HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
                    if (httpCookie == null)
                    {
                        break;
                    }
                    httpCookie.Value = HttpUtility.UrlEncode(value, Encoding.UTF8);
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        httpCookie.Path = path;
                    }
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        httpCookie.Domain = text;
                    }
                    if (!expires.Equals(DateTime.MinValue))
                    {
                        httpCookie.Expires = expires;
                    }
                    HttpContext.Current.Response.Cookies.Add(httpCookie);
                }
            }
        }

        private static string SelectDomain(string domains)
        {
            bool flag = false;
            string result;
            if (domains.Trim().Length == 0)
            {
                result = "";
            }
            else
            {
                string text = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
                if (!text.Contains("."))
                {
                    flag = true;
                }
                string text2 = "";
                string[] array = domains.Split(new char[]
                {
                    ';'
                });
                int i = 0;
                while (i < array.Length)
                {
                    if (text.Contains(array[i].Trim()))
                    {
                        if (flag)
                        {
                            text2 = "";
                            break;
                        }
                        text2 = array[i].Trim();
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                result = text2;
            }
            return result;
        }

    }
}
