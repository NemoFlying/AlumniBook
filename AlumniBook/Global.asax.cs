﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AlumniBook.BLL.ClassInfoService.Dto;
using AlumniBook.BLL.UserService.Dto;
using AlumniBook.Models;
using AlumniBook.ViewModels;
using AutoMapper;

namespace AlumniBook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static class MappingConfig
        {
            public static void RegisterMaps()
            {
                Mapper.Initialize(config =>
                {
                    //用户输出
                    config.CreateMap<User, UserInfoOutput>();
                    config.CreateMap<User, UserViewModel>();
                    config.CreateMap<UserInfoOutput, UserViewModel>();
                    
                    config.CreateMap<RegistUserInput, User>();
                    config.CreateMap<ClassQuestion, QuestionViewModel>();

                    //留言信息
                    config.CreateMap<ClassLeavingMessage, LeavingMessageViewModel>()
                    .ForMember(dest => dest.createUserName, opt => opt.MapFrom(src => src.User.UserName))
                    .ForMember(dest => dest.QqId, opt => opt.MapFrom(src => src.User.QqId))
                    .ForMember(dest => dest.HeadPortrait, opt => opt.MapFrom(src => src.User.HeadPortrait));
                    
                    config.CreateMap<ClassNotice, NoticeViewModel>()
                    .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.ClassInfo.ClassName));
                    config.CreateMap<NoticeViewModel, NoticeInput>();
                    config.CreateMap<NoticeInput, ClassNotice>();
                    config.CreateMap<ClassAlbum, AlbumViewModel>();

                    config.CreateMap<ClassQuestionInput, ClassQuestion>();
                    config.CreateMap<ClassInfo, ClassInfoViewModel>();

                    ////店铺转换
                    //config.CreateMap<Shop, ShopOutPutViewModel>()
                    //.ForMember(dest => dest.KeeperName, opt => opt.MapFrom(src => src.Keeper.LogonUser));

                    ////产品转换
                    //config.CreateMap<Goods, GoodsOutputViewModel>()
                    //.ForMember(dest => dest.ShopName, opt => opt.MapFrom(src => src.Shop.Name));

                    ////订单
                    //config.CreateMap<Order, OrderOutPutViewModel>()
                    //.ForMember(dest => dest.OrderUser, opt => opt.MapFrom(src => src.OrderUser.LogonUser))
                    //.ForMember(dest => dest.GoodsCnt, opt => opt.MapFrom(src => src.Goods.Count()));

                    ////角色
                    //config.CreateMap<Role, RoleOutputViewModel>();
                });
            }
        }




        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MappingConfig.RegisterMaps();
        }
    }
}
