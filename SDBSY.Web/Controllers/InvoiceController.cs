using SDBSY.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDBSY.Common;
using SDBSY.Web.Models;

namespace SDBSY.Web.Controllers
{
    public class InvoiceController : Controller
    {
        public IInvoiceService invSvc { get; set; }

        // GET: Invoice
        public ActionResult List()
        {
            var invoices= invSvc.GetAll();
            InvoiceListViewModel model=new InvoiceListViewModel();
            model.Invoices = invoices;
            return View(model);
        }

        public ActionResult Index(long id)
        {
            var invoice = invSvc.GetById(id);
            var pics = invSvc.GetPics(id);
            var model=new InvoiceIndexViewModel();
            model.Invoice = invoice;
            model.Pics = pics;
            return View(model);
        }

        [HttpPost]
        public ActionResult Shenhe(long id, int status, string msg)
        {
            invSvc.Shenhe(id,status,msg);
            return Json(new AjaxResult {Status = "ok"});
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            invSvc.Delete(id);
            return Json(new AjaxResult() {Status = "ok"});
        }
    }
}