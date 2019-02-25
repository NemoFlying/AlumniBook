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
                Introduce = "48个快乐的孩子，怀着对梦的执着；对美好青春的渴望；对未来无限的憧憬；踏进了六年五班。这是一个团结上进的班集体；一个温馨和谐的大家庭，一个充满爱的乐园。一个团结的群体，一个充满活力的群体，一个不甘落后的群体，一个饱含热情的群体 ；它不会因为矛盾而分散，不会因为压力而沉寂，不会因为失败而丧气，不会因为竞争而冷漠。我们，是一个整体，我们，是不可分割的一织血脉。纵然有过羞涩，纵然有过隔阂，但一切的不愉快都随风飘逝，所有的不和都随波消散，友好，和睦，亲密无间，成为了永恒的旋律。心灵与心灵相交融，友爱和友爱相重叠，平平淡淡的生活中，所涂抹出来的，是一幅没有灰色，没有黑色，没有夜的暗，没有冬之寒的油画。这就是五班。"

            };
            var classInfo2 = new ClassInfo()
            {
                ClassName = "启航2",
                CreateUser = "admin",
                Introduce = "因为有梦，所以远方！"

            };
            context.ClassInfo.Add(classInfo);
            context.ClassInfo.Add(classInfo2);

            //添加用户班级 1班
            var user = new List<User>()
            {
                new User()
            {
                UserName = "admin",
                Password = "F6FDFFE48C908DEB0F4C3BD36C032E72",
                UserType = 1,
                Certification = "Y",
                HeadPortrait = "/img/defaultHead",
                NikeName = "admin",
                RealName = "admin",
                QqId = "895190626",
                ClassInfo = classInfo

            },
                new User()
            {
                UserName = "Jerry",
                Password = "9FD966C8E14B44EEF8F685DCAB3395E7",
                UserType = 0,
                Certification = "Y",
                HeadPortrait = "/img/defaultHead",
                QqId = "240269961",
                NikeName = "Nemo",
                RealName = "Nemo",
                ClassInfo = classInfo
            },
                new User()
            {
                UserName = "miao",
                Password = "9FD966C8E14B44EEF8F685DCAB3395E7",
                UserType = 0,
                Certification = "Y",
                HeadPortrait = "/img/defaultHead",
                QqId = "544088599",
                NikeName = "Nemo",
                RealName = "Nemo",
                ClassInfo = classInfo
            }
            };
            //admin用户注册

            user.ForEach(item => context.User.Add(item));
            //context.SaveChanges();

            //留言信息
            var classLeavingMessage = new List<ClassLeavingMessage>() {
                 new ClassLeavingMessage(){
                      ClassInfo =classInfo,
                       CreateUser = user[0],
                        CreateDate = DateTime.Now,
                         Msg = "Nemo留言，好好学习天天向上!"

                 },
                 new ClassLeavingMessage(){
                     ClassInfo =classInfo,
                       CreateUser = user[1],
                        CreateDate = DateTime.Now,
                         Msg = "Jerry留言，好好学习天天向上!"
                 },
                 new ClassLeavingMessage(){
                     ClassInfo =classInfo,
                       CreateUser = user[2],
                        CreateDate = DateTime.Now,
                         Msg = "miao留言，好好学习天天向上!"
                 },

            };
            classLeavingMessage.ForEach(item => context.ClassLeavingMessage.Add(item));

            //班级公告

            var classNotice = new List<ClassNotice>(){
                new ClassNotice()
                {
                    ClassInfo = classInfo,
                    CreateDate = DateTime.Now,
                    CreateUser = user[0],
                    Notice = "无论你今天要面对什么，既然走到了这一步，就坚持下去，给自己一些肯定，你比自己想象中要坚强。你再优秀也会有人对你不屑一顾，你再不堪也会有人把你视若生命"
                },
                new ClassNotice()
                {
                    ClassInfo = classInfo,
                    CreateDate = DateTime.Now,
                    CreateUser = user[0],
                    Notice = "活得糊涂的，容易幸福；活得清醒的，容易烦恼。这是因为，清醒的人看得太真切，一较真，生活中便烦恼遍地；而糊涂的人，计较得少，虽然活得简单粗糙，却因此觅得了人生的大滋味。"
                },
                new ClassNotice()
                {
                    ClassInfo = classInfo,
                    CreateDate = DateTime.Now,
                    CreateUser = user[0],
                    Notice = "当人生遇到坎坷，历经磨难时，我们应该不断为自己鼓掌，鼓劲、鼓励。不为困苦所屈服，不为艰险而低头，不为磨难所吓倒。生活的理想是为了理想的生活，只有为自己鼓掌，人生之路会越走越宽广，人生之路会越走越坦荡。"
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
                      PhotoUrl="../assets/Images/Album/Class1/20190225220359.png",
                      IsCover="Y",
                      CreateUser = user[0].UserName,
                      CreateDate = DateTime.Now,
                     UpdateUser = user[0].UserName,
                     UpdateDate = DateTime.Now
                },
                new ClassAlbum()
                {
                    ClassInfo = classInfo,
                    PhotoUrl="../assets/Images/Album/Class1/20190225220446.jpg",
                    IsCover="N",
                    CreateUser = user[0].UserName,
                       CreateDate = DateTime.Now,
                       UpdateUser = user[0].UserName,
                     UpdateDate = DateTime.Now
                },
                new ClassAlbum()
                {
                    ClassInfo = classInfo,
                    PhotoUrl="../assets/Images/Album/Class1/20190225220454.jpg",
                    IsCover="N",
                    CreateUser = user[0].UserName,
                       CreateDate = DateTime.Now,
                       UpdateUser = user[0].UserName,
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