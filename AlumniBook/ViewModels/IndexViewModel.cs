using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniBook.Models;

namespace AlumniBook.ViewModels
{
    public class IndexViewModel
    {
        /// <summary>
        /// 用户基本信息
        /// </summary>
        public UserViewModel UserInfo { get; set; }

        /// <summary>
        /// Banner IMG Url
        /// </summary>
        public string BannerImgUrl { get; set; }

        /// <summary>
        /// 班级相册封面地址
        /// </summary>
        public string AlumCoverImgUrl { get; set; }

        /// <summary>
        /// 同学列表
        /// </summary>
        public List<UserViewModel> Classmate { get; set; }

        /// <summary>
        /// 公告列表
        /// </summary>
        public List<NoticeViewModel> Notices { get; set; }

        /// <summary>
        /// 留言信息列表
        /// </summary>
        public List<LeavingMsgInfo> Bbs { get; set; }
        /// <summary>
        /// 班级信息
        /// </summary>
        public ClassInfoViewModel ClassInfo { get; set; }
    }
}