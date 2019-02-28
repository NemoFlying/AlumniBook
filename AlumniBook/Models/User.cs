using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlumniBook.Models
{
    public class User
    {
        public User()
        {
            Certification = "N";
            RegistDate = DateTime.Now;
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
        public string UserName { get; set; }

        /// <summary>
        /// 登陆密码【MD5加密】
        /// </summary>
        [MaxLength(150)]
        [Required]
        public string Password { get; set; }

        ///// <summary>
        ///// 用户类型
        ///// 0:普通用户
        ///// 1.管理员
        ///// </summary>
        //[Required]
        //public int UserType { get; set; }

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
        /// QQ
        /// </summary>
        public string QqId { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        [MaxLength(150)]
        public string HeadPortrait { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(50)]
        public string RealName { get; set; }

        /// <summary>
        /// M/F
        /// </summary>
        [MaxLength(1)]
        public string Sex { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(50)]
        public string Phone { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        [MaxLength(500)]
        public string Addr { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [MaxLength(500)]
        public string BirthDay { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegistDate { get; set; }

        /// <summary>
        /// 班级信息
        /// </summary>
        public virtual ICollection<ClassInfo> UserClass { get; set; }

        public virtual ICollection<ClassInfo> AdminClass { get; set; }

        /// <summary>
        /// 创建班级信息【当前只能创建一个】
        /// </summary>
        public virtual ICollection<ClassInfo> CreateClass { get; set; }

        public virtual ICollection<ClassLeavingMessage> ClassLeavingMessage { get; set; }
    }
}