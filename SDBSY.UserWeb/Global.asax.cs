using Autofac;
using Autofac.Integration.Mvc;
using log4net;
using SDBSY.Common;
using SDBSY.IService;
using SDBSY.UserWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SDBSY.Service;

namespace SDBSY.UserWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static ILog log = LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();



            var builder = new ContainerBuilder();
            /*
            builder.RegisterType<StudentService>().As<IStudentService>();
            builder.RegisterType<AdminLogService>().As<IAdminLogService>();
            builder.RegisterType<AdminUserService>().As<IAdminUserService>();
            builder.RegisterType<CountryService>().As<ICountryService>();
            builder.RegisterType<DataDictionaryService>().As<IDataDictionaryService>();
            builder.RegisterType<FoodService>().As<IFoodService>();
            builder.RegisterType<GuardianService>().As<IGuardianService>();
            builder.RegisterType<MenuService>().As<IMenuService>();
            builder.RegisterType<NationService>().As<INationService>();
            builder.RegisterType<ParentService>().As<IParentService>();
            builder.RegisterType<PermissionService>().As<IPermissionService>();
            builder.RegisterType<PlaceService>().As<IPlaceService>();
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<SystemSettingService>().As<ISystemSettingService>();
            builder.RegisterType<TeacherService>().As<ITeacherService>();
            builder.RegisterType<UserService>().As<IUserService>();*/

            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                .PropertiesAutowired(); //把当前程序集中的Controller都注册        
                                        //不要忘了.PropertiesAutowired(),属性注入必须的，下面的也是
                                        // 获取所有相关类库的程序集
            Assembly[] assemblies = new Assembly[] { Assembly.Load("SDBSY.Service") };
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => !type.IsAbstract
                               && typeof(IServiceSupport).IsAssignableFrom(type))
                .AsImplementedInterfaces()
                .InstancePerRequest()//单个 Web / HTTP / API 请求的范围内的组件的共享一个实例。仅可用于支持每个请求的依赖关系的整合（如MVC，Web API，Web Forms等）。
                .PropertiesAutowired();
            //Assign：赋值
            //type1.IsAssignableFrom(type2);type1类型的变量是否可以指向type2类型的对象
            //换一种说法：type2是否实现了type1接口/type2是否继承自type1
            //typeof(IServiceSupport).IsAssignableFrom(type)只注册实现了IServiceSupport的类
            //避免其他无关的类注册到AutoFac中

            var container = builder.Build();
            //注册系统级别的DependencyResolver，这样当MVC框架创建Controller等对象的时候都是管Autofac要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); //!!!

            //log.DebugFormat("到底什么错误呢,个数");
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalFilters.Filters.Add(new SDBSYExceptionFilter());//异常记录
            GlobalFilters.Filters.Add(new SDBSYAuthorizeFilter());//权限检查
            GlobalFilters.Filters.Add(new JsonNetActionFilter());//json序列化
        }
    }
}
