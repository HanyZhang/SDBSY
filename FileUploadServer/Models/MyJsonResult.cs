using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileUploadServer.Models
{
    public class MyJsonResult
    {
        public string Status { get; set; }
        public object Data { get; set; }
    }
}