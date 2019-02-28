using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        //https://stackoverflow.com/questions/33029127/go-to-definition-cannot-navigate-to-the-symbol-under-the-caret
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
        /// 【登录认证】
        /// </summary>
        /// <param name="userName">登录账号</param>
        /// <param name="password">密码</param>
        /// <returns>
        /// 返回登录信息
        /// </returns>
        public ResultBaseOutput LogonAuthen(string userName,string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var pwd = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(userName + password)));
            pwd = pwd.Replace("-", "");
            var user = _userDAL.GetModels(con => con.UserName == userName).Include("UserClass").FirstOrDefault();

            var result = new ResultBaseOutput();
            result.Status = false;
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
                result.Status = true;
                result.Data = Mapper.Map<UserInfoOutput>(user);
            }
            return result;
        }

        /// <summary>
        /// 根据用户名称获取基本信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserInfoOutput GetUserInfoById(int userId)
        {
            return Mapper.Map<UserInfoOutput>(
                _userDAL.GetModels(con => con.Id == userId)
                .FirstOrDefault()
                );
        }

        /// <summary>
        /// 【注册用户】
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <returns></returns>
        public ServiceBaseOutput RegistUser(RegistUserInput newUserInfo)
        {
            var result = new ServiceBaseOutput();
            //基本输入信息验证判断
            //判断用户是否存在
            var user = _userDAL.GetModels(con=>con.UserName==newUserInfo.UserName)
                .FirstOrDefault();
            var md5 = new MD5CryptoServiceProvider();
            if (user != null)
            {
                result.Status = false;
                result.Msg = "用户名已经存在！";
                return result;
            }
            if(newUserInfo.UserType==1) //管理员用户
            {
                //检查创建班级名称是否存在
                var classInfo = _classInfoDAL.GetModels(con => con.ClassName == newUserInfo.ClassName).FirstOrDefault();
                if(classInfo != null)
                {
                    result.Status = false;
                    result.Msg = "班级名称已经存在！";
                    return result;
                }
                //创建用户
                user = Mapper.Map<User>(newUserInfo);
                user.Password = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(user.UserName + user.Password))).Replace("-", "");
                //创建问题&答案
                //var qa = new List<ClassQuestion>();
                //newUserInfo.QuestionConfig.ForEach(item =>
                //    qa.Add(new ClassQuestion() { Question = item.Question, Answer = item.Answer })
                //);
                //创建班级
                var newClassInfo = new ClassInfo()
                {
                    ClassName = newUserInfo.ClassName,
                    CreateUser = user,
                    User = new List<User>() { user },
                    adminUser = new List<User>() { user }
                    //Introduce = newUserInfo.Introduce,
                    //adminUser = new List<User>() { user },
                    //ClassQustion = qa
                };
                _classInfoDAL.Add(newClassInfo);
                try
                {
                    _classInfoDAL.SaveChanges();
                    result.Status = true;
                }
                catch (Exception ex)
                {
                    result.Status = false;
                    result.Msg = ex.ToString();
                    result.Data = ex;
                }
            }
            else if(newUserInfo.UserType == 2) //普通用户
            {
                //问题答案验证
                var classInfo = _classInfoDAL.GetModels(con => con.Id == newUserInfo.ClassId).FirstOrDefault();

                var anSwerResult = true;
                classInfo.ClassQustion.ToList().ForEach(
                    item => {
                        if (newUserInfo.QuestionConfig.Find(con => con.Question == item.Question && con.Answer==item.Answer) is null)
                        {
                            anSwerResult = false;
                        }
                    }
                    );
                if(!anSwerResult)
                {
                    result.Status = false;
                    result.Msg = "班级问题验证失败！";
                }
                else
                {
                    user = Mapper.Map<User>(newUserInfo);
                    //密码加密
                    user.Password = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(user.UserName + user.Password))).Replace("-", "");
                    classInfo.User.Add(user);
                    _classInfoDAL.Update(classInfo);
                    try
                    {
                        _classInfoDAL.SaveChanges();
                        result.Status = true;
                    }
                    catch (Exception ex)
                    {
                        result.Status = false;
                        result.Msg = ex.ToString();
                        result.Data = ex;
                    }
                }
                
            }
            else
            {
                result.Status = false;
                result.Msg = "用户类型不确定";
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
            return _userDAL.GetModels(con => con.Id == userId).FirstOrDefault().UserClass.FirstOrDefault();

        }
        
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public List<UserInfoOutput> GetAllUser()
        {
            return Mapper.Map<List<UserInfoOutput>>(
                _userDAL.GetModels(con => 1==1)
                .FirstOrDefault()
                );

        }


        /// <summary>
        /// 根据班级获取班级所有学生
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<UserInfoOutput> GetAllClassUser(int classId)
        {
            return Mapper.Map<List<UserInfoOutput>>(_classInfoDAL.GetModels(con => con.Id == classId)
                .FirstOrDefault().User.ToList());
        }

        /// <summary>
        /// 根据用户ID删除用户
        /// </summary>
        /// <param name="userId"></param>
        public ResultBaseOutput DeleteUserById(int userId)
        {
            var result = new ResultBaseOutput();
            var user = _userDAL.GetModels(con => con.Id == userId).FirstOrDefault();
            _userDAL.Delete(user);
            try
            {
                _userDAL.SaveChanges();
                result.Status = true;
            }
            catch(Exception ex)
            {
                result.Status = false;
                result.Msg = "删除失败";
                result.Data = ex;
            }
            return result;

        }

        public ResultBaseOutput SetUserAdmin(int userId)
        {
            var result = new ResultBaseOutput();
            ////var user = _userDAL.GetModels(con => con.Id == userId).FirstOrDefault();
            ////user.UserType = 1;
            ////_userDAL.Update(user);
            ////try
            ////{
            ////    _userDAL.SaveChanges();
            ////    Status.Status = true;
            ////}
            ////catch
            ////{
            ////    Status.Status = false;
            ////    Status.Msg = "数据库执行失败";
            ////}
            return result;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="updUser"></param>
        /// <returns></returns>
        public ResultBaseOutput UpdateUser(UserInfoUpdateInput updUser)
        {
            var result = new ResultBaseOutput();

            
            var user = _userDAL.GetModels(con => con.Id == updUser.Id).FirstOrDefault();
            updUser.UserName = user.UserName;
            if (string.IsNullOrEmpty(updUser.Password))
            {
                updUser.Password = user.Password;
            }
            else
            {
                var md5 = new MD5CryptoServiceProvider();
                updUser.Password = BitConverter
                    .ToString(md5.ComputeHash(Encoding.Default.GetBytes(updUser.UserName + updUser.Password)))
                    .Replace("-", "");
            }
            Mapper.Map(updUser, user);
            
            try
            {
                _userDAL.Update(user);
                _userDAL.SaveChanges();
                result.Status = true;
                result.Data = user;
            }
            catch(Exception ex)
            {
                result.Status = false;
                result.Msg = "删除失败";
                result.Data = ex;
            }
            return result;
        }

        /// <summary>
        /// 根据关键字获取班级所有学生
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public List<UserInfoOutput> GetAllClassUserByKeyWord(string keyWords)
        {
            var users = new List<UserInfoOutput>();
            users.AddRange(Mapper.Map<List<UserInfoOutput>>(
                _userDAL.GetModels(con => con.UserName.Contains(keyWords)
                || con.Addr.Contains(keyWords)
                || con.RealName.Contains(keyWords)
                || con.QqId.Contains(keyWords)
                ).ToList()));
            //users.AddRange(Mapper.Map<List<UserInfoOutput>>(
            //    _userDAL.GetModels(con => con.Addr.Contains(keyWords)).ToList()));
            //users.AddRange(Mapper.Map<List<UserInfoOutput>>(
            //    _userDAL.GetModels(con => con.RealName.Contains(keyWords)).ToList()));
            //users.AddRange(Mapper.Map<List<UserInfoOutput>>(
            //    _userDAL.GetModels(con => con.QqId.Contains(keyWords)).ToList()));

            return users;
        }
    }
}