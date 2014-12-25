using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Web.Utility;

namespace Modules.Helpers
{
    public class ForgetPasswordHelper
    {
        public static void SendEmail(string email)
        {
            //发送找回密码邮件操作

            Guid g = Guid.NewGuid();
            HttpRuntime.Cache.Insert(g.ToString(), email, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));
            string msg = "这里是重置密码连接 " + String.Format("http://questionnaire.chinacloudsites.cn/UserOp/Auth/FindPassword/" + "{0}", g.ToString()) + "，30分钟内有效，请尽快更改密码";
            EmailHelper.SendEmail("问卷系统找回密码", msg, email);
        }
    }
}
