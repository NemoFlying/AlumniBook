using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.ViewModels
{
    public class LoginInput
    {
        /// <summary>
        /// 登陆用户名
        /// </summary>
        public string LogonUser { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }
    }
}