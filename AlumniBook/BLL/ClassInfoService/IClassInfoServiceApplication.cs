using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlumniBook.BLL.Dto;
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
        ClassInfo GetClassInfoById(int? classId);

        /// <summary>
        /// 获取班级所有公告信息
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        List<ClassNotice> GetAllNotices(int? classId);


        /// <summary>
        /// 删除NoticeId
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        ResultBaseOutput DeleteNoticeId(int noticeId);

        /// <summary>
        /// 添加新的公告
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="newnotice"></param>
        /// <returns></returns>
        ResultBaseOutput AddClassNotice(int classId, NoticeInput newnotice);

    }
}
