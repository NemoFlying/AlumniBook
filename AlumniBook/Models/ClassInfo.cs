using System;
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
        /// 创建人员信息
        /// </summary>
        public string  CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 班级描述
        /// </summary>
        [Required]
        public string Introduce { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public virtual List<User> Users { get; set; }

        /// <summary>
        /// 班级相册列表
        /// </summary>
        public virtual List<ClassAlbum> ClassAlbum { get; set; }

        /// <summary>
        /// 班级留言信息
        /// </summary>
        public virtual List<ClassLeavingMessage> ClassLeavingMessage { get; set; }

        /// <summary>
        /// 班级公告
        /// </summary>
        public virtual List<ClassNotice> ClassNotice { get; set; }


    }
}