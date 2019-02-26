using SDBSY.Common;
using System;
using System.Web.Mvc;

namespace SDBSY.TeacherWeb.App_Start
{
    public class TeacherWebAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string loginUrl = "/Account/Signin";

            CheckLoginAttribute[] attrs =(CheckLoginAttribute[])
                filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(CheckLoginAttribute), false);
            if (attrs.Length <= 0)
            {
                return;
            }

            //得到当前登录用户的id
            long? userId = (long?)filterContext.HttpContext.Session["LoginTeacherId"];

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
                    filterContext.Result = new RedirectResult("~" + loginUrl);

                }
            }
        }
    }
}