using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniBook.DAL;

namespace AlumniBook.BLL.ClassQustionService
{
    public class ClassQustionServiceApplication
    {
        private IClassQuestionDAL _classQuestionService { get; set; }

        public ClassQustionServiceApplication()
        {
            _classQuestionService = new ClassQuestionDAL();
        }

        public void GetAllClassNameList()
        {
        }

    }
}