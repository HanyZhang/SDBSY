using SDBSY.Common;
using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.Web.App_Start;
using SDBSY.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace SDBSY.Web.Controllers
{
    public class MobileController : Controller
    {
        public IAdminUserService adminUserSvc { get; set; }
        public IAdminLogService logSvc { get; set; }
        public IFoodService foodSvc { get; set; }
        // GET: Mobile
        public ActionResult Index()
        {
            long? userId = AdminHelper.GetUserId(HttpContext);
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            var user = adminUserSvc.GetById(userId.Value);
            if(user.Role!=YongHuShenFen.Worker)
            {
                return View("Error", (object)"当前系统仅供后勤工作人员使用，其他功能敬请期待");
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            string loginChatcha = (string)TempData["Captcha"];
            if (string.IsNullOrEmpty(loginChatcha) || loginChatcha != model.Captcha)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误，请重试。" });
            }
            else
            {
                bool b = adminUserSvc.CheckLogin(model.UserName, model.Password);
                if (!b)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "账号或者密码错误" });
                }
                else
                {
                    //登录成功，记录Session
                    long userId = adminUserSvc.GetByUserName(model.UserName).Id;
                    Session["LoginUserId"] = userId;
                    logSvc.AddNew(userId, "登录成功");
                    return Json(new AjaxResult { Status = "ok" });
                }
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return Redirect("/Mobile/Login");
        }
        public ActionResult FoodList()
        {
            var foods = foodSvc.GetAll().OrderBy(t=>t.Id).Skip((1-1)*1).Take(1).ToArray();
            return View(foods);
        }
        [HttpPost]
        public ActionResult FoodListPage(int page)
        {
            var foods = foodSvc.GetAll().OrderBy(t => t.Id).Skip((page - 1) * 1).Take(1).ToArray();
            return Json(new AjaxResult { Status = "ok", Data = foods });
        }
        [HttpGet]
        [CheckPermission("Food.Add")]
        public ActionResult FoodAdd()
        {
            return View();
        }
        [HttpPost]
        [CheckPermission("Food.Add")]
        public ActionResult FoodAdd(FoodAddPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    long id = foodSvc.AddNew(model.Name, model.Unit, model.Supplier);
                    long userId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(userId, "添加食材,id=" + id);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch (Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }
        }
        [CheckPermission("FoodBuyRecord.List")]
        public ActionResult FoodBuyRecordList(long id, DateTime? startTime, DateTime? endTime)
        {
            //食材
            var foods = foodSvc.GetAll().ToList();
            foods.Insert(0, new FoodDTO { Name = "全部", Id = 0 });
            //食物的入库记录
            FoodBuyRecordDTO[] records;
            if (id <= 0)
            {//所有记录
                records = foodSvc.GetAllRecords();
            }
            else
            {
                records = foodSvc.GetAllRecords(id);
            }
            if (startTime != null)
            {
                records = records.Where(t => t.BuyTime >= startTime.Value).ToArray();
            }
            if (endTime != null)
            {
                records = records.Where(t => t.BuyTime < endTime.Value.AddDays(1)).ToArray();
            }

            FoodBuyRecordsListViewModel model = new FoodBuyRecordsListViewModel()
            {
                Foods = foods.ToArray(),
                Records = records,
                FoodId = id,
                StartTime = startTime == null ? "" : startTime.Value.ToShortDateString(),
                EndTime = endTime == null ? "" : endTime.Value.ToShortDateString(),
            };
            return View(model);
        }
        [HttpGet]
        [CheckPermission("FoodBuyRecord.Add")]
        public ActionResult FoodBuyRecordAdd()
        {
            var foods = foodSvc.GetAll();
            return View(foods);
        }
        [HttpPost]
        [CheckPermission("Food.Delete")]
        public ActionResult FoodDelete(long id)
        {
            long? userId = AdminHelper.GetUserId(HttpContext);
            if (userId == null)
            {
                return Json(new AjaxResult { Status="timeout",ErrorMsg="登录超时",Data="/Mobile/Login"});
            }
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    foodSvc.Delete(id);
                    logSvc.AddNew(userId.Value, "删除食材,id=" + id);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch (Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }
        }
        [HttpPost]
        [CheckPermission("FoodBuyRecord.Delete")]
        public ActionResult FoodBuyRecordDelete(long id)
        {
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    foodSvc.RecordDelete(id);
                    long userId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(userId, "删除入库记录,id=" + id);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch (Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }
        }
    }
}