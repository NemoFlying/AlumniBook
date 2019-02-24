﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniBook.Models;

namespace AlumniBook.BLL.UserService.Dto
{
    public class UserInfoOutput
    {
        public int Id { get; set; }
        /// <summary>
        /// 登陆用户名
        /// 不能重复
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户类型
        /// 0:普通用户
        /// 1.管理员
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 是否实名认证
        /// Y:表示已经认证
        /// N:表示未认证【默认值】
        /// </summary>
        public string Certification { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NikeName { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string QqId { get; set; }

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

        public ClassInfo classInfo { get; set; }
    }
}