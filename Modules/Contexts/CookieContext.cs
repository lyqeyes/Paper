using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Utility;
using System.Web;

namespace Modules.Contexts
{
    public class CookieContext:IAuthCookie
    {
        public CookieContext() 
        { 
        }
        public virtual string KeyPrefix
        {
            get
            {
                return "Context_";
            }
        }

        public void Set(string key, string value, int expiresHours = 0)
        {
            if (expiresHours > 0)
            {
                Cookie.Save(KeyPrefix + key, value, expiresHours);
            }
            else
            {
                Cookie.Save(KeyPrefix + key, value);
            }
        }


        #region 对IAuthCookie接口的实现
        private int userExpiresHours = 10;
        public int UserExpiresHours
        {
            get
            {
                return userExpiresHours;
            }
            set
            {
                userExpiresHours = value;
            }
        }

        public int UserId
        {
            get
            {
                int id = 0;
                int.TryParse(Cookie.GetValue(KeyPrefix + "UserId"), out id);
                return id;
            }
            set
            {
                Cookie.Save(KeyPrefix + "UerId", value.ToString(), UserExpiresHours);
            }
        }

        public string UserName
        {
            get
            {
                return HttpUtility.UrlDecode(Cookie.GetValue(KeyPrefix + "UserName"));
            }
            set
            {
                Cookie.Save(KeyPrefix + "UserName", value, UserExpiresHours);
            }
        }

        public string Email
        {
            get
            {
                return Cookie.GetValue(KeyPrefix + "Email");
            }
            set
            {
                Cookie.Save(KeyPrefix + "Email", value, UserExpiresHours);
            }
        }

        public int Role
        {
            get
            {
                int role = 0;
                int.TryParse(Cookie.GetValue(KeyPrefix + "Role"), out role);
                return role;
            }
            set
            {
                Cookie.Save(KeyPrefix + "Role", value.ToString(), UserExpiresHours);
            }
        } 
        #endregion
    }
}
