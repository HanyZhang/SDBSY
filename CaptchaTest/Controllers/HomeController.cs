using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaptchaTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            CreateImage("W5RK");
            return View();

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
    }
}