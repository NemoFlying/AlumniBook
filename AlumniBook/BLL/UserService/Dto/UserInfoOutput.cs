using System;
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

        public List<ClassInfo> UserClass { get; set; }

        /// <summary>
        /// 当前用户查看班级
        /// </summary>
        public ClassInfo CurrentClass { get { return UserClass[0]; } }

        /// <summary>
        /// 0=>超级管理员
        /// 1=>系统管理员
        /// 2=>普通用户
        /// </summary>
        public int UserType
        {
            get
            {
                if (UserName == "admin")
                    return 0;
                else if (CurrentClass.adminUser.ToList().Find(con => con.Id == Id) != null)
                    return 1;
                else
                    return 2;
            }
        }

        

    }
}