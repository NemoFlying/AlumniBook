using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.ViewModels
{
    public class LeavingMsgInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 留言用户名
        /// </summary>
        public string FromUser { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadPortrait { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Notice { get; set; }
    }
}