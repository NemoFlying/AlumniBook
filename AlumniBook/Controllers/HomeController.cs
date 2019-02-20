using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using AlumniBook.DAL;
using AlumniBook.Models;
using AlumniBook.ViewModels;

namespace AlumniBook.Controllers
{
    public class HomeController : AlumniBookControllerBase
    {
        
        public ActionResult Index()
        {
            return View();
        }

        //登陆

        public ActionResult Logon()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetIndexModel()
        {
            var indexViewModel = new IndexViewModel();
            indexViewModel.UserInfo = GuserInfo;
            indexViewModel.BannerImgUrl = new List<string>();
            indexViewModel.Classmate = new List<UserInfo>();
            indexViewModel.Notices = new List<NoticeInfo>();
            indexViewModel.Bbs = new List<LeavingMsgInfo>();
            //获取Banner
            dbcontext.ClassAlbum
                .Where(con => con.ClassInfo.Id == GuserInfo.CurrentClass.Id)
                .Where(con => con.IsCover == "Y")
                .ToList()
                .ForEach(item => indexViewModel.BannerImgUrl.Add(item.PhotoUrl));
            //获取班级成员列表
            dbcontext.UserClass
                .Where(con => con.ClassInfo.Id == GuserInfo.CurrentClass.Id)
                .ToList()
                .ForEach(item => indexViewModel.Classmate.Add(
                    new UserInfo()
                    {
                        Certification = item.UserInfo.Certification,
                        HeadPortrait = item.UserInfo.HeadPortrait,
                        LogonUser = item.UserInfo.LogonUser,
                        UserType = item.UserInfo.UserType,

                    }
                    ));
            //获取留言信息
            dbcontext.ClassLeavingMessage
                .Where(con => con.ClassInfo.Id == GuserInfo.CurrentClass.Id)
                .ToList()
                .ForEach(item => indexViewModel.Bbs.Add(
                   new LeavingMsgInfo()
                   {
                       ClassName = item.ClassInfo.ClassName,
                       FromUser = item.CreateUser.LogonUser,
                       HeadPortrait = item.CreateUser.HeadPortrait,
                       Notice = item.Msg,
                       Id = item.Id

                   }
                    ));
            //获取公告信息
            dbcontext.ClassNotice
                .Where(con => con.ClassInfo.Id == GuserInfo.CurrentClass.Id)
                .ToList()
                .ForEach(item => indexViewModel.Notices.Add(
                   new NoticeInfo()
                   {
                       ClassName = item.ClassInfo.ClassName,
                       Notice = item.Notice,
                       Id = item.Id,
                       NoticDate= item.CreateDate
                   }
                    ));
            //获取相册【当前为Banner】
            indexViewModel.AlumCoverImgUrl = indexViewModel.BannerImgUrl.Count <= 0 ? null : indexViewModel.BannerImgUrl[0];
            return Json(indexViewModel, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 用户登陆认证
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoginAuthentication(LoginInput loginInput)
        {
            var md5 = new MD5CryptoServiceProvider();
            var passWord = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(loginInput.LogonUser + loginInput.Password)));
            passWord = passWord.Replace("-", "");
            var logonUser = dbcontext.RegisteredUser
                .Where(con => con.LogonUser == loginInput.LogonUser && con.Password == passWord)
                .FirstOrDefault();
            var Result = new JsonReMsg()
            {
                Status = "OK"
            };
            if (logonUser is null)
            {
                Result.Status = "ERR";
                logonUser = dbcontext.RegisteredUser
                    .Where(con => con.LogonUser == loginInput.LogonUser)
                    .FirstOrDefault();
                if (logonUser is null)
                {
                    Result.Msg = "用户尚未注册，请先注册";
                }
                else
                {
                    Result.Msg = "用户密码不符，请注意大小写！";
                }
            }
            else
            {
                //认证通过KeepSession
                var userInfo = new UserInfo()
                {
                    Certification = logonUser.Certification,
                    HeadPortrait = logonUser.HeadPortrait,
                    LogonUser = logonUser.LogonUser,
                    UserType = logonUser.UserType,
                    ClassId = new List<int>()
                };
                var classInfos = dbcontext.UserClass
                    .Where(con => con.UserInfo.Id == logonUser.Id)
                    .ToList();
                //保存该用户加入的班级列表
                //当有多个的时候在前端要用户进行选择【补充功能】
                classInfos.ForEach(item => userInfo.ClassId.Add(item.ClassInfo.Id));
                if (classInfos.Count == 1)
                {
                    //表示已经申请了一个班级
                    userInfo.CurrentClass = classInfos[0].ClassInfo;
                }
                //保存Session
                HttpContext.Session["userinfo"] = userInfo;

                Result.Data = userInfo;//将用户基本信息传回前台
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 注册新用户接收方法
        /// </summary>
        /// <returns></returns>
        public JsonResult Regist(string logonUser,string password)
        {
            JsonReMsg reData = new JsonReMsg();
            //用户名验证
            //1.命名规则验证【略】
            //2.重复验证
            var user = dbcontext.RegisteredUser
                .Where(con => con.LogonUser == logonUser)
                .ToList();
            if(user.Count>0)
            {
                reData.Status = "ERR";
                reData.Msg = "用户名已经存在！";
                return Json(reData, JsonRequestBehavior.AllowGet);
            }
            //password规则验证【略】
            //password加密
            var md5 = new MD5CryptoServiceProvider();
            var passWord = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(logonUser + password)));
            passWord = passWord.Replace("-", "");
            try
            {
                dbcontext.RegisteredUser.Add(new RegisteredUser()
                {
                    LogonUser = logonUser,
                    Password = password
                });
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                //日志记录【略】
                //返回错误信息
                reData.Status = "ERR";
                reData.Msg = "服务内部错误，请联系管理员处理！";
                return Json(reData, JsonRequestBehavior.AllowGet);
            }
            //返回刚注册的用户信息
            reData.Status = "OK";
            var newUser = dbcontext.RegisteredUser.Where(con => con.LogonUser == logonUser).FirstOrDefault();
            var userInfo = new UserInfo()
            {
                Id = newUser.Id,
                LogonUser = newUser.LogonUser,
                HeadPortrait = newUser.HeadPortrait,
                Certification = newUser.Certification,
                UserType = newUser.UserType
            };
            reData.Data = userInfo;
            return Json(reData, JsonRequestBehavior.AllowGet);
        }

       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}