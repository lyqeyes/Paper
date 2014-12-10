using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Exercise.Common;

namespace Web.Exercise.Areas.UserOp.Controllers
{
    public class UHomeController : UserBaseController
    {
        // GET: UserOp/Home
        
        /// <summary>
        /// 介绍页面
        /// </summary>
        /// <returns></returns>
        [AuthorizeIgnore]
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// 登录后的用户主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}