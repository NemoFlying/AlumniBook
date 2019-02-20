using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.ViewModels
{
    public class JsonReMsg
    {
        /// <summary>
        /// 状态
        /// OK
        /// ERROR
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; }

        public Object Data { get; set; }
    }
}