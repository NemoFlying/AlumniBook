using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniBook.BLL.UserService.Dto;
using AlumniBook.DAL;
using AlumniBook.ViewModels;

namespace AlumniBook.Controllers
{
    public class AlumniBookBaseController : Controller
    {
        protected AlumniBookContext dbcontext = new AlumniBookContext();

        /// <summary>
        /// 全局用户信息
        /// </summary>
        protected UserInfoOutput GuserInfo => (UserInfoOutput)HttpContext.Session["userinfo"];


    }
}