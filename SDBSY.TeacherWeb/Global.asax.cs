using Autofac;
using Autofac.Integration.Mvc;
using SDBSY.IService;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using SDBSY.TeacherWeb.App_Start;
using SDBSY.Common;

namespace SDBSY.TeacherWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            var builder = new ContainerBuilder();
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
            var container = builder.Build();
            //注册系统级别的DependencyResolver，这样当MVC框架创建Controller等对象的时候都是管Autofac要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); //!!!

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalFilters.Filters.Add(new TeacherWebExceptionFilter());//异常检查
            GlobalFilters.Filters.Add(new JsonNetActionFilter());//json序列化
            GlobalFilters.Filters.Add(new TeacherWebAuthorizeFilter());//登陆检测
        }
    }
}
