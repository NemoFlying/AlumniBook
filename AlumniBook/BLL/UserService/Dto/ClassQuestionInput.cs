using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.BLL.UserService.Dto
{
    public class ClassQuestionInput
    {
        public int id { get; set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }
    }
}