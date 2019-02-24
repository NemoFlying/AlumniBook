using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.ViewModels
{
    public class ClassInfoViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 班级名称
        /// 不能重复
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 创建人员信息
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 班级描述
        /// </summary>
        public string Introduce { get; set; }
    }
}