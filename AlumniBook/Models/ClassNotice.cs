using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlumniBook.Models
{
    public class ClassNotice
    {
        public ClassNotice()
        {
            CreateDate = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //设置自动增长
        public int Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 公告信息
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// 留言班级
        /// </summary>
        public virtual ClassInfo ClassInfo { get; set; }

        /// <summary>
        /// 创建人信息
        /// </summary>
        public virtual User CreateUser { get; set; }


    }
}