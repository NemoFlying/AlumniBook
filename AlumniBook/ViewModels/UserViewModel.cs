using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.ViewModels
{
    public class UserViewModel
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
        /// 班级名称
        /// 
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 0=>超级管理员
        /// 1=>系统管理员
        /// 2=>普通用户
        /// </summary>
        public string userType { get; set; }
    }
}