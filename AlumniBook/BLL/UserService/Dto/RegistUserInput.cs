﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.BLL.UserService.Dto
{
    /// <summary>
    /// 注册可注册两种类型
    /// 普通用户【需要提交申请加入班级的问题答案】
    /// 管理员【注册成功后需要添加班级信息&问题及答案】
    /// </summary>
    public class RegistUserInput
    {
        /// <summary>
        /// 登陆用户名
        /// 不能重复
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 注册类型
        /// 0:普通用户
        /// 1.管理员
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QqId { get; set; }

        /// <summary>
        /// 普通用户申请班级ID
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// 新创建班级的名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 问题
        /// </summary>
        public List<string> Question
        {
            get;set;
        }
        /// <summary>
        /// 答案
        /// </summary>
        public List<string> Anser
        {
            get; set;
        }

        /// <summary>
        /// 班级介绍
        /// </summary>
        public string Introduce { get; set; }
    }
}