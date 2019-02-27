using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.BLL.SensitiveService
{
    public class SensitiveResult
    {
        public ResultInfo result { get; set; }
    }
    public class ResultInfo
    {
        public int spam { get; set; }
    }
}