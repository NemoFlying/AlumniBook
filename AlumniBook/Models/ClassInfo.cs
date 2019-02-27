﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlumniBook.Models
{    
    public class ClassInfo
    {

        public ClassInfo()
        {
            this.CreateDate = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //设置自动增长
        public int Id { get; set; }

        /// <summary>
        /// 班级名称
        /// 不能重复
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ClassName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 班级描述
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// 创建人员信息
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// 管理员
        /// </summary>
        public virtual ICollection<User> adminUser { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public virtual ICollection<User> User { get; set; }

        /// <summary>
        /// 班级相册列表
        /// </summary>
        public virtual ICollection<ClassAlbum> ClassAlbum { get; set; }

        /// <summary>
        /// 班级留言信息
        /// </summary>
        public virtual ICollection<ClassLeavingMessage> ClassLeavingMessage { get; set; }

        /// <summary>
        /// 班级公告
        /// </summary>
        public virtual ICollection<ClassNotice> ClassNotice { get; set; }

        /// <summary>
        /// 申请班级Q&A
        /// </summary>
        public virtual ICollection<ClassQuestion> ClassQustion { get; set; }
    }
}