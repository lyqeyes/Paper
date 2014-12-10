using Modules.Database;
using Modules.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Exercise.Common;

namespace Web.Exercise.Areas.UserOp.Controllers
{
    public class AuthController : UserBaseController
    {
        // GET: UserOp/Auth

        #region 登录
        [AuthorizeIgnore]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeIgnore]
        public ActionResult Login(string email, string password)
        {
            var logininfo = db.Users.FirstOrDefault(a => a.Email == email && a.Password == password);
            if (logininfo != null && logininfo.Role == (int)EnumUserRole.Register)
            {
                this.CookieContext.Email = logininfo.Email;
                this.CookieContext.Role = logininfo.Role;
                this.CookieContext.UserId = logininfo.Id;
                this.CookieContext.UserName = logininfo.Name;

                return RedirectToAction("index", "uhome", new { Area = "UserOp" });
            }
            else if (logininfo != null && logininfo.Role != (int)EnumUserRole.Register)
            {
                ModelState.AddModelError("error", "非用户帐号");
                return View();
            }
            else
            {
                ModelState.AddModelError("error", "帐号或密码错误");
                return View();
            }
        } 
        #endregion

        [AuthorizeIgnore]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeIgnore]
        public ActionResult Register(User registerInfo)
        {
            //registerInfo.CreateTime = DateTime.Now;
            //registerInfo.Role = (int)EnumUserRole.Register;
            //registerInfo.State = (int)EnumUserState.IsAble;
            var db_2 = new QuestionnaireDBEntities();
            db_2.Users.Add(new User { 
                Email = registerInfo.Email,
                Phone = registerInfo.Phone,
                Name = registerInfo.Name,
                Password = registerInfo.Password,
                CreateTime = DateTime.Now,
                State = (int)EnumUserState.IsAble,
                Role = (int)EnumUserRole.Register
            });
            db_2.SaveChanges();
            return RedirectToAction("Login");
        }


        public ActionResult Logout()
        {
            this.CookieContext.Email = String.Empty;
            this.CookieContext.Role = default(int);
            this.CookieContext.UserId = 0;
            this.CookieContext.UserName = String.Empty;
            return RedirectToAction("Login");
        }
    }
}