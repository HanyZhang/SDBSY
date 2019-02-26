using FileUploadServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUploadServer.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return Content("home/index");
        }
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            MyJsonResult result = new MyJsonResult();
            if(file==null|| file.ContentLength<=0)
            {
                result.Status = "error";
                result.Data = "no file";
                return Json(result);
            }
            string uid = Request.Headers["uid"];
            string pwd = Request.Headers["pwd"];
            string path = Request.Headers["path"];
            if (uid=="sdbsy"&&pwd=="ysbds")
            {
                Guid guid = Guid.NewGuid();
                string thumbPath = "/upload/"+path+"/" + DateTime.Now.ToString("yyyyMMdd") + "/" + guid.ToString() + Path.GetExtension(file.FileName);
                string thumbFullPath = HttpContext.Server.MapPath("~" + thumbPath);//获取全路径
                new FileInfo(thumbFullPath).Directory.Create();
                file.SaveAs(thumbFullPath);
                result.Status = "ok";
                result.Data = thumbPath;
                return Json(result);
            }
            else
            {
                result.Status = "error";
                result.Data = "uid or pwd is wrong";
                return Json(result);
            }
        }
    }
}