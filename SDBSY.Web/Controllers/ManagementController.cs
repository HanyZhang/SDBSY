using SDBSY.Common;
using SDBSY.IService;
using SDBSY.Web.App_Start;
using SDBSY.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDBSY.Web.Controllers
{
    public class ManagementController : Controller
    {
        public IAdminUserService adminUserSvc { get; set; }
        public IAdminLogService logSvc { get; set; }
        // GET: Management
        
        public ActionResult Index()
        {
            /*long id= adminUserSvc.AddNewAdmin("administrator", "sdbsylqb");
            return Content("ok:"+id);*/
            long? userId = AdminHelper.GetUserId(HttpContext);
            if(userId==null)
            {
                return RedirectToAction("Signin");
            }
            var user = adminUserSvc.GetById(userId.Value);
            return View(user);
        }
        [HttpGet]
        public ActionResult Signin()
        {
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult Signin(AdminLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            string sessionCaptcha = (string)TempData["captcha"];
            if (model.Captcha != sessionCaptcha)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误" });
            }
            else
            {
                bool b = adminUserSvc.CheckLogin(model.UserName, model.Password);
                if (!b)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "管理员账号或者密码错误" });
                }
                else
                {
                    //登录成功，记录Session
                    long userId= adminUserSvc.GetByUserName(model.UserName).Id;
                    Session["LoginUserId"] = userId;
                    //Session["UserRole"] = "admin";
                    logSvc.AddNew(userId, "登录成功");
                    return Json(new AjaxResult { Status = "ok" });
                }
            }
        }
        public void CreateCaptcha()
        {
            string captcha = CommonHelper.CreateVerifyCode(4);
            //验证码使用一次就应该销毁，避免恶意登录
            TempData["captcha"] = captcha;
            CreateImage(captcha);
        }
        private void CreateImage(string randomcode)
        {
            int randAngle = 45;     //随机转动角度  
            int mapwidth = (int)(randomcode.Length * 20);
            using (Bitmap map = new Bitmap(mapwidth, 35))//创建图片背景，设置其长宽  
            using (Graphics graph = Graphics.FromImage(map))
            {
                graph.Clear(Color.AliceBlue);
                graph.DrawRectangle(new Pen(Color.Black, 0), 0, 0, map.Width - 1, map.Height - 1);//画一个边框  

                Random rand = new Random();

                // 生成背景噪点  
                Pen blackPen = new Pen(Color.LightGray, 0);
                for (int i = 0; i < 50; i++)
                {
                    int x = rand.Next(0, map.Width);
                    int y = rand.Next(0, map.Height);
                    graph.DrawRectangle(blackPen, x, y, 1, 1);
                }

                //验证码旋转，防止机器识别  
                char[] chars = randomcode.ToCharArray();//拆散字符串成单字符数组  

                //文字距中  
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                // 定义随机颜色列表  
                Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
                // 定义随机字体字体  
                string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };

                for (int i = 0; i < chars.Length; i++)
                {
                    int cindex = rand.Next(7);
                    int findex = rand.Next(5);

                    Font f = new System.Drawing.Font(font[findex], 16, System.Drawing.FontStyle.Bold); // 字体样式(参数2为字体大小)  
                    Brush b = new System.Drawing.SolidBrush(c[cindex]);

                    Point dot = new Point(13, 13);  // 括号内数值越大，字符间距越大  
                    float angle = rand.Next(0, randAngle);  // 转动的度数，如果将0改为-randAngle，那么旋转角度为-45度～45度  

                    graph.TranslateTransform(dot.X, dot.Y);
                    graph.RotateTransform(angle);
                    graph.DrawString(chars[i].ToString(), f, b, 2, 6, format); // 第4、5个参数控制左、上间距  
                    graph.RotateTransform(-angle);
                    graph.TranslateTransform(2, -dot.Y);
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/gif";
                Response.BinaryWrite(ms.ToArray());
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return Redirect("/Management/Signin");
        }
        public ActionResult Welcome()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            long userId = (long)AdminHelper.GetUserId(HttpContext);
            var user = adminUserSvc.GetById(userId);
            return View(user);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordPostModel model)
        {
            if(!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            if(!adminUserSvc.CheckOldPassword(model.Id,model.OldPassword))
            {
                return Json(new AjaxResult { Status = "error",ErrorMsg="旧密码输入错误" });
            }
            adminUserSvc.UpdatePassword(model.Id, model.NewPassword);
            long userId = (long)AdminHelper.GetUserId(HttpContext);
            logSvc.AddNew(userId, "修改密码");
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}