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
                HttpContext.Session["userinfo"] = Mapper.Map<UserViewModel>((UserInfoOutput)result.Data);
                result.Data = null;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取当前用户主页信息
        /// </summary>
        public JsonResult GetUserIndexInfo()
        {
            var classInfo = _userService.GetClassInfoByUid(GuserInfo.Id);
            var indexView = new IndexViewModel()
            {
                UserInfo = GuserInfo,
                AlumCoverImgUrl = classInfo.ClassAlbum.Find(con => con.IsCover == "Y").PhotoUrl,
                BannerImgUrl = classInfo.ClassAlbum.Find(con => con.IsCover == "Y").PhotoUrl,
                Classmate = Mapper.Map<List<UserViewModel>>(classInfo.Users),
                Bbs = Mapper.Map<List<LeavingMsgInfo>>(classInfo.ClassLeavingMessage),
                Notices = Mapper.Map<List<NoticeInfo>>(classInfo.ClassNotice)
            };
            return Json(indexView, JsonRequestBehavior.AllowGet);

        }

        


    }
}