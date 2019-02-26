using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Net;
using System.Web.Configuration;
using FileUpload.Models;

namespace FileUpload.Controllers
{
    public class HomeController : Controller
    {
        public static readonly string serverUrl = WebConfigurationManager.AppSettings["fileserver"];
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
        public async System.Threading.Tasks.Task<ActionResult> UploadAsync(HttpPostedFileBase file)
        {
            Guid guid = Guid.NewGuid();
            string ext = Path.GetExtension(file.FileName);
            using (HttpClient httpClient = new HttpClient())
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Headers.Add("uid", "sdbsy");//增加账号密码，防止恶意上传
                content.Headers.Add("pwd", "ysbds");
                using (Stream stream = file.InputStream)
                {
                    content.Add(new StreamContent(stream), "file", guid.ToString() + ext);
                    var respMsg = await httpClient.PostAsync(serverUrl+"/Home/FileUpload/", content);
                    string msgBody = await respMsg.Content.ReadAsStringAsync();

                    if( respMsg.StatusCode==HttpStatusCode.OK)
                    {
                        //请求成功
                        var result= (MyJsonResult)Newtonsoft.Json.JsonConvert.DeserializeObject(msgBody, typeof(MyJsonResult));
                        if(result.Status=="ok")
                        {
                            //上传成功
                        }
                        else
                        {
                            //上传失败
                        }
                    }

                    return Content(respMsg.StatusCode.ToString() + ";" + msgBody);
                }
            }
        }
    }
}