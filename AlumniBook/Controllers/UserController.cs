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
        /// 【注册新用户接收方法】
        /// </summary>
        /// <returns></returns>
        public JsonResult Regist(RegistUserInput newUser)
        {
            //基本判断

            return Json(_userService.RegistUser(newUser), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 【用户认证】
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
                //Keep Session
                HttpContext.Session["userinfo"] = result.Data;
                result.Data = Mapper.Map<UserViewModel>(result.Data);
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
                UserInfo = Mapper.Map<UserViewModel>( GuserInfo),
                AlumCoverImgUrl = classInfo.ClassAlbum.ToList().Find(con => con.IsCover == "Y").PhotoUrl,
                BannerImgUrl = classInfo.ClassAlbum.ToList().Find(con => con.IsCover == "Y").PhotoUrl,
                Classmate = Mapper.Map<List<UserViewModel>>(classInfo.User),
                Bbs = Mapper.Map<List<LeavingMessageViewModel>>(classInfo.ClassLeavingMessage),
                Notices = Mapper.Map<List<NoticeViewModel>>(classInfo.ClassNotice),
                ClassInfo = Mapper.Map<ClassInfoViewModel>(classInfo)
            };
            return Json(indexView, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 获取当前班级所有学生
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllClassUser()
        {
            return Json(Mapper.Map<List<UserViewModel>>(_userService.GetAllClassUser(GuserInfo.UserClass[0].Id)), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public JsonResult DeleteUserById(int userId)
        {
            var reJson = new JsonReMsg();
            //判断是否具有权限
            if(GuserInfo.UserType!=1)
            {
                reJson.Status = "ERR";
                reJson.Msg = "没有权限删除";
            }else
            {
                var delResult = _userService.DeleteUserById(userId);
                if (delResult.result)
                {
                    reJson.Status = "OK";
                    reJson.Data = Mapper.Map<List<UserViewModel>>(_userService.GetAllClassUser(GuserInfo.UserClass[0].Id));
                }
                else
                {
                    //删除失败
                    reJson.Status = "ERR";
                    reJson.Msg = delResult.Msg;
                    reJson.Data = delResult.Data;
                }
            }
            return Json(reJson, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult ModifyUserInfo(userinfo)

    }
}