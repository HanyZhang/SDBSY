using SDBSY.Common;
using SDBSY.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDBSY.UserWeb.App_Start
{
    public class SDBSYAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //CheckSystemAttribute
            CheckSystemAttribute[] attrs = (CheckSystemAttribute[])filterContext.ActionDescriptor
                .GetCustomAttributes(typeof(CheckSystemAttribute), false);
            if (attrs.Length > 0)
            {
                ISystemSettingService settingSvc = DependencyResolver.Current.GetService<ISystemSettingService>();
                //IDataDictionaryService dataSvc = DependencyResolver.Current.GetService<IDataDictionaryService>();
                var systemOpen = settingSvc.GetVal("SystemOpen");
                if (string.IsNullOrEmpty(systemOpen) || systemOpen.ToUpper()!= "ON")//没有设置过，或者不是ON
                {
                    //根据不同的请求，给予不同的返回格式。确保ajax请求，浏览器端也能收到json格式
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        AjaxResult ajaxResult = new AjaxResult();
                        ajaxResult.Status = "error";
                        ajaxResult.ErrorMsg = "报名系统暂时关闭";
                        filterContext.Result = new JsonNetResult { Data = ajaxResult };
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("~/Home/SystemOff");
                    }
                    return;
                }
            }

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
                    ajaxResult.Data = "/Home/Signin";
                    ajaxResult.ErrorMsg = "登录信息超时";
                    filterContext.Result = new JsonNetResult { Data = ajaxResult };
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Home/Signin");
                }
                return;
            }

        }
    }
}