using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniBook.Models;

namespace AlumniBook.ViewModels
{
    public class UserInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// 昵称/登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否实名认证
        /// Y:表示已经认证
        /// N:表示未认证【默认值】
        /// </summary>
        public string Certification { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadPortrait { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegistDate { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QqId { get; set; }
        /// <summary>
        /// 用户类型
        /// 0:普通用户
        /// 1.管理员
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 当前查看班级
        /// </summary>
        public ClassInfo ClassInfo { get; set; }
    }
}