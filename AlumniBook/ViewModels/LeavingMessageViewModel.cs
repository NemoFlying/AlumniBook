using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.ViewModels
{
    public class LeavingMessageViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 留言信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string createUserName { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QqId { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadPortrait { get; set; }
    }
}