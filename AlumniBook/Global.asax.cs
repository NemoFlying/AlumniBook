using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
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
                    config.CreateMap<UserInfoOutput, UserInfo>();
                    config.CreateMap<RegistUserInput, User>();
                    config.CreateMap<ClassQuestion, ClassQuestionViewModel>();
                    //config.CreateMap<ClassQustion, ClassQuestionViewModel>();
                    //config.CreateMap<GoodsInfoInput, Goods>();

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
