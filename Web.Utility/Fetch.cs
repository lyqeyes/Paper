using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web.Utility
{
    public class Fetch
    {
        public static string CurrentUrl
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString();
            }
        }
        public static string ServerDomain
        {
            get
            {
                string text = HttpContext.Current.Request.Url.Host.ToLower();
                string[] array = text.Split(new char[]
				{
					'.'
				});
                string result;
                if (array.Length < 3 || RegExp.IsIp(text))
                {
                    result = text;
                }
                else
                {
                    string text2 = text.Remove(0, text.IndexOf(".") + 1);
                    if (text2.StartsWith("com.") || text2.StartsWith("net.") || text2.StartsWith("org.") || text2.StartsWith("gov."))
                    {
                        result = text;
                    }
                    else
                    {
                        result = text2;
                    }
                }
                return result;
            }
        }
        public static string UserIp
        {
            get
            {
                string text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                string text2 = text;
                if (text2 == null || text2 == "")
                {
                    text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                string result;
                if (!RegExp.IsIp(text))
                {
                    result = "Unknown";
                }
                else
                {
                    result = text;
                }
                return result;
            }
        }
        public static string Get(string name)
        {
            string text = HttpContext.Current.Request.QueryString[name];
            return (text == null) ? "" : text.Trim();
        }
        public static string Post(string name)
        {
            string text = HttpContext.Current.Request.Form[name];
            return (text == null) ? "" : text.Trim();
        }
        public static int GetQueryId(string name)
        {
            int result = 0;
            int.TryParse(Fetch.Get(name), out result);
            return result;
        }
        public static int[] GetIds(string name)
        {
            string text = Fetch.Post(name);
            List<int> list = new List<int>();
            int item = 0;
            string[] array = text.Split(new char[]
			{
				','
			});
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text2 = array2[i];
                if (int.TryParse(text2.Trim(), out item))
                {
                    list.Add(item);
                }
            }
            return list.ToArray();
        }
        public static int[] GetQueryIds(string name)
        {
            string text = Fetch.Get(name);
            List<int> list = new List<int>();
            int item = 0;
            string[] array = text.Split(new char[]
			{
				','
			});
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text2 = array2[i];
                if (int.TryParse(text2.Trim(), out item))
                {
                    list.Add(item);
                }
            }
            return list.ToArray();
        }
    }
}
