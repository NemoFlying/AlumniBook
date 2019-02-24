using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniBook.BLL.UserService;
using AlumniBook.BLL.UserService.Dto;

namespace AlumniBook.Controllers
{
    public class UserController : AlumniBookControllerBase
    {
        private IUserServiceApplication _userService { get; set; }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public UserController() {
            _userService = new UserServiceApplication();
        }

        /// <summary>
        /// 注册新用户接收方法
        /// </summary>
        /// <returns></returns>
        public JsonResult Regist(RegistUserInput newUser)
        {
            //基本判断

            return Json(_userService.RegistUser(newUser), JsonRequestBehavior.AllowGet);
        }
    }
}