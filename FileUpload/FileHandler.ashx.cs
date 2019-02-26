using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileUpload
{
    /// <summary>
    /// FileHandler 的摘要说明
    /// </summary>
    public class FileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try {
                HttpFileCollection httpFile = context.Request.Files;
                if(httpFile.Count<=0)
                {
                    context.Response.Write("error!");
                }
                else
                {
                    Guid guid = Guid.NewGuid();
                    var file = httpFile[0];
                    string ext = Path.GetExtension(file.FileName);
                    file.SaveAs(HttpContext.Current.Server.MapPath("~" + "/Upload/Photo/" + DateTime.Now.ToString("yyyyMMdd") + "/" + guid.ToString() + ext));
                    context.Response.Write("save success!");
                }


            }
            catch(Exception e)
            {
                context.Response.Write("error!"+e.Message );
            }

            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}