using Modules.Contexts;
using Modules.ControllerBase;
using Modules.Database;
using Modules.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Exercise.Common
{
    public class AdminBaseController : BaseController
    {
        public QuestionnaireDBEntities db
        {
            get
            {
                return DataSourceContext.Current;
            }
        }


        public QuestionnaireCookieContext CookieContext
        {
            get
            {
                return QuestionnaireCookieContext.Current;
            }
        }

        public QuestionnaireUserContext UserContext
        {
            get
            {
                return QuestionnaireUserContext.Current;
            }
        }

        public virtual UserLoginInfo LoginInfo
        {
            get
            {
                return UserContext.LoginInfo;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var noAuthorizeAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizeIgnoreAttribute), false);
            if (noAuthorizeAttributes.Length > 0)
                return;

            base.OnActionExecuting(filterContext);

            if (this.LoginInfo == null)
            {
                filterContext.Result = RedirectToAction("Login", "Auth", new { Area = "Admin" });
                return;
            }

            if (LoginInfo.Role != (int)EnumUserRole.Admin)
            {
                filterContext.Result = Content("没有权限！");
            }
        }
    }
}