using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class AdminUserAddPostModel
    {
        [Required]
        [Phone]
        public string UserName { get; set; }
        public int Role { get; set; }//1管理员 2教师 3后勤
        public long[] RoleIds { get; set; }
    }
}