using Modules.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Utility;

namespace Modules.Contexts
{
    public class UserContext
    {
        protected IAuthCookie authCookie;
        public UserContext(IAuthCookie authCookie)
        {
            this.authCookie = authCookie;
        }

        public UserLoginInfo LoginInfo
        {
            get
            {
                if (authCookie.UserId == default(int) || authCookie.UserId == null)
                    return null;
                else if (DataSourceContext.Current.Users.FirstOrDefault(a => a.Id == authCookie.UserId && a.Email == authCookie.Email) == null)
                    return null;

                UserLoginInfo userlogininfo = new UserLoginInfo
                {
                    Email = authCookie.Email,
                    UserId = authCookie.UserId,
                    UserName = authCookie.UserName,
                    Role = authCookie.Role
                };
                return userlogininfo;
            }
        }

        public User UserInfo
        {
            get {
                if (this.LoginInfo == null)
                    return null;
                else {
                    return DataSourceContext.Current.Users.FirstOrDefault(a => a.Email == LoginInfo.Email);
                }
            }
        }
    }
}
