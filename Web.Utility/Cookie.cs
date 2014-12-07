using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web.Utility
{
    public class Cookie
    {
        public static HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }
        public static string GetValue(string name)
        {
            HttpCookie httpCookie = Cookie.Get(name);
            string result;
            if (httpCookie != null)
            {
                result = httpCookie.Value;
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }
        public static void Remove(string name)
        {
            Cookie.Remove(Cookie.Get(name));
        }
        public static void Remove(HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now;
                Cookie.Save(cookie, 0);
            }
        }
        public static void Save(string name, string value, int expiresHours = 0)
        {
            HttpCookie httpCookie = Cookie.Get(name);
            if (httpCookie == null)
            {
                httpCookie = Cookie.Set(name);
            }
            httpCookie.Value = value;
            Cookie.Save(httpCookie, expiresHours);
        }
        public static void Save(HttpCookie cookie, int expiresHours = 0)
        {
            string serverDomain = Fetch.ServerDomain;
            string b = HttpContext.Current.Request.Url.Host.ToLower();
            if (serverDomain != b)
            {
                cookie.Domain = serverDomain;
            }
            if (expiresHours > 0)
            {
                cookie.Expires = DateTime.Now.AddHours((double)expiresHours);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static HttpCookie Set(string name)
        {
            return new HttpCookie(name);
        }
    }
}
