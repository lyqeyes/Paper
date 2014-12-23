using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Helpers
{
    public class WebClientHelper
    {
        public static string GetResponse(string ip)
        {
            string siteaddress = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=" + ip;
            WebClient wc = new WebClient();
            string allresult = wc.DownloadString(siteaddress);
            string[] items = allresult.Split(' ');
            return items[4] + items[5];
        }
    }
}
