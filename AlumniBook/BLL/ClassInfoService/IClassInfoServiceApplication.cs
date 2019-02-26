﻿using System;
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

        // <summary>
        /// 删除班级相册
        /// </summary>
        /// <param name="albumsId"></param>
        /// <returns></returns>
        ResultBaseOutput DeleteClassAlbums(int classId, int albumsId);

        /// <summary>
        /// 为班级添加照片
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="userId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        ResultBaseOutput AddClassAlbums(int classId, string userName, string url);

        /// <summary>
        /// 添加BBS留言
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="userId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        ResultBaseOutput AddClassBbs(int classId, int userId, string msg);
    }
}
