using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.ViewModels
{
    /// <summary>
    /// 公告信息
    /// </summary>
    public class NoticeInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// 发布公告时间
        /// </summary>
        public DateTime NoticDate { get; set; }

    }
}