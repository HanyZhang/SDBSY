﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.TeacherWeb.Models
{
    public class InvoiceEditPostModel
    {
        public long Id { get; set; }
        [Display(Name = "物品名称")]
        [Required(ErrorMessage = "请输入{0}")]
        public string GoodsName { get; set; }
        [Display(Name = "购买时间")]
        [Required(ErrorMessage = "请输入{0}")]
        public DateTime BuyDateTime { get; set; }
        [Display(Name = "购买班级")]
        [Required(ErrorMessage = "请选择{0}")]
        public long ClassId { get; set; }
        [Display(Name = "总金额")]
        [Required(ErrorMessage = "请输入{0}")]
        public decimal Total { get; set; }
        [Display(Name = "购买明细")]
        [Required(ErrorMessage = "请输入{0}")]
        public string Detail { get; set; }
        [Display(Name = "存档资料")]
        public HttpPostedFileBase[] UpFiles { get; set; }
    }
}