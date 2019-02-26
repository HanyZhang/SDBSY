using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using log4net;
using SDBSY.Common;
using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.TeacherWeb.App_Start;
using SDBSY.TeacherWeb.Models;

namespace SDBSY.TeacherWeb.Controllers
{
    [CheckLogin]
    public class CertificateController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(CertificateController));
        public static readonly string serverUrl = WebConfigurationManager.AppSettings["fileserver"];

        public ITeacherService teacherSvc { get; set; }

        // GET: Certificate
        public ActionResult List()
        {
            var id = (long)AdminHelper.GetUserId(HttpContext);
            var teacher = teacherSvc.GetByAdminId(id);
            if (teacher == null)
            {
                return View("Error", (object)"请先添加教师信息");
            }

            var cers = teacherSvc.GetCertificates(teacher.Id);
            var pics = teacherSvc.GetPics();
            var model=new CertificateListViewModel();
            model.Certificates = cers;
            model.CertificatePics = pics;
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var id = (long)AdminHelper.GetUserId(HttpContext);
            var teacher = teacherSvc.GetByAdminId(id);
            var model = new CertificateAddViewModel()
            {
                TeacherId = teacher.Id
            };
            return View(model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(CertificateAddPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult() { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            //保存证书信息
            long id = teacherSvc.AddNew(Todto(model));
            //保存图片
            if (model.UpFiles.Length > 0)
            {
                for (int i = 0; i < model.UpFiles.Length; i++)
                {
                    HttpPostedFileBase file = model.UpFiles[i];//这个对象是用来接收文件,利用这个对象可以获取文件的name path等
                    try
                    {
                        var result = await SaveImgAsync(file);
                        if (result.Status == "ok")
                        {
                            var url = result.Data.ToString();
                            long picId = teacherSvc.AddNew(id, url, string.Empty);
                        }
                        else
                        {
                            return Json(new AjaxResult() { Status = "error", ErrorMsg = result.Data.ToString() });
                        }
                    }
                    catch (Exception ex)
                    {

                        log.Debug("图片保存出错：" + ex.Message);
                        return Json(new AjaxResult() { Status = "error", ErrorMsg = ex.Message });
                    }
                }
            }
            return Json(new AjaxResult { Status = "ok" });
        }

        private async System.Threading.Tasks.Task<AjaxResult> SaveImgAsync(HttpPostedFileBase file)
        {
            Guid guid = Guid.NewGuid();
            using (HttpClient httpClient = new HttpClient())
            {
                //Bitmap bitmap = new Bitmap(memoryStream);
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Headers.Add("uid", "sdbsy");//增加账号密码，防止恶意上传
                content.Headers.Add("pwd", "ysbds");
                content.Headers.Add("path", "CertificatePics");
                content.Add(new StreamContent(file.InputStream), "file", guid.ToString() + ".png");
                var respMsg = await httpClient.PostAsync(serverUrl + "/Home/FileUpload/", content);
                string msgBody = await respMsg.Content.ReadAsStringAsync();

                if (respMsg.IsSuccessStatusCode)
                {
                    //请求成功
                    var result = (AjaxResult)Newtonsoft.Json.JsonConvert.DeserializeObject(msgBody, typeof(AjaxResult));
                    result.Data = serverUrl + result.Data;
                    result.Status = "ok";
                    return result;
                }
                else
                {
                    return new AjaxResult { Status = "error" };
                }

            }
        }

        private CertificateAddNewDTO Todto(CertificateAddPostModel model)
        {
            var dto = new CertificateAddNewDTO();
            dto.Name = model.Name;
            dto.TeacherId = model.TeacherId;
            //dto.Url = model.Url;
            //dto.ThumbUrl = model.ThumbUrl;
            dto.AwardDateTime = model.AwardDateTime;
            dto.AwardUnit = model.AwardUnit;
            dto.Number = model.Number;
            return dto;
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var cer = teacherSvc.GetCertificate(id);
            var pics = teacherSvc.GetPics(id);
            var model=new CertificateEditViewModel();
            model.Certificate = cer;
            model.CertificatePics = pics;

            return View(model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(CertificateEditPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult() { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }

            teacherSvc.Update(Todto(model));
            if (model.UpFiles.Length > 0)
            {
                //1.清除原来的图片
                teacherSvc.DelPics(model.Id);
                //2.保存新图片
                for (int i = 0; i < model.UpFiles.Length; i++)
                {
                    HttpPostedFileBase file = model.UpFiles[i];//这个对象是用来接收文件,利用这个对象可以获取文件的name path等
                    try
                    {
                        var result = await SaveImgAsync(file);
                        if (result.Status == "ok")
                        {
                            var url = result.Data.ToString();
                            long picId = teacherSvc.AddNew(model.Id, url, string.Empty);
                        }
                        else
                        {
                            return Json(new AjaxResult() { Status = "error", ErrorMsg = result.Data.ToString() });
                        }
                    }
                    catch (Exception ex)
                    {

                        log.Debug("图片保存出错：" + ex.Message);
                        return Json(new AjaxResult() { Status = "error", ErrorMsg = ex.Message });
                    }
                }
            }
            return Json(new AjaxResult() { Status = "ok" });
        }

        private CertificateEditDTO Todto(CertificateEditPostModel model)
        {
            var cer = new CertificateEditDTO()
            {
                Name = model.Name,
                AwardDateTime = model.AwardDateTime,
                AwardUnit = model.AwardUnit,
                Id = model.Id,
                Number = model.Number,
                //Url = model.Url,
                //ThumbUrl = model.ThumbUrl
            };
            return cer;
        }
        [HttpPost]
        public ActionResult Del(long id)
        {
            teacherSvc.DelCertificate(id);
            return Json(new AjaxResult() { Status = "ok" });
        }
    }
}