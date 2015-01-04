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
        /// <summary>
        /// 通过cookies的名称获取cookie
        /// </summary>
        /// <param name="name">cookie的名称</param>
        /// <returns></returns>
        public static HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }

        /// <summary>
        /// 通过cookie的名称获取cookie的值
        /// </summary>
        /// <param name="name">cookie的名称</param>
        /// <returns></returns>
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

        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="name">cookie的名称</param>
        public static void Remove(string name)
        {
            Cookie.Remove(Cookie.Get(name));
        }
        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="cookie">具体的cookie</param>
        public static void Remove(HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now;
                Cookie.Save(cookie, 0);
            }
        }
        /// <summary>
        /// 保存cookie
        /// </summary>
        /// <param name="name">cookie名称</param>
        /// <param name="value">cookie的值</param>
        /// <param name="expiresHours">cookie的有效期</param>
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
        /// <summary>
        /// 保存cookie
        /// </summary>
        /// <param name="cookie">具体的cookie对象</param>
        /// <param name="expiresHours">有效期</param>
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
        /// <summary>
        /// 生成新的cookie实体
        /// </summary>
        /// <param name="name">cookie的名称</param>
        /// <returns></returns>
        public static HttpCookie Set(string name)
        {
            return new HttpCookie(name);
        }
    }
}
