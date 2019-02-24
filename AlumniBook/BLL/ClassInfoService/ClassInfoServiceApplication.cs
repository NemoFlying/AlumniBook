using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniBook.DAL;
using AlumniBook.Models;

namespace AlumniBook.BLL.ClassInfoService
{
    public class ClassInfoServiceApplication: IClassInfoServiceApplication
    {
        private IClassInfoDAL _classDAL { get; set; }
        //private IClass _classDAL { get; set; }
        public ClassInfoServiceApplication()
        {
            _classDAL = new ClassInfoDAL();

        }

        /// <summary>
        /// 获取所有班级信息
        /// </summary>
        /// <returns></returns>
        public List<ClassInfo> GetAllClassInfo()
        {
            return _classDAL.GetModels(con => 1 == 1).ToList();
        }

        /// <summary>
        /// 根据班级ID获取班级问题答案
        /// </summary>
        /// <returns></returns>
        public ClassInfo GetClassInfoById(int classId)
        {
            return _classDAL.GetModels(con => con.Id == classId).FirstOrDefault();
        }

    }
}