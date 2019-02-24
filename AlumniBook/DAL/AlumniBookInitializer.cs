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

            //默认班级
            var classInfo = new ClassInfo()
            {
                ClassName = "启航",
                CreateUser = "admin",
                Introduce = "因为有梦，所以远方！"

            };
            var classInfo2 = new ClassInfo()
            {
                ClassName = "启航2",
                CreateUser = "admin",
                Introduce = "因为有梦，所以远方！"

            };
            context.ClassInfo.Add(classInfo);
            context.ClassInfo.Add(classInfo2);
            //admin用户注册
            var adminUser = new User()
            {
                UserName = "admin",
                Password = "9FD966C8E14B44EEF8F685DCAB3395E7",
                UserType = 1,
                Certification = "Y",
                HeadPortrait = "/img/defaultHead",
                NikeName = "admin",
                RealName = "admin",
                QqId = "895190626",
                ClassInfo = classInfo

            };
            var NemoUser = new User()
            {
                UserName = "Nemo",
                Password = "9FD966C8E14B44EEF8F685DCAB3395E7",
                UserType = 0,
                Certification = "Y",
                HeadPortrait = "/img/defaultHead",
                QqId = "895190626",
                NikeName = "Nemo",
                RealName = "Nemo",
                ClassInfo = classInfo
            };
            context.User.Add(adminUser);
            context.User.Add(NemoUser);
            //context.SaveChanges();
            
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

            //context.SaveChanges();

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

            //context.SaveChanges();

            //班级相册
            var classAlbum = new List<ClassAlbum>()
            {
                new ClassAlbum()
                {
                     ClassInfo = classInfo,
                      PhotoUrl="/img/Album/启航/photo1",
                     IsCover="Y",
                      CreateUser = adminUser.UserName,
                       CreateDate = DateTime.Now,
                     UpdateUser = adminUser.UserName,
                     UpdateDate = DateTime.Now
                },
                new ClassAlbum()
                {
                    ClassInfo = classInfo,
                    PhotoUrl="/img/Album/启航/photo2",
                    IsCover="N",
                    CreateUser = adminUser.UserName,
                       CreateDate = DateTime.Now,
                       UpdateUser = adminUser.UserName,
                     UpdateDate = DateTime.Now
                },
                new ClassAlbum()
                {
                    ClassInfo = classInfo,
                    PhotoUrl="/img/Album/启航/photo3",
                    IsCover="N",
                    CreateUser = adminUser.UserName,
                       CreateDate = DateTime.Now,
                       UpdateUser = adminUser.UserName,
                     UpdateDate = DateTime.Now
                }
            };
            classAlbum.ForEach(item => context.ClassAlbum.Add(item));

            //班级问题
            var Q1 = new List<ClassQuestion>(){
                new ClassQuestion()
                {
                    ClassInfo = classInfo,
                    Question = "Q1",
                    QuestionAnswer = "Q1"
                },
                new ClassQuestion()
                {
                    ClassInfo = classInfo,
                    Question = "Q2",
                    QuestionAnswer = "Q2"
                },
                new ClassQuestion()
                {
                    ClassInfo = classInfo,
                    Question = "Q3",
                    QuestionAnswer = "Q3"
                },
                new ClassQuestion()
                {
                    ClassInfo = classInfo2,
                    Question = "Q4",
                    QuestionAnswer = "Q1"
                },
                new ClassQuestion()
                {
                    ClassInfo = classInfo2,
                    Question = "Q5",
                    QuestionAnswer = "Q5"
                },
                new ClassQuestion()
                {
                    ClassInfo = classInfo2,
                    Question = "Q6",
                    QuestionAnswer = "Q6"
                },
            };

            Q1.ForEach(item => context.ClassQuestion.Add(item));
            context.SaveChanges();
        }
    }
}