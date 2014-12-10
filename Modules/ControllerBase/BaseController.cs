using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Modules.ControllerBase
{
    public class BaseController:Controller
    {
        public virtual int PageSize {
            get {
                return 15;
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        protected virtual ActionResult HttpNotPermission()
        {
            return RedirectToAction("NoPermission", "Home");
        }
    }
}
