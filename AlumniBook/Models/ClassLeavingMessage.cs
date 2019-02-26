using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlumniBook.Models
{
    /// <summary>
    /// 留言信息
    /// </summary>
    public class ClassLeavingMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //设置自动增长
        public int Id { get; set; }

        public ClassLeavingMessage()
        {
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 留言信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 班级信息
        /// </summary>
        public virtual ClassInfo ClassInfo { get; set; }

        /// <summary>
        /// 创建人信息
        /// </summary>
        public virtual User User { get; set; }

        
    }
}