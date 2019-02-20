using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AlumniBook.Models;

namespace AlumniBook.DAL
{
    public class AlumniBookInitializer:DropCreateDatabaseIfModelChanges<AlumniBookContext>
    {
        protected override void Seed(AlumniBookContext context)
        {
            //admin用户注册
            var adminUser = new RegisteredUser()
            {
                LogonUser = "admin",
                Password = "9FD966C8E14B44EEF8F685DCAB3395E7",
                UserType = 1,
                Certification = "Y",
                HeadPortrait = "/img/defaultHead",
                NikeName = "admin",
                RealName = "admin"

            };
            var NemoUser = new RegisteredUser()
            {
                LogonUser = "Nemo",
                Password = "9FD966C8E14B44EEF8F685DCAB3395E7",
                UserType = 0,
                Certification = "Y",
                HeadPortrait = "/img/defaultHead",
                NikeName = "Nemo",
                RealName = "Nemo"
            };
            context.RegisteredUser.Add(adminUser);
            context.RegisteredUser.Add(NemoUser);
            context.SaveChanges();
            var classInfo = new ClassInfo()
            {
                ClassName = "启航",
                CreateUser = adminUser,
                Introduce = "因为有梦，所以远方！"

            };
            context.ClassInfo.Add(classInfo);

            var userClass = new List<UserClass>() {
                new UserClass()
                {
                     ClassInfo = classInfo,
                      UserInfo = adminUser
                },
                new UserClass()
                {
                    ClassInfo = classInfo,
                      UserInfo = NemoUser
                }
            };
            userClass.ForEach(item => context.UserClass.Add(item));
            context.SaveChanges();

            //留言信息
            var classLeavingMessage = new List<ClassLeavingMessage>() {
                 new ClassLeavingMessage(){
                      ClassInfo =classInfo,
                       CreateUser = adminUser,
                        CreateDate = DateTime.Now,
                         Msg = "admin 留言"
                 },
                 new ClassLeavingMessage(){
                     ClassInfo =classInfo,
                       CreateUser = adminUser,
                        CreateDate = DateTime.Now,
                         Msg = "admin 留言2"
                 },
                 new ClassLeavingMessage(){
                     ClassInfo =classInfo,
                       CreateUser = NemoUser,
                        CreateDate = DateTime.Now,
                         Msg = "Nemo 留言2"
                 },

            };
            classLeavingMessage.ForEach(item => context.ClassLeavingMessage.Add(item));

            context.SaveChanges();

            //班级公告

            var classNotice = new List<ClassNotice>(){
                new ClassNotice()
                {
                    ClassInfo = classInfo,
                    CreateDate = DateTime.Now,
                    CreateUser = adminUser,
                    Notice = "公告1"
                },
                new ClassNotice()
                {
                    ClassInfo = classInfo,
                    CreateDate = DateTime.Now,
                    CreateUser = adminUser,
                    Notice = "公告2"
                },
                new ClassNotice()
                {
                    ClassInfo = classInfo,
                    CreateDate = DateTime.Now,
                    CreateUser = adminUser,
                    Notice = "公告3"
                }
            };

            classNotice.ForEach(item => context.ClassNotice.Add(item));

            context.SaveChanges();

            //班级相册
            var classAlbum = new List<ClassAlbum>()
            {
                new ClassAlbum()
                {
                     ClassInfo = classInfo,
                      PhotoUrl="/img/Album/启航/photo1",
                     IsCover="Y",
                      CreateUser = adminUser,
                       CreateDate = DateTime.Now,
                     UpdateUser = adminUser,
                     UpdateDate = DateTime.Now
                },
                new ClassAlbum()
                {
                    ClassInfo = classInfo,
                    PhotoUrl="/img/Album/启航/photo2",
                    IsCover="N",
                    CreateUser = adminUser,
                       CreateDate = DateTime.Now,
                       UpdateUser = adminUser,
                     UpdateDate = DateTime.Now
                },
                new ClassAlbum()
                {
                    ClassInfo = classInfo,
                    PhotoUrl="/img/Album/启航/photo3",
                    IsCover="N",
                    CreateUser = adminUser,
                       CreateDate = DateTime.Now,
                       UpdateUser = adminUser,
                     UpdateDate = DateTime.Now
                }
            };
            classAlbum.ForEach(item => context.ClassAlbum.Add(item));
            
            context.SaveChanges();
        }
    }
}