using Modules.Database;
using Modules.Enums;
using Modules.Helpers;
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
                ViewBag.Msg = "非用户帐号";
                return View();
            }
            else
            {
                ViewBag.Msg = "用户名或密码错误";
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
            if (db.Users.FirstOrDefault(a => a.Email == registerInfo.Email) != null)
            {
                ViewBag.Msg = "该邮箱已被注册";
                return View();
            }
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
            TempData["Msg"] = "注册成功!";
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

        [AuthorizeIgnore]
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [AuthorizeIgnore]
        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {
            var user = db.Users.FirstOrDefault(a => a.Email == email);
            if (user == null)
            {
                ViewBag.Msg = "该邮箱没有注册";
                return View();
            }
            else {
                ViewBag.Msg = "提交成功, 请登录您的邮箱按操作修改密码";
                ForgetPasswordHelper.SendEmail(email);
                return View();
            }
        }

        [AuthorizeIgnore]
        /// <summary>
        /// 从邮箱打开的链接
        /// </summary>
        /// <returns></returns>
        public ActionResult FindPassword(string id)
        {
            var temp = HttpRuntime.Cache.Get(id);
            if (temp != null)
            {
                string s = temp.ToString();
                var account = db.Users.FirstOrDefault(a => a.Email == s);
                if (account != null)
                {
                    return View(account);
                }
            }
            return Content("链接已失效,请重新执行找回密码操作");
        }
        [AuthorizeIgnore]
        public ActionResult ResetPassword(string id, string newPassword)
        {
            int id_int = Int32.Parse(id);
            var user = db.Users.FirstOrDefault(a => a.Id == id_int);
            user.Password = newPassword;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["Msg"] = "重置密码成功, 请用新密码登录";
            return RedirectToAction("Login");
        }
    }
}