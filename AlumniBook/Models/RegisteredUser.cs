using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlumniBook.Models
{
    public class RegisteredUser
    {
        public RegisteredUser()
        {
            this.UserType = 0;
            this.Certification = "N";

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //设置自动增长
        public int Id { get; set; }
        /// <summary>
        /// 登陆用户名
        /// 不能重复
        /// </summary>
        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        public string LogonUser { get; set; }

        /// <summary>
        /// 登陆密码【MD5加密】
        /// </summary>
        [MaxLength(150)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 用户类型
        /// 0:普通用户
        /// 1.管理员
        /// </summary>
        [Required]
        public int UserType { get; set; }

        /// <summary>
        /// 是否实名认证
        /// Y:表示已经认证
        /// N:表示未认证【默认值】
        /// </summary>
        [Required]
        [MaxLength(1)]
        public string Certification { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50)]
        public string NikeName { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        [MaxLength(50)]
        public string HeadPortrait { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(50)]
        public string RealName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [MaxLength(1)]
        public string Sex { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(50)]
        public string Phone { get; set; }


    }
}