using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using CodeCarvings.Piczard;
using Newtonsoft.Json;
using SDBSY.Common;
using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.TeacherWeb.App_Start;
using SDBSY.TeacherWeb.Models;
using log4net;

namespace SDBSY.TeacherWeb.Controllers
{
    [CheckLogin]
    public class InvoiceController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(InvoiceController));
        public static readonly string serverUrl = WebConfigurationManager.AppSettings["fileserver"];
        public ITeacherService teacherSvc { get; set; }
        public IInvoiceService invoiceSvc { get; set; }
        public IDataDictionaryService dataSvc { get; set; }
        // GET: Invoice
        [HttpGet]
        public ActionResult Add()
        {
            var id = (long)AdminHelper.GetUserId(HttpContext);
            var teacher = teacherSvc.GetByAdminId(id);
            if (teacher == null)
            {
                return View("Error", (object)"请先添加教师信息");
            }

            var classes = dataSvc.GetByName("ClassType");
            var model = new InvoiceAddViewModel()
            {
                TeacherId = teacher.Id,
                TeacherName = teacher.Name,
                Classes= JsonConvert.SerializeObject(classes, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
            };
            return View(model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(InvoiceAddPostModel  model)
        {
            
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult() { Status = "error",ErrorMsg = MVCHelper.GetValidMsg(ModelState)});
            }
            //1.保存信息
            var dto = new InvoiceAddNewDTO()
            {
                ClassId=model.ClassId,
                Detail = model.Detail,
                GoodsName = model.GoodsName,
                TeacherId = model.TeacherId,
                Total = model.Total,
                BuyDateTime = model.BuyDateTime
            };
            long id= invoiceSvc.AddNew(dto);
            //2.保存图片
            if (model.UpFiles.Length > 0)
            {
                for (int i = 0; i < model.UpFiles.Length; i++)
                {
                    HttpPostedFileBase file = model.UpFiles[i];//这个对象是用来接收文件,利用这个对象可以获取文件的name path等
                    try
                    {
                        //log.Debug("保存图片开始-------");
                        var result= await SaveImgAsync(file);
                        if (result.Status == "ok")
                        {
                            var dto1 = new InvoicePicAddNewDTO()
                            {
                                InvocieId = id,
                                Url = result.Data.ToString()
                            };
                            long picId = invoiceSvc.AddNewPic(dto1);
                            //log.Debug("保存图片成功-------");
                        }
                        else
                        {
                            //log.Debug("保存图片失败-------");
                            return Json(new AjaxResult() { Status = "error", ErrorMsg = result.Data.ToString() });
                        }
                    }
                    catch (Exception ex)
                    {

                        log.Debug("图片保存出错："+ex.Message);
                        return Json(new AjaxResult() { Status = "error", ErrorMsg = ex.Message });
                    }
                }
                
            }
            return Json(new AjaxResult() {Status = "ok"});
        }

        private async System.Threading.Tasks.Task<AjaxResult> SaveImgAsync(HttpPostedFileBase file)
        {
            var uri=serverUrl + "/Home/FileUpload/";
            Guid guid = Guid.NewGuid();
            using (var handler = new HttpClientHandler(){AutomaticDecompression = DecompressionMethods.None})
            using (HttpClient httpClient = new HttpClient(handler))
            {

                log.Debug("old-URI:" + httpClient.BaseAddress);
                log.Debug("serverUrl:" + serverUrl);
                log.Debug("string-URI:" + uri);
                httpClient.BaseAddress=new Uri(uri);
                //日志
                log.Debug("new-URI:"+httpClient.BaseAddress);
                //Bitmap bitmap = new Bitmap(memoryStream);
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Headers.Add("uid", "sdbsy");//增加账号密码，防止恶意上传
                content.Headers.Add("pwd", "ysbds");
                content.Headers.Add("path", "InvoicePics");
                content.Add(new StreamContent(file.InputStream), "file", guid.ToString() + ".png");
                var respMsg = httpClient.PostAsync(httpClient.BaseAddress, content).Result;
                string msgBody = await respMsg.Content.ReadAsStringAsync();
                //日志
                log.Debug("开始获取服务器保存的结果-------");
                if (respMsg.IsSuccessStatusCode)
                {
                    log.Debug("开始获取服务器结果成功！！！！！");
                    //请求成功
                    var result = (AjaxResult)Newtonsoft.Json.JsonConvert.DeserializeObject(msgBody, typeof(AjaxResult));
                    result.Data = serverUrl + result.Data;
                    result.Status = "ok";
                    return result;
                }
                else
                {
                    log.Debug("开始获取服务器结果失败！！！！！");
                    return new AjaxResult { Status = "error",ErrorMsg = msgBody};
                }

            }
        }

        /*private void SaveImg(long id,HttpPostedFileBase file)
        {
            //原图
            var ext = Path.GetExtension(file.FileName);//获取扩展名
            var fileMd5 = CommonHelper.CalcMD5(file.InputStream);//将文件流生成MD5值，作为文件名，避免重复
            string path = "/Upload/InvoicePics/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fileMd5 + ext;
            string fullPath = HttpContext.Server.MapPath("~" + path);//获取全路径
            new FileInfo(fullPath).Directory.Create();//尝试创建可能不存在的文件夹，如果存在也不会报错
            ImageProcessingJob jobNormal = new ImageProcessingJob();
            jobNormal.SaveProcessedImageToFileSystem(file.InputStream,fullPath);//原图保存
            file.InputStream.Position = 0;//指针复位，进行md5转换时，读取文件流，指针落在最后的位置，部分版本可能会出现保存的文件大小为0

            //缩略图
            var thumbPath = "/Upload/InvoiceThumbPics/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fileMd5 + ext;
            string thumbFullPath = HttpContext.Server.MapPath("~" + thumbPath);//获取全路径
            new FileInfo(thumbFullPath).Directory.Create();//尝试创建可能不存在的文件夹，如果存在也不会报错
            ImageProcessingJob jobThumb = new ImageProcessingJob();
            jobThumb.Filters.Add(new FixedResizeConstraint(200, 200));//缩略图尺寸 200*200
            jobThumb.SaveProcessedImageToFileSystem(file.InputStream, thumbFullPath);//缩略图保存

            var dto = new InvoicePicAddNewDTO()
            {
                InvocieId = id,
                Url=path,
                ThumbUrl = thumbPath
            };
            long picId = invoiceSvc.AddNewPic(dto);
        }*/
        

        
        public ActionResult List()
        {
            var id = (long)AdminHelper.GetUserId(HttpContext);
            var teacher = teacherSvc.GetByAdminId(id);
            if (teacher == null)
            {
                return View("Error", (object)"请先添加教师信息");
            }

            var invoices= invoiceSvc.GetByTeacherId(teacher.Id);
            var invoicePics = invoiceSvc.GetAllPics();
            var model=new InvoiceListViewModel();
            model.InvoicePics = invoicePics;
            model.Invoices = invoices;
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var invoice = invoiceSvc.GetById(id);
            var pics = invoiceSvc.GetPics(id);
            var classes = dataSvc.GetByName("ClassType");
            var model=new InvoiceEditViewModel()
            {
                Classes = JsonConvert.SerializeObject(classes, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                Invoice = invoice,
                Pics = pics
            };
            return View(model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(InvoiceEditPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult() { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            //1.保存信息
            var dto = new InvoiceEditDTO()
            {
                ClassId = model.ClassId,
                Detail = model.Detail,
                GoodsName = model.GoodsName,
                Id = model.Id,
                Total = model.Total,
                BuyDateTime = model.BuyDateTime
            };
            invoiceSvc.Update(dto);
            //2.保存图片
            if (model.UpFiles.Length > 0)
            {
                //2.1.清理原来的图片
                invoiceSvc.DeletePics(dto.Id);
                //2.2.保存新图片
                for (int i = 0; i < model.UpFiles.Length; i++)
                {
                    HttpPostedFileBase file = model.UpFiles[i];//这个对象是用来接收文件,利用这个对象可以获取文件的name path等
                    try
                    {
                        log.Debug("保存图片开始-------");
                        var result = await SaveImgAsync(file);
                        if (result.Status == "ok")
                        {
                            var dto1 = new InvoicePicAddNewDTO()
                            {
                                InvocieId = dto.Id,
                                Url = result.Data.ToString()
                            };
                            long picId = invoiceSvc.AddNewPic(dto1);
                            log.Debug("保存图片成功-------");
                        }
                        else
                        {
                            log.Debug("保存图片失败-------");
                            return Json(new AjaxResult() { Status = "error", ErrorMsg = result.Data.ToString() });
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(new AjaxResult() { Status = "error", ErrorMsg = ex.Message });
                    }
                }

            }
            return Json(new AjaxResult() {Status = "ok"});
        }

        [HttpPost]
        public ActionResult Del(long id)
        {
            invoiceSvc.Delete(id);
            return Json(new AjaxResult() {Status = "ok"});
        }
    }
}