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
    public class UserBaseController : BaseController
    {
        //public QuestionnaireDBEntities db
        //{
        //    get
        //    {
        //        return DataSourceContext.Current;
        //    }
        //}
        public QuestionnaireDBEntities db = new QuestionnaireDBEntities();

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
            //1. 是否忽略验证
            var noAuthorizeAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizeIgnoreAttribute), false);
            if (noAuthorizeAttributes.Length > 0)
                return;
            //2.是否已登录
            if (this.LoginInfo == null)
            {
                filterContext.Result = RedirectToAction("Login", "Auth", new { Area = "UserOp"});
                return;
            }
            //3. 帐号权限检测
            if (this.LoginInfo.Role != (int)EnumUserRole.Register)
            {
                filterContext.Result = Content("非注册用户");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}