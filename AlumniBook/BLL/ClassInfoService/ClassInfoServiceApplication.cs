using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniBook.BLL.ClassInfoService.Dto;
using AlumniBook.DAL;
using AlumniBook.Models;
using AlumniBook.ViewModels;
using AutoMapper;

namespace AlumniBook.BLL.ClassInfoService
{
    public class ClassInfoServiceApplication: IClassInfoServiceApplication
    {
        private IClassInfoDAL _classDAL { get; set; }
        private IClassNoticeDAL _noticeDAL { get; set; }
        public ClassInfoServiceApplication()
        {
            _classDAL = new ClassInfoDAL();
            _noticeDAL = new ClassNoticeDAL();
        }

        /// <summary>
        /// 【获取所有班级信息】
        /// </summary>
        /// <returns></returns>
        public List<ClassInfo> GetAllActiveClassInfo()
        {
            return _classDAL.GetModels(con => con.ClassQustion.Count() == 3).ToList();
        }

        /// <summary>
        /// 【根据班级ID获取班级信息】
        /// </summary>
        /// <returns></returns>
        public ClassInfo GetClassInfoById(int classId)
        {
            return _classDAL.GetModels(con => con.Id == classId).FirstOrDefault();
        }


        /// <summary>
        /// 跟新班级基本信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public ResultBaseOutput UpdateClassBaseInfo(int userId,ClassInfoBaseUpdateInput input)
        {
            var result = new ResultBaseOutput();
            var classInfo = _classDAL.GetModels(con => con.Id == input.Id).FirstOrDefault();
            if (classInfo is null)
            {
                result.Status = false;
                result.Msg = "未找到班级信息！";
                return result;
            }
            if(!string.IsNullOrEmpty(input.Introduce))
            {
                //修改描述
                classInfo.Introduce = input.Introduce;
            }
            if(!string.IsNullOrEmpty(input.ClassNotice))
            {
                //添加公告
                classInfo.ClassNotice.Add(new ClassNotice()
                {
                    CreateUser = classInfo.User.Where(con => con.Id == userId).FirstOrDefault(),
                    Notice = input.ClassNotice
                });
            }
            if(input.qa !=null&&input.qa.Count==3)
            {
                classInfo.ClassQustion = new List<ClassQuestion>();
                //问题&答案添加
                input.qa.ForEach(item =>
                classInfo.ClassQustion.Add(Mapper.Map<ClassQuestion>(item))
                );
            }
            try
            {
                _classDAL.SaveChanges();
                result.Status = true;
                result.Data = classInfo;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Msg = "删除失败";
                result.Data = ex;
            }
            return result;

        }


        /// <summary>
        /// 获取班级所有公告信息
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<ClassNotice> GetAllNotices(int? classId)
        {
            return _classDAL.GetModels(con => classId.HasValue ? con.Id == classId : 1 == 1).FirstOrDefault()
                .ClassNotice.ToList();
        }



        /// <summary>
        /// 删除NoticeId
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public ResultBaseOutput DeleteNoticeId( int noticeId)
        {
            var result = new ResultBaseOutput();
            var notice = _noticeDAL.GetModels(con => con.Id == noticeId).FirstOrDefault();
            _noticeDAL.Delete(notice);

            try
            {
                _noticeDAL.SaveChanges();
                result.Status = true;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Msg = "删除失败";
                result.Data = ex;
            }
            return result;
        }

        /// <summary>
        /// 添加新的公告
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="newnotice"></param>
        /// <returns></returns>
        public ResultBaseOutput AddClassNotice(int classId,NoticeInput newnotice)
        {
            var notice = Mapper.Map<ClassNotice>(newnotice);
            notice.ClassInfo = _classDAL.GetModels(con => con.Id == classId).FirstOrDefault();
            _noticeDAL.Add(notice);
            var result = new ResultBaseOutput();
            try
            {
                _noticeDAL.SaveChanges();
                result.Status = true;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Msg = "添加失败";
                result.Data = ex;
            }
            return result;
        }

        /// <summary>
        /// 删除班级相册
        /// </summary>
        /// <param name="albumsId"></param>
        /// <returns></returns>
        public ResultBaseOutput DeleteClassAlbums(int classId,int albumsId)
        {
            var result = new ResultBaseOutput();
            var classInfo = _classDAL.GetModels(con => con.Id == classId)
                .FirstOrDefault();
            classInfo.ClassAlbum.Remove(classInfo.ClassAlbum.FirstOrDefault(con => con.Id == albumsId));
            _classDAL.Update(classInfo);
            try
            {
                _noticeDAL.SaveChanges();
                result.Status = true;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Msg = "删除失败";
                result.Data = ex;
            }
            return result;
        }

        /// <summary>
        /// 为班级添加照片
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="userId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public ResultBaseOutput AddClassAlbums(int classId,string userName,string url)
        {
            var result = new ResultBaseOutput();
            var classinfo = _classDAL.GetModels(con => con.Id == classId).FirstOrDefault();
            var album = new ClassAlbum()
            {
                IsCover = "N",
                CreateUser = userName,
                PhotoUrl = ".." + url
            };
            classinfo.ClassAlbum.Add(album);
            try
            {
                _classDAL.SaveChanges();
                result.Status = true;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Msg = "删除失败";
                result.Data = ex;
            }
            return result;
        }

        /// <summary>
        /// 添加BBS留言
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="userId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ResultBaseOutput AddClassBbs(int classId, int userId,string msg)
        {
            var result = new ResultBaseOutput();
            var classinfo = _classDAL.GetModels(con => con.Id == classId).FirstOrDefault();
            var bbs = new ClassLeavingMessage()
            {
                ClassInfo = classinfo,
                Msg = msg,
                User = classinfo.User.ToList().Find(con => con.Id == userId)
            };
            classinfo.ClassLeavingMessage.Add(bbs);
            try
            {
                //_bbs.Add(bbs);
                _classDAL.SaveChanges();
                result.Status = true;
                result.Data = bbs;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Msg = "删除失败";
                result.Data = ex;
            }
            return result;

        }

    }
}