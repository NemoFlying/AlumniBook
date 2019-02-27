using AlumniBook.BLL.UserService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.BLL.ClassInfoService.Dto
{
    public class ClassInfoBaseUpdateInput
    {
        public int Id { get; set; }

        /// <summary>
        /// 班级描述
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        public string ClassNotice { get; set; }

        /// <summary>
        /// 问题&答案
        /// </summary>
        public List<ClassQuestionInput> qa { get; set; }

    }
}