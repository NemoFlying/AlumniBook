using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using AlumniBook.BLL.UserService.Dto;
using AlumniBook.DAL;
using AlumniBook.Models;
using AutoMapper;

namespace AlumniBook.BLL.UserService
{
    public class UserServiceApplication:IUserServiceApplication
    {

        private readonly IUserDAL _userDAL;
        private readonly IClassInfoDAL _classInfoDAL;

        /// <summary>
        /// 构造函数
        /// 后续修改为依赖注入的方式
        /// </summary>
        public UserServiceApplication()
        {
            _userDAL = new UserDAL();
            _classInfoDAL = new ClassInfoDAL();
        }

        /// <summary>
        /// 登录认证
        /// </summary>
        /// <param name="userName">登录账号</param>
        /// <param name="password">密码</param>
        /// <returns>
        /// 返回登录信息
        /// </returns>
        public LogonResultOutput LogonAuthen(string userName,string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var pwd = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(userName + password)));
            pwd = pwd.Replace("-", "");
            var user = _userDAL.GetModels(con => con.UserName == userName).FirstOrDefault();

            var result = new LogonResultOutput();
            result.result = false;
            if (user is null)
            {
                //表示没有注册
                result.Msg = "用户尚未注册";
            }
            else if (user.Password != pwd)
            {
                //密码错误
                result.Msg = "用户密码错误";
            }
            else
            {
                //认证通过
                result.result = true;
                result.Data = Mapper.Map<UserInfoOutput>(user);
            }
            return result;


        }

        /// <summary>
        /// 根据用户名称获取基本信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserInfoOutput GetUserInfoByName(string userName)
        {
            return Mapper.Map<UserInfoOutput>(
                _userDAL.GetModels(con => con.UserName == userName)
                .FirstOrDefault()
                );
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <returns></returns>
        public RegistResultOutput RegistUser(RegistUserInput newUserInfo)
        {
            var result = new RegistResultOutput();
            //基本输入信息验证判断
            //判断用户是否存在
            var user = _userDAL.GetModels(con=>con.UserName==newUserInfo.UserName)
                .FirstOrDefault();
            if(user != null)
            {
                result.result = false;
                result.Msg = "用户名已经存在！";
            }
            else
            {
                //问题答案验证
                var classInfo = _classInfoDAL.GetModels(con => con.Id == newUserInfo.ClassId).FirstOrDefault();
                var compareResult = true;
                classInfo.ClassQustion.ForEach(
                    item => {
                        if (!newUserInfo.question.Contains(item.QuestionAnswer))
                        {
                            compareResult = false;
                        }
                    }
                    );
                if(!compareResult)
                {
                    result.result = false;
                    result.Msg = "问题回答错误！";
                }
                else
                {
                    user = Mapper.Map<User>(newUserInfo);
                    //密码加密
                    var md5 = new MD5CryptoServiceProvider();
                    user.Password = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(user.UserName + user.Password))).Replace("-", "");
                    user.ClassInfo = classInfo;
                    _userDAL.Add(user);
                    try
                    {
                        _userDAL.SaveChanges();
                        result.result = true;
                    }
                    catch (Exception ex)
                    {
                        result.result = false;
                        result.Data = ex;
                    }
                }
                
            }
            
           
            return result;
        }

        
        /// <summary>
        /// 根据用户获得班级信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ClassInfo GetClassInfoByUid(int userId)
        {
            return _userDAL.GetModels(con => con.Id == userId).FirstOrDefault().ClassInfo;

        }

        //public List<UserInfoOutput> GetAllUserInfo()
        //{

        //}

    }
}