using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.ViewModels
{
    /// <summary>
    /// 班级基本信息
    /// 包括相册
    /// 包括成员
    /// 包括公告
    /// 包括留言
    /// 包括简介
    /// </summary>
    public class ClassInfoViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
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
        public UserViewModel CreateUser { get; set; }

        /// <summary>
        /// 成员列表
        /// </summary>
        public List<UserViewModel> User { get; set; }

        /// <summary>
        /// 问题&答案列表
        /// </summary>
        public List<QuestionViewModel> ClassQustion { get; set; }


    }
}