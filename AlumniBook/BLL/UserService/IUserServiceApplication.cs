using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniBook.BLL.UserService.Dto;
using AlumniBook.Models;

namespace AlumniBook.BLL.UserService
{
    public interface IUserServiceApplication
    {

        /// <summary>
        /// 登录认证
        /// </summary>
        /// <param name="userName">登录账号</param>
        /// <param name="password">密码</param>
        /// <returns>
        /// 返回登录信息
        /// </returns>
        ResultBaseOutput LogonAuthen(string userName, string password);

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <returns></returns>
        ServiceBaseOutput RegistUser(RegistUserInput newUserInfo);


        /// <summary>
        /// 根据用户获得班级信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ClassInfo GetClassInfoByUid(int userId);

        /// <summary>
        /// 根据班级获取班级所有学生
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        List<UserInfoOutput> GetAllClassUser(int classId);

        /// <summary>
        /// 根据用户ID删除用户
        /// </summary>
        /// <param name="userId"></param>
        ResultBaseOutput DeleteUserById(int userId);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="updUser"></param>
        /// <returns></returns>
        ResultBaseOutput UpdateUser(UserInfoUpdateInput updUser);
    }
}