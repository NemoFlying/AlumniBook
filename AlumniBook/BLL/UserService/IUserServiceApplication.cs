using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniBook.BLL.UserService.Dto;

namespace AlumniBook.BLL.UserService
{
    public interface IUserServiceApplication
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <returns></returns>
        RegistResultOutput RegistUser(RegistUserInput newUserInfo);
    }
}