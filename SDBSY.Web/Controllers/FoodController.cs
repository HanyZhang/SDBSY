using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SDBSY.Common;
using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.Web.App_Start;
using SDBSY.Web.Models;

namespace SDBSY.Web.Controllers
{
    public class FoodController : Controller
    {
        public IFoodService foodSvc { get; set; }
        public IAdminLogService logSvc { get; set; }
        // GET: Food
        [CheckPermission("Food.Index")]
        public ActionResult Index(long id)
        {
            return View();
        }
        [HttpGet]
        [CheckPermission("Food.Add")]
        public ActionResult Add()
        {
            return View();
        }
        [CheckPermission("Food.Add")]
        public ActionResult Add(FoodAddPostModel model)
        {
            if(!ModelState.IsValid)
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
        [CheckPermission("Food.List")]
        public ActionResult List()
        {
            var foods = foodSvc.GetAll();
            return View(foods);
        }
        [HttpPost]
        [CheckPermission("Food.Delete")]
        public ActionResult Delete(long id)
        {
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    foodSvc.Delete(id);
                    long userId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(userId, "删除食材,id=" + id);
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
        [CheckPermission("Food.Delete")]
        public ActionResult PatchDel(long[] selectedIds)
        {
            if (selectedIds == null || selectedIds.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "未选中任何信息" });
            }
            long userId = (long)AdminHelper.GetUserId(HttpContext);
            foreach (long id in selectedIds)
            {
                foodSvc.Delete(id);
                logSvc.AddNew(userId, "删除食材,id=" + id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("FoodBuyRecord.Delete")]
        public ActionResult BuyRecordDelete(long id)
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
        [HttpPost]
        [CheckPermission("FoodBuyRecord.Delete")]
        public ActionResult RecordsPatchDel(long[] selectedIds)
        {
            if (selectedIds == null || selectedIds.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "未选中任何信息" });
            }
            long userId = (long)AdminHelper.GetUserId(HttpContext);
            foreach (long id in selectedIds)
            {
                foodSvc.RecordDelete(id);
                logSvc.AddNew(userId, "删除入库记录,id=" + id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("FoodBuyRecord.List")]
        public ActionResult BuyRecordsList(long id,DateTime? startTime,DateTime? endTime)
        {
            //食材
            var foods = foodSvc.GetAll().ToList();
            foods.Insert(0, new FoodDTO { Name = "全部" ,Id=0});
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
            if(startTime!=null)
            {
                records = records.Where(t => t.BuyTime >= startTime.Value).ToArray();
            }
            if(endTime!=null)
            {
                records = records.Where(t => t.BuyTime < endTime.Value.AddDays(1)).ToArray();
            }

            FoodBuyRecordsListViewModel model = new FoodBuyRecordsListViewModel() {
                Foods = foods.ToArray(),
                Records=records,
                FoodId=id,
                StartTime=startTime==null?"":startTime.Value.ToShortDateString(),
                EndTime=endTime==null?"":endTime.Value.ToShortDateString(),
            };
            return View(model);
        }
        [HttpGet]
        [CheckPermission("FoodBuyRecord.Add")]
        public ActionResult BuyRecordAdd()
        {
            var foods = foodSvc.GetAll();
            return View(foods);
        }
        [HttpPost]
        [CheckPermission("FoodBuyRecord.Add")]
        public ActionResult BuyRecordAdd(FoodBuyRecordAddPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    long id = foodSvc.AddNewBuyRecord(model.FoodId, model.UnitPrice, model.Amount, model.BuyTime, model.TotalPrice, model.Remark);
                    long userId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(userId, "添加入库记录,id=" + id);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch (Exception ex)
                {
                    return Json(new AjaxResult
                    {
                        Status = "error",
                        ErrorMsg = ex.Message
                    });
                }
            }
        }
        [HttpPost]
        public ActionResult ExportExcle(long[] selectedIds)
        {
            try
            {
                var records = foodSvc.GetAllRecords(selectedIds);
                //导出
                string path = "";
                CreateExcel(records, out path);
                path = path.Remove(0, 1);
                return Json(new AjaxResult { Status = "ok" ,Data=path});
            }
            catch(Exception ex)
            {
                return Json(new AjaxResult { Status = "error",ErrorMsg=ex.Message });
            }
        }
        private void CreateExcel(FoodBuyRecordDTO[] records, out string path)
        {
            //加载模板文件路径  
            string templetFileName = Server.MapPath("../foodBuyList.xls");
            HSSFWorkbook hssfworkbook = null;
            using (FileStream fs = System.IO.File.Open(templetFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //把xls文件读入workbook变量里，之后就可以关闭了
                hssfworkbook = new HSSFWorkbook(fs);
                fs.Close();
            }
            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
            //var row1 = sheet.GetRow(1);
            //var cell1 = row1.GetCell(6);
            //var value= cell1.DateCellValue;//.SetCellValue(DateTime.Now.ToString("yyyy年MM月dd日"));
            //cell1.SetCellValue(DateTime.Now);
            //定义单元格格式（边框）
            ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Center;//水平
            cellStyle.VerticalAlignment = VerticalAlignment.Center;//垂直
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            //写入数据
            decimal total = 0;
            for (int i = 0; i < records.Count(); i++)
            {
                var record = records[i];
                var row = sheet.GetRow(3 + i);
                row.GetCell(0).SetCellValue(record.BuyTime.ToShortDateString());//进货日期
                row.GetCell(1).SetCellValue(Convert.ToString(record.FoodName));//食物名称
                row.GetCell(2).SetCellValue(Convert.ToString(record.FoodUnit));//单位
                row.GetCell(3).SetCellValue(Convert.ToString(record.Amount));//数量
                row.GetCell(4).SetCellValue(Convert.ToString(record.UnitPrice));//单价
                row.GetCell(5).SetCellValue(Convert.ToString(record.TotalPrice));//总金额
                row.GetCell(6).SetCellValue(Convert.ToString(record.Remark));//备注
                for(int j=0;j<7;j++)//设置单元格格式
                {
                    row.GetCell(j).CellStyle = cellStyle;
                }
                total += record.TotalPrice;
            }
            //统计
            var totalRow = sheet.GetRow(3 + records.Count());
            totalRow.GetCell(1).SetCellValue("总计");
            totalRow.GetCell(5).SetCellValue(Convert.ToString(total));
            hssfworkbook.ForceFormulaRecalculation = true;

            string fileName = Guid.NewGuid().ToString("N") + ".xls";
            path = "~/Download/FoodFile/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fileName;
            string filePath = Server.MapPath(path);
            new FileInfo(filePath).Directory.Create();//尝试创建路径，即使存在也不会报错

            using (FileStream fs = System.IO.File.OpenWrite(filePath))
            {
                hssfworkbook.Write(fs);
            }
            
        }
    }
}