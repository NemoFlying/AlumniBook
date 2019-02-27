using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniBook.BLL;
using AlumniBook.BLL.ClassInfoService;
using AlumniBook.BLL.ClassInfoService.Dto;
using AlumniBook.Models;
using AlumniBook.ViewModels;
using AutoMapper;

namespace AlumniBook.Controllers
{
    public class ClassInfoController : AlumniBookBaseController
    {
        
        private IClassInfoServiceApplication _classInfoService { get; set; }
        // GET: ClassInfo
        public ActionResult Index()
        {
            return View();
        }

        public ClassInfoController()
        {
            _classInfoService = new ClassInfoServiceApplication();
        }

        /// <summary>
        /// 【获得所有班级列表】
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllActiveClassForSelect()
        {
            var options = new List<SelectViewModel>();
            _classInfoService.GetAllActiveClassInfo().ForEach(
                item => options.Add(new SelectViewModel()
                {
                    Key = item.Id.ToString(),
                    Value = item.ClassName
                })
                );
            return Json(options, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 根据班级管理员获得班级列表【扩展用】
        /// </summary>
        /// <returns></returns>
        //public JsonResult GetClassInfoByAdminUser()
        //{

        //}

        /// <summary>
        /// 【根据ClassId 获得问题描述】
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetClassQuestionByClassId(int classId)
        {
            var qa = Mapper.Map<List<QuestionViewModel>>(
                _classInfoService.GetClassInfoById(classId).ClassQustion);
            qa.ForEach(item => item.Answer = "");
            return Json(qa, JsonRequestBehavior.AllowGet);
            
        }

        //班级管理员管理部分
        /// <summary>
        /// 获取班级基本信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCurrentClassInfo()
        {
            var result = new ResultBaseOutput();
            result.Data = Mapper.Map<ClassInfoViewModel>(GuserInfo.CurrentClass);
            result.Status = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改班级基本信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClassInfoBaseUpdate(ClassInfoBaseUpdateInput input)
        {
            var result = _classInfoService.UpdateClassBaseInfo(GuserInfo.Id, input);
            result.Data = Mapper.Map<ClassInfoViewModel>(result.Data);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddClassNotice(NoticeViewModel newNotice)
        {
            var reJson = new JsonReMsg();
            //判断是否具有权限
            if (GuserInfo.UserType != 0)
            {
                reJson.Status = "ERR";
                reJson.Msg = "没有发布公告权限";
            }
            else
            {
                var addresult = _classInfoService.AddClassNotice(GuserInfo.CurrentClass.Id, Mapper.Map<NoticeInput>(newNotice));
                if (addresult.Status)
                {
                    reJson.Status = "OK";
                    reJson.Data = Mapper.Map<List<NoticeViewModel>>(_classInfoService.GetAllNotices(GuserInfo.CurrentClass.Id));
                }
                else
                {
                    //删除失败
                    reJson.Status = "ERR";
                    reJson.Msg = addresult.Msg;
                    reJson.Data = addresult.Data;
                }
            }
            return Json(reJson, JsonRequestBehavior.AllowGet);
        }



        //END 班级管理员部分




        /// <summary>
        /// 获取当前班级公告
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCurrentClassNotice()
        {
            var reJson = new JsonReMsg() { Status = "OK" };
            reJson.Data = Mapper.Map<List<NoticeViewModel>>(_classInfoService.GetAllNotices(
                GuserInfo.CurrentClass.Id
                ).OrderBy(con=>con.CreateDate));
            return Json(reJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除班级公告信息
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public JsonResult DeleteClassNotice(int noticeId)
        {
            var reJson = new JsonReMsg();
            //判断是否具有权限
            if (GuserInfo.UserType != 1)
            {
                reJson.Status = "ERR";
                reJson.Msg = "没有权限删除";
            }
            else
            {
                var delResult = _classInfoService.DeleteNoticeId(noticeId);
                if (delResult.Status)
                {
                    reJson.Status = "OK";
                    reJson.Data = Mapper.Map<List<NoticeViewModel>>(_classInfoService.GetAllNotices(GuserInfo.CurrentClass.Id));
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

        

        /// <summary>
        /// 获取当前班级相册列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCurrentClassAlbums()
        {
            return Json(Mapper.Map<List<AlbumViewModel>>(GuserInfo.CurrentClass.ClassAlbum.ToList()), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除相册【当前班级管理员才能删除】
        /// </summary>
        /// <param name="albumsId"></param>
        /// <returns></returns>
        public JsonResult DeleteClassAlbums(int albumsId)
        {
            var result = new ResultBaseOutput();
            ///权限判断
            if(GuserInfo.UserType >= 2)
            {
                //表示没有权限删除
                result.Status = false;
                result.Msg = "权限不足";
            }
            else
            {
                result = _classInfoService.DeleteClassAlbums(GuserInfo.CurrentClass.Id, albumsId);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddClassAlbums(HttpPostedFileBase imgFile1)
        {
            var result = new ResultBaseOutput();
            try
            {
                HttpPostedFileBase img = Request.Files["imgFile1"];
                string fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(img.FileName);
                string filepath = "/assets/Images/Album/" + GuserInfo.CurrentClass.ClassName + "/";
                if (Directory.Exists(Server.MapPath(filepath)) == false)
                {
                    Directory.CreateDirectory(Server.MapPath(filepath));
                }
                string virpath = filepath + fileName;
                img.SaveAs(Server.MapPath(virpath));
                result.Status = true;
                result = _classInfoService.AddClassAlbums(GuserInfo.CurrentClass.Id, GuserInfo.UserName, virpath);
                if(result.Status)
                {
                    result.Data = ".." + virpath;
                }
            }
            catch(Exception ex)
            {
                result.Status = false;
                result.Msg = "上传失败";
                result.Data = ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加留言信息
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public JsonResult AddClassBbs(string Msg)
        {
            var result = new ResultBaseOutput();
            ///权限判断
            if (GuserInfo.Certification != "Y")
            {
                //表示没有权限删除
                result.Status = false;
                result.Msg = "请先实名认证!";
            }
            else
            {
                result = _classInfoService.AddClassBbs(GuserInfo.CurrentClass.Id, GuserInfo.Id, Msg);
                result.Data = Mapper.Map<LeavingMessageViewModel>((ClassLeavingMessage)(result.Data));
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
    }
}