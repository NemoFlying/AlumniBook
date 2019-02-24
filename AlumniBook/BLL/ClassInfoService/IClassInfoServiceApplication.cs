using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlumniBook.Models;

namespace AlumniBook.BLL.ClassInfoService
{
    public interface IClassInfoServiceApplication
    {
        /// <summary>
        /// 获取所有班级信息
        /// </summary>
        /// <returns></returns>
        List<ClassInfo> GetAllClassInfo();


        /// <summary>
        /// 根据班级ID获取班级问题答案
        /// </summary>
        /// <returns></returns>
        ClassInfo GetClassInfoById(int classId);
    }
}
