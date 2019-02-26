using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniBook.BLL;
using AlumniBook.BLL.ClassInfoService;
using AlumniBook.BLL.Dto;
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
        /// 获得所有班级列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllClassForSelect()
        {
            var options = new List<SelectViewModel>();
            _classInfoService.GetAllClassInfo().ForEach(
                item => options.Add(new SelectViewModel()
                {
                    Key = item.Id.ToString(),
                    Value = item.ClassName
                })
                );
            return Json(options, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据ClassId 获得问题描述
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public JsonResult GetClassQuestionByClassId(int classId)
        {
            return Json(Mapper.Map<List<QuestionViewModel>>(
                _classInfoService.GetClassInfoById(classId).ClassQustion
                ), JsonRequestBehavior.AllowGet);
            
        }

        /// <summary>
        /// 获取当前班级公告
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCurrentClassNotice()
        {
            var reJson = new JsonReMsg() { Status = "OK" };
            reJson.Data = Mapper.Map<List<NoticeViewModel>>(_classInfoService.GetAllNotices(
                GuserInfo.CurrentClass.Id
                ));
            return Json(reJson, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetAllClassNotice()
        //{
        //    var reJson = new JsonReMsg() { Status = "OK" };
        //    if(GuserInfo.us)
        //}
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

        public JsonResult AddClassNotice(NoticeViewModel newNotice)
        {
            var reJson = new JsonReMsg();
            //判断是否具有权限
            if (GuserInfo.UserType !=1 )
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
            if(GuserInfo.UserType != 1)
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
        public JsonResult AddClassAlbums()
        {
            var result = new ResultBaseOutput();
            try
            {
                HttpPostedFileBase img = Request.Files["imgFile"];
                string fileName = Guid.NewGuid().ToString().Replace("-", "") + "." + Path.GetExtension(img.FileName);
                string filepath = "/Images/Album/" + GuserInfo.CurrentClass.ClassName + "/";
                if (Directory.Exists(Server.MapPath(filepath)) == false)
                {
                    Directory.CreateDirectory(Server.MapPath(filepath));
                }
                string virpath = filepath + fileName;
                img.SaveAs(Server.MapPath(virpath));
                result.Status = true;
            }
            catch(Exception ex)
            {
                result.Status = false;
                result.Msg = "上传失败";
                result.Data = ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        
    }
}