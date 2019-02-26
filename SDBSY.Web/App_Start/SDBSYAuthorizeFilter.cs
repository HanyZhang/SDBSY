using SDBSY.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDBSY.IService;

namespace SDBSY.Web.App_Start
{
    public class SDBSYAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //获取登录设备信息
            string loginUrl = "/Management/Signin";
            //if(CommonHelper.IsMobileDevice())
            //{
            //    loginUrl = "/Mobile/Login";
            //}

            //获得当前要执行的Action上标注的CheckPermissionAttribute实例对象
            CheckPermissionAttribute[] permAtts = (CheckPermissionAttribute[])filterContext.ActionDescriptor
                .GetCustomAttributes(typeof(CheckPermissionAttribute), false);
            if (permAtts.Length <= 0)//没有标注任何的CheckPermissionAttribute，因此也就不需要检查是否登录
                                     //“无欲无求”
            {
                return;//登录等这些不要求有用户登录的功能
            }
            //得到当前登录用户的id
            long? userId = (long?)filterContext.HttpContext.Session["LoginUserId"];

            if (userId == null)//连登录都没有，就不能访问
            {

                //根据不同的请求，给予不同的返回格式。确保ajax请求，浏览器端也能收到json格式
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    AjaxResult ajaxResult = new AjaxResult();
                    ajaxResult.Status = "redirect";
                    ajaxResult.Data = loginUrl;
                    ajaxResult.ErrorMsg = "登录信息超时";
                    filterContext.Result = new JsonNetResult { Data = ajaxResult };
                }
                else
                {
                    filterContext.Result = new RedirectResult("~"+loginUrl);

                }

                return;
            }
            //由于ZSZAuthorizeFilter不是被autofac创建，因此不会自动进行属性的注入
            //需要手动获取Service对象
            IAdminUserService userService =
                DependencyResolver.Current.GetService<IAdminUserService>();

            foreach (var permAtt in permAtts)
            {
                if(!userService.HasPermission(userId.Value,permAtt.Permission))
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        AjaxResult ajaxResult = new AjaxResult();
                        ajaxResult.Status = "error";
                        ajaxResult.ErrorMsg = "抱歉，您没有权限进行此操作";
                        filterContext.Result = new JsonNetResult { Data = ajaxResult };
                    }
                    else
                    {
                        filterContext.Result
                       = new ContentResult { Content = "抱歉，您没有权限进行此操作" };
                    }
                    return;
                }
            }
        }
    }
}