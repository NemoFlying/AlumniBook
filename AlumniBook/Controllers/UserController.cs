﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniBook.BLL;
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
            if(result.Status)
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

            var indexView = new IndexViewModel();
            var alum = classInfo.ClassAlbum==null?null: classInfo.ClassAlbum.ToList().Find(con => con.IsCover == "Y");
            indexView.AlumCoverImgUrl = alum == null ? "../assets/Images/defaultPhoto.jpg" : alum.PhotoUrl;
            indexView.BannerImgUrl = alum == null ? "../assets/Images/defaultPhoto.jpg" : alum.PhotoUrl;
            indexView.Classmate = Mapper.Map<List<UserViewModel>>(classInfo.User);
            indexView.Bbs = Mapper.Map<List<LeavingMessageViewModel>>(classInfo.ClassLeavingMessage);
            indexView.Notices = Mapper.Map<List<NoticeViewModel>>(classInfo.ClassNotice);
            indexView.UserInfo = Mapper.Map<UserViewModel>(GuserInfo);
            indexView.ClassInfo = Mapper.Map<ClassInfoBaseViewModel>(classInfo);
            return Json(indexView, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 获取当前班级所有学生
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllClassUser()
        {
            return Json(Mapper.Map<List<UserViewModel>>(_userService.GetAllClassUser(GuserInfo.CurrentClass.Id)), JsonRequestBehavior.AllowGet);
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
                if (delResult.Status)
                {
                    reJson.Status = "OK";
                    reJson.Data = Mapper.Map<List<UserViewModel>>(_userService.GetAllClassUser(GuserInfo.CurrentClass.Id));
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
        public JsonResult UpdateUserInfo(UserInfoUpdateInput userInput)
        {
            var result = new ResultBaseOutput();
            //if() 实名认证部分
            //若实名认证过就不需认证
            userInput.Certification = "Y";
            result = _userService.UpdateUser(userInput);
            if(result.Status)
            {
                HttpContext.Session["userinfo"] = Mapper.Map<UserInfoOutput>(result.Data);
                result.Data = Mapper.Map<UserViewModel>(result.Data);
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}