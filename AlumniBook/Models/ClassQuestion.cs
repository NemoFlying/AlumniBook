using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlumniBook.Models
{
    public class ClassQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //设置自动增长
        public int Id { get; set; }

        public string Question { get; set; }

        public string QuestionAnswer { get; set; }

        /// <summary>
        /// 班级信息
        /// </summary>
        public virtual ClassInfo ClassInfo { get; set; }
    }
}