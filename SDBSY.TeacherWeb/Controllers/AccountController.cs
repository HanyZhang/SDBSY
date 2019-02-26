using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using SDBSY.Common;
using SDBSY.IService;
using SDBSY.TeacherWeb.Models;

namespace SDBSY.TeacherWeb.Controllers
{
    public class AccountController : Controller
    {
        public IAdminUserService adminSvc { get; set; }
        // GET: Account
        [HttpGet]
        public ActionResult Signin()
        {
            //long id = adminSvc.AddNew("15376261308", "123456", 2);
            //return Content(id.ToString());
            return View();
        }
        [HttpPost]
        public ActionResult Signin(SigninPostModel model)
        {
            //账号、密码、验证码  表单校验
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            //验证码
            string loginChatcha = (string)TempData["Captcha"];
            if (string.IsNullOrEmpty(loginChatcha) || loginChatcha.ToLower() != model.Captcha.ToLower())
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误，请重试。" });
            }
            //账号密码的验证
            bool signin = adminSvc.CheckLogin(model.Account, model.Password);
            if (!signin)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "账号或密码错误，请重试。" });
            }
            //j记录session
            Session["LoginTeacherId"] = adminSvc.GetByUserName(model.Account).Id;
            return Json(new AjaxResult { Status = "ok" });

        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(SignupPostModel model)
        {
            //表单校验
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            //验证码
            var loginChatcha = (string)TempData["Captcha"];
            if (string.IsNullOrEmpty(loginChatcha) || loginChatcha.ToLower() != model.Captcha.ToLower())
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误，请重试" });
            }

            long id = adminSvc.AddNew(model.Account, model.Password, model.Role);
            if (id == -1)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "手机号已被占用" });
            }
            return Json(new AjaxResult() {Status = "ok"});
        }
        [HttpPost]
        public ActionResult Validate(string account)
        {
            var admin = adminSvc.GetByUserName(account);
            return admin==null? Content("true"): Content("false");
        }

        public void CreateCaptcha()
        {
            string captcha = CommonHelper.CreateVerifyCode(4);
            //验证码使用一次就应该销毁，避免恶意
            TempData["Captcha"] = captcha;//验证码
            CommonHelper.CreateImage(captcha);
        }

        [HttpPost]
        public ActionResult SendSms(string phoneNum,string captcha)
        {
            if (string.IsNullOrWhiteSpace(phoneNum))
            {
                return Json(new AjaxResult() { Status = "error",ErrorMsg = "请输入手机号"});
            }

            if (string.IsNullOrWhiteSpace(captcha))
            {
                return Json(new AjaxResult() { Status = "error",ErrorMsg = "请输入图形验证码"});
            }
            #region 首先验证图形验证码
            string losePassChatcha = (string)TempData["Captcha"];
            if (string.IsNullOrEmpty(losePassChatcha) || captcha != losePassChatcha)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "图形验证码错误，请重试。" });
            }
            #endregion

            #region 检查手机号是否存在
            var user = adminSvc.GetByUserName(phoneNum);
            if (user == null)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = $"此用户{phoneNum}不存在"
                });
            }
            #endregion

            #region 生成验证码并保存到session

            Random r = new Random();
            string code = r.Next(100000, 999999).ToString();
            TempData["SmsCaptcha"] = code;
            Session["PhoneNum"] = phoneNum;

            #endregion

            var result = SendSMS.Send(phoneNum, code);
            if (result.result == 0)
            {
                return Json(new AjaxResult() { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult() { Status = "error",ErrorMsg = result.errMsg});
            }

            
        }
        [HttpGet]
        public ActionResult ResetPwdOne()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPwdOne(string phoneNum,string smsCode)
        {
            if (phoneNum != (string)Session["PhoneNum"])
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "请填写接受短信的手机号" });
            }
            if (smsCode != (string)TempData["SmsCaptcha"])
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "短信验证码错误" });
            }
            TempData["LosePass_OK"] = "OK";
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpGet]
        public ActionResult ResetPwdTwo()
        {
            var losePass_OK = (string) TempData["LosePass_OK"];
            if (string.IsNullOrEmpty(losePass_OK))
            {
                return View("Error", (object) "修改密码出现错误，请重新尝试");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ResetPwdTwo(string newPwd, string reNewPwd)
        {
            string phoneNum = (string)Session["PhoneNum"];
            if (string.IsNullOrEmpty(phoneNum))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "操作超时" });
            }

            if (newPwd != reNewPwd)
            {
                return Json(new AjaxResult() {Status = "error", ErrorMsg = "两次输入的密码不一致"});
            }
            var user = adminSvc.GetByUserName(phoneNum);
            if (user == null)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = $"此用户{phoneNum}不存在"
                });
            }

            adminSvc.UpdatePassword(user.Id,newPwd);
            return Json(new AjaxResult() {Status = "ok"});
        }

        public ActionResult ResetPwdOK()
        {
            return View();
        }
    }
}