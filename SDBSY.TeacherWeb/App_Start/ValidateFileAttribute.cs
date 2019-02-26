using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.TeacherWeb.App_Start
{
    public class ValidateFileAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int MaxContentLength = 1024 * 1024 * 4;
            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".bmp" };

            var file = value as HttpPostedFileBase[];

            if (file == null||file.Length<=0)
                return false;
            else
            {
                for(int i=0;i<file.Length;i++)
                {
                    if (!AllowedFileExtensions.Contains(file[i].FileName.Substring(file[i].FileName.LastIndexOf('.'))))
                    {
                        ErrorMessage = "请上传你的图片类型: " + string.Join(", ", AllowedFileExtensions);
                        return false;
                    }
                    else if (file[i].ContentLength > MaxContentLength)
                    {
                        ErrorMessage = "上传图片不能超过 : " + (MaxContentLength / 1024/1024).ToString() + "MB";
                        return false;
                    }
                    else
                        return true;
                }
                return true;
            }

            
            
            
        }
    }
}