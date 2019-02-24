using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniBook.BLL.ClassInfoService;
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
            return Json(Mapper.Map<ClassQuestionViewModel>(
                _classInfoService.GetClassInfoById(classId).ClassQustion
                ), JsonRequestBehavior.AllowGet);
            
        }



    }
}