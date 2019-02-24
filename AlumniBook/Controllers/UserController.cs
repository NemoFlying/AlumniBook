using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniBook.BLL.UserService;
using AlumniBook.BLL.UserService.Dto;
using AlumniBook.Models;
using AlumniBook.ViewModels;
using AutoMapper;

namespace AlumniBook.Controllers
{
    public class UserController : AlumniBookBaseController
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

        /// <summary>
        /// 用户认证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult LogonAuthen(string userName,string password)
        {
            var result = _userService.LogonAuthen(userName, password);
            if(result.result)
            {
                //表示认证通过
                //Keeper Session
                HttpContext.Session["userinfo"] = Mapper.Map<UserInfo>((UserInfoOutput)result.Data);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void GetUserClassBaseInfo()
        {
            var kk = _userService.GetClassInfoByUid(1);
        }

        


    }
}