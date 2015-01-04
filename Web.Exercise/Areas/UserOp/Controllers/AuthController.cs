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

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="email">登录邮箱</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeIgnore]
        public ActionResult Login(string email, string password)
        {
            //查询数据库
            var logininfo = db.Users.FirstOrDefault(a => a.Email == email && a.Password == password);
            //如果通过验证
            if (logininfo != null && logininfo.Role == (int)EnumUserRole.Register)
            {
                //将登录信息写入cookie
                this.CookieContext.Email = logininfo.Email;
                this.CookieContext.Role = logininfo.Role;
                this.CookieContext.UserId = logininfo.Id;
                this.CookieContext.UserName = logininfo.Name;
                //重定向到首页
                return RedirectToAction("index", "uhome", new { Area = "UserOp" });
            }
            //登录不成功, 则报错
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

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="registerInfo">注册信息模型绑定</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeIgnore]
        public ActionResult Register(User registerInfo)
        {
            //检测邮箱是否已被注册
            if (db.Users.FirstOrDefault(a => a.Email == registerInfo.Email) != null)
            {
                ViewBag.Msg = "该邮箱已被注册";
                return View();
            }
            //如果注册信息合法, 则将注册信息添加到数据库
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
        /// 从找回密码邮件中打开的重置密码的链接
        /// </summary>
        /// <returns></returns>
        public ActionResult FindPassword(string id)
        {
            //从Cache中读取临时信息
            var temp = HttpRuntime.Cache.Get(id);
            //如果链接有效, 则打开重置密码的页面
            if (temp != null)
            {
                string s = temp.ToString();
                var account = db.Users.FirstOrDefault(a => a.Email == s);
                if (account != null)
                {
                    return View(account);
                }
            }
            //链接无效则则提示错误
            return Content("链接已失效,请重新执行找回密码操作");
        }

        /// <summary>
        /// 接收重置密码的信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        [AuthorizeIgnore]
        public ActionResult ResetPassword(string id, string newPassword)
        {
            //更新用户密码并保存到数据库
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