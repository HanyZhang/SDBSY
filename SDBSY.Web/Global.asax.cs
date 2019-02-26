using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using SDBSY.IService;
using SDBSY.Common;
using SDBSY.Service;
using SDBSY.Web.App_Start;

namespace SDBSY.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            var builder = new ContainerBuilder();
            builder.RegisterType<StudentService>().As<IStudentService>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                .PropertiesAutowired();//把当前程序集中的Controller都注册        
                                        //不要忘了.PropertiesAutowired()
                                       // 获取所有相关类库的程序集
            Assembly[] assemblies = new Assembly[] { Assembly.Load("SDBSY.Service") };
            builder.RegisterAssemblyTypes(assemblies)
            .Where(type => !type.IsAbstract
                    && typeof(IServiceSupport).IsAssignableFrom(type))
                    .AsImplementedInterfaces().InstancePerRequest()
                .PropertiesAutowired();
            //Assign：赋值
            //type1.IsAssignableFrom(type2);type1类型的变量是否可以指向type2类型的对象
            //换一种说法：type2是否实现了type1接口/type2是否继承自type1
            //typeof(IServiceSupport).IsAssignableFrom(type)只注册实现了IServiceSupport的类
            //避免其他无关的类注册到AutoFac中
            builder.RegisterFilterProvider();
            var container = builder.Build();
            //注册系统级别的DependencyResolver，这样当MVC框架创建Controller等对象的时候都是管Autofac要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//!!!

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalFilters.Filters.Add(new SDBSYExceptionFilter());//异常记录
            GlobalFilters.Filters.Add(new SDBSYAuthorizeFilter());//权限检查
            GlobalFilters.Filters.Add(new JsonNetActionFilter());//json序列化
        }
    }
}
