using CodeCarvings.Piczard;
using SDBSY.Common;
using SDBSY.IService;
using SDBSY.Web.App_Start;
using SDBSY.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDBSY.Web.Controllers
{
    public class CertificateController : Controller
    {
        public ITeacherService teacherSvc { get; set; }
        // GET: Certificate
        [CheckPermission("Certificate.Index")]
        public ActionResult Index()
        {
            return View();
        }
        //[CheckPermission("Certificate.List")]
        //public ActionResult List(long educateId)
        //{
        //    var cers = teacherSvc.GetEducateCertificates(educateId);
        //    var model = new CertificateListViewModel() {
        //        EducateId = educateId,
        //        Certificates = cers,
        //    };
        //    return View(model);
        //}
        [HttpGet]
        [CheckPermission("Certificate.Add")]
        public ActionResult Add(long educateId)
        {
            return View(educateId);
        }
        [HttpPost]
        [CheckPermission("Certificate.Add")]
        public ActionResult Add(long educateId, HttpPostedFileBase file)
        {
            try
            {
                string ext = Path.GetExtension(file.FileName);//获取文件后缀名
                string fileMd5 = CommonHelper.CalcMD5(file.InputStream);//将文件流生成MD5值，作为文件名，避免重复
                string path = "/Upload/ZhengShu/Full/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fileMd5 + ext;
                string fullPath = HttpContext.Server.MapPath("~" + path);//获取全路径
                new FileInfo(fullPath).Directory.Create();//尝试创建可能不存在的文件夹，如果存在也不会报错
                file.InputStream.Position = 0;//指针复位，进行md5转换时，读取文件流，指针落在最后的位置，部分版本可能会出现保存的文件大小为0
                file.SaveAs(fullPath);

                //生成缩略图
                string thumbPath = "/Upload/ZhengShu/Thumb/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fileMd5 + ext;
                string thumbFullPath = HttpContext.Server.MapPath("~" + thumbPath);//获取全路径
                new FileInfo(thumbFullPath).Directory.Create();//尝试创建可能不存在的文件夹，如果存在也不会报错
                ImageProcessingJob jobThumb = new ImageProcessingJob();
                jobThumb.Filters.Add(new FixedResizeConstraint(200, 200));//缩略图尺寸 200*200
                jobThumb.SaveProcessedImageToFileSystem(file.InputStream, thumbFullPath);
                file.InputStream.Position = 0;


                //保存到数据库
                //teacherSvc.AddNewCertificate(educateId, path, thumbPath);

                return Json(new AjaxResult { Status = "ok" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
            }
        }
        [HttpPost]
        [CheckPermission("Certificate.Delete")]
        public ActionResult PatchDel(long[] selectedIds)//批量删除
        {
            if (selectedIds == null || selectedIds.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "未选中任何信息" });
            }
            foreach (long id in selectedIds)
            {
                teacherSvc.DelCertificate(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}