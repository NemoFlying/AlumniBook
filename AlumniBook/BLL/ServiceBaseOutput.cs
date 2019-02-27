using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.BLL
{
    public class ServiceBaseOutput
    {
        /// <summary>
        /// 状态
        /// OK
        /// ERROR
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; }

        public Object Data { get; set; }
    }
}