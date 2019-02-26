using SDBSY.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDBSY.IService;
using SDBSY.DTO;
using SDBSY.Web.Models;
using SDBSY.Common;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Reflection;
using System.ComponentModel;
using System.Net;
using System.Transactions;

namespace SDBSY.Web.Controllers
{
    public class StudentController : Controller
    {
        public IStudentService stuSvc { get; set; }
        public IDataDictionaryService dataSvc { get; set; }
        public IPlaceService placeSvc { get; set; }
        public INewPlaceService newplaceSvc { get; set; }
        public INationService nationSvc { get; set; }
        public ICountryService couSvc { get; set; }
        public IParentService pareSvc { get; set; }
        public IGuardianService guaSvc { get; set; }
        public IAdminLogService logSvc { get; set; }
        // GET: Student
        //[CheckPermission("Stu.List")]
        //public StudentController(IStudentService stuSvc)
        //{
        //    this.stuSvc = stuSvc;
        //}
        public ActionResult List(long classId, DateTime? startTime, DateTime? endTime)//在园
        {
            var students = GetStudents(classId, startTime, endTime);

            var classes = dataSvc.GetByName("ClassType").ToList();
            classes.Insert(0, new DataDictionaryDTO { Name = "ClassType", Value = "全部" });
            StudentListViewModel model = new StudentListViewModel();
            model.Students = students;
            model.Classes = classes.ToArray();
            model.ClassId = classId;
            return View(model);
        }
        private StudentDTO[] GetStudents(long classId, DateTime? startTime, DateTime? endTime)
        {
            StudentDTO[] students;
            if (classId <= 0)//已在园全部
            {
                students = stuSvc.GetAll().Where(t => t.Status == ShenHeZhuangTai.TongGuo && t.IsFinishSchool == false && t.ClassId > 0).ToArray();
            }
            else
            {
                students = stuSvc.GetAll().Where(t => t.Status == ShenHeZhuangTai.TongGuo && t.ClassId == classId && t.IsFinishSchool == false).ToArray();
            }
            if (startTime != null)
            {
                students = students.Where(t => t.CreateDateTime >= startTime.Value).ToArray();
            }
            if (endTime != null)
            {
                students = students.Where(t => t.CreateDateTime < endTime.Value.AddDays(1)).ToArray();
            }
            return students;
        }
        private StudentDTO[] GetStudents(long[] selectIds)
        {
            StudentDTO[] students = stuSvc.GetById(selectIds);
            return students;
        }
        //[CheckPermission("Stu.List")]
        public ActionResult NewList()//新报名
        {
            var students = stuSvc.GetAll().Where(t => t.IsFinishSchool == false && t.Status == ShenHeZhuangTai.MoRen).ToArray();

            return View(students);
        }
        //[CheckPermission("Stu.List")]
        public ActionResult BiYeList()//已毕业
        {
            var students = stuSvc.GetAll().Where(t => t.ClassId == null && t.IsFinishSchool == true).ToArray();
            return View(students);
        }
        [HttpGet]
        [CheckPermission("Stu.Index")]
        public ActionResult Index(long id)
        {
            var stu = stuSvc.GetById(id);
            var classes = dataSvc.GetByName("ClassType");
            var idCardTypes = dataSvc.GetByName("ChildIdCardType");//幼儿证件类型
            var bloodTypes = dataSvc.GetByName("BloodType");//血型
            var countries = couSvc.GetAll();//国籍
            var nations = nationSvc.GetAll().ToList();//民族
            nations.Insert(0, new NationDTO { Name = "请选择" });

            var identities = dataSvc.GetByName("Identity");//港澳台侨外
            //var places = placeSvc.GetAll().ToList();//地市代码
            var newplaces = newplaceSvc.GetAll().ToList();
            newplaces.Insert(0, new NewPlaceDTO { Name = "请选择" });

            var huKouXingZhi = dataSvc.GetByName("HuKouXingZhi").ToList();//户口性质
            huKouXingZhi.Insert(0, new DataDictionaryDTO { Name = "HuKouXingZhi", Value = "请选择" });
            var feiNongHuKouTypes = dataSvc.GetByName("FeiNongHuKouType").ToList();//非农户口类型
            feiNongHuKouTypes.Insert(0, new DataDictionaryDTO { Name = "FeiNongHuKouType", Value = "请选择" });

            var studyTypes = dataSvc.GetByName("StudyType");//就读方式
            var isStayAtHome = dataSvc.GetByName("IsStayAtHome");//是否留守儿童
            var healthyTypes = dataSvc.GetByName("HealthyType");//健康状况
            var disabilityTypes = dataSvc.GetByName("DisabilityType").ToList();//残疾类别
            disabilityTypes.Insert(0, new DataDictionaryDTO { Name = "DisabilityType", Value = "请选择" });

            var adultIdCardTypes = dataSvc.GetByName("AdultIdCardType");//成人证件类型
            EditInfoViewModel model = new EditInfoViewModel();
            model.Classes = classes;
            model.Student = stu;
            model.IdCardTypes = idCardTypes;
            model.BloodTypes = bloodTypes;
            model.Countrties = countries;
            model.Nations = nations.ToArray();
            model.Identities = identities;
            model.Places = newplaces.ToArray();
            model.HuKouXingZhi = huKouXingZhi.ToArray();
            model.FeiNongHuKouTypes = feiNongHuKouTypes.ToArray();
            model.StudyTypes = studyTypes;
            model.IsStayAtHome = isStayAtHome;
            model.HealthyTypes = healthyTypes;
            model.DisabilityTypes = disabilityTypes.ToArray();
            model.AdultIdCardTypes = adultIdCardTypes;

            return View(model);
        }
        [HttpGet]
        [CheckPermission("Stu.Edit")]
        public ActionResult Edit(long id)
        {
            var stu = stuSvc.GetById(id);
            var classes = dataSvc.GetByName("ClassType");
            var idCardTypes = dataSvc.GetByName("ChildIdCardType");//幼儿证件类型
            var bloodTypes = dataSvc.GetByName("BloodType");//血型
            var countries = couSvc.GetAll();//国籍
            var nations = nationSvc.GetAll().ToList();//民族
            nations.Insert(0, new NationDTO { Name = "请选择" });

            var identities = dataSvc.GetByName("Identity");//港澳台侨外
            //var places = placeSvc.GetAll().ToList();//地市代码
            //places.Insert(0, new PlaceDTO { Name = "请选择" });
            var newplaces = newplaceSvc.GetAll().ToList();
            newplaces.Insert(0, new NewPlaceDTO { Name = "请选择" });

            var huKouXingZhi = dataSvc.GetByName("HuKouXingZhi").ToList();//户口性质
            huKouXingZhi.Insert(0, new DataDictionaryDTO { Name = "HuKouXingZhi", Value = "请选择" });
            var feiNongHuKouTypes = dataSvc.GetByName("FeiNongHuKouType").ToList();//非农户口类型
            feiNongHuKouTypes.Insert(0, new DataDictionaryDTO { Name = "FeiNongHuKouType", Value = "请选择" });

            var studyTypes = dataSvc.GetByName("StudyType");//就读方式
            var isStayAtHome = dataSvc.GetByName("IsStayAtHome");//是否留守儿童
            var healthyTypes = dataSvc.GetByName("HealthyType");//健康状况
            var disabilityTypes = dataSvc.GetByName("DisabilityType").ToList();//残疾类别
            disabilityTypes.Insert(0, new DataDictionaryDTO { Name = "DisabilityType", Value = "请选择" });

            var adultIdCardTypes = dataSvc.GetByName("AdultIdCardType");//成人证件类型
            EditInfoViewModel model = new EditInfoViewModel();
            model.Classes = classes;
            model.Student = stu;
            model.IdCardTypes = idCardTypes;
            model.BloodTypes = bloodTypes;
            model.Countrties = countries;
            model.Nations = nations.ToArray();
            model.Identities = identities;
            model.Places = newplaces.ToArray();
            model.HuKouXingZhi = huKouXingZhi.ToArray();
            model.FeiNongHuKouTypes = feiNongHuKouTypes.ToArray();
            model.StudyTypes = studyTypes;
            model.IsStayAtHome = isStayAtHome;
            model.HealthyTypes = healthyTypes;
            model.DisabilityTypes = disabilityTypes.ToArray();
            model.AdultIdCardTypes = adultIdCardTypes;

            return View(model);
        }
        [HttpPost]
        [CheckPermission("Stu.Edit")]
        public ActionResult Edit(StudentEditDTO model)
        {
            var stu = stuSvc.GetById(model.Id);
            if (stu == null)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "幼儿信息不存在" });
            }
            if (!CommonHelper.CheckIDCard18(model.IdCardNum))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "幼儿证件号码验证错误。" });
            }

            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    //1、更新监护人

                    if (stu.GuardianId == model.GuardianId)//传值过程中未出错
                    {
                        guaSvc.Update(stu.GuardianId, model.GuardianName, model.GuardianPhoneNum, model.GuardianCardTypeId, model.GuardianCardNum);
                    }
                    //2、更新父母
                    if (model.FatherId != null && model.FatherId.Value > 0)
                    {
                        pareSvc.Update(stu.FatherId.Value, model.FatherName, model.FatherWorkUnit, model.FatherPhoneNum, model.FatherCardTypeId.Value, model.FatherCardNum);
                    }
                    else//添加
                    {
                        if (!string.IsNullOrEmpty(model.FatherName))
                        {
                            model.FatherId = pareSvc.AddNew(model.FatherName, model.FatherWorkUnit, model.FatherPhoneNum, model.FatherCardTypeId.Value, model.FatherCardNum);
                        }
                    }
                    if (model.MotherId != null && model.MotherId.Value > 0)
                    {
                        pareSvc.Update(stu.MotherId.Value, model.MotherName, model.MotherWorkUnit, model.MotherPhoneNum, model.MotherCardTypeId.Value, model.MotherCardNum);
                    }
                    else//添加
                    {
                        if (!string.IsNullOrEmpty(model.MotherName))
                        {
                            model.MotherId = pareSvc.AddNew(model.MotherName, model.MotherWorkUnit, model.MotherPhoneNum, model.MotherCardTypeId.Value, model.MotherCardNum);
                        }
                    }

                    //更新父母信息或着监护人信息时，身份证号重复处理
                    if (model.MotherId < 0 || model.FatherId < 0)
                    {
                        throw new ArgumentException("父母身份证号已被占用，详情请联系管理员");
                    }


                    //3、更新学生
                    stuSvc.UpdateStudent(model);


                    //stuSvc.UpdateClass(model.Id, classId);
                    //stuSvc.AddGoUpRecord(model.Id, stu.ClassId, classId);
                    stuSvc.UpdateStatus(model.Id, ShenHeZhuangTai.TongGuo);
                    //记录操作日志
                    long adminId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(adminId, "更新幼儿信息，studentId：" + model.Id);
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
        [CheckPermission("Stu.Index")]
        public ActionResult StayInClass(long id)
        {
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    stuSvc.StayInClass(id, true);
                    long adminId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(adminId, "设置幼儿留班，studentId：" + id);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch (Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }

        }
        [HttpGet]
        public ActionResult BiYe(long studentId)
        {
            return View(studentId);
        }
        [HttpPost]
        [CheckPermission("Stu.Index")]
        public ActionResult BiYe(long studentId, DateTime dateTime)
        {
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    stuSvc.FinishSchool(studentId, dateTime);//毕业并添加升班（毕业）记录
                    long adminId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(adminId, "设置幼儿毕业，studentId：" + studentId);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch (Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }
        }
        //自定义导出
        [HttpPost]
        [CheckPermission("Stu.Index")]
        public ActionResult ExportExcle(ChooseFieldPostModel model)
        {
            if (model.DicFields == null || model.DicFields.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "请选择需要导出的字段" });
            }
            var students = GetStudents(model.SelectedIds);
            if (students == null || students.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "没有符合条件的学生信息" });
            }
            string path = "";
            //WriteToExcel(students ,model.DicFields, out path);
            CreateExcel(students, model.DicFields, out path);
            path = path.Remove(0, 1);
            long adminId = (long)AdminHelper.GetUserId(HttpContext);
            logSvc.AddNew(adminId, "导出幼儿信息");
            return Json(new AjaxResult { Status = "ok", Data = path });
        }
        #region 导出自定义表格
        private void CreateExcel(StudentDTO[] stus, string[] fields, out string path)
        {
            string fileName = Guid.NewGuid().ToString("N") + ".xls";
            path = "~/Download/File/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fileName;
            string filePath = Server.MapPath(path);
            new FileInfo(filePath).Directory.Create();//尝试创建路径，即使存在也不会报错
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ICellStyle style = hssfworkbook.CreateCellStyle();//单元格样式
            style.Alignment = HorizontalAlignment.Left;//水平左对齐
            style.VerticalAlignment = VerticalAlignment.Center;//垂直居中
            ISheet sheet = hssfworkbook.CreateSheet();

            HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();

            sheet.CreateFreezePane(0, 1);//冻结列头行
            #region 头行
            IRow rowTitle = sheet.CreateRow(0);//创建头行
            for (int i = 0; i < fields.Length; i++)
            {
                sheet.SetColumnWidth(i, 35 * 256);
                ICell cell = rowTitle.CreateCell(i);//创建单元格

                //获取StudentDTO各属性的DisplayName
                Type t = typeof(StudentDTO);
                var playName = t.GetProperty(fields[i]);
                var displayName = playName.GetCustomAttribute<DisplayNameAttribute>();
                cell.SetCellValue(displayName.DisplayName);
            }

            #endregion
            #region 学生数据行
            for (int i = 0; i < stus.Length; i++)
            {
                IRow row = sheet.CreateRow(i + 1);//创建学生数据行
                for (int j = 0; j < fields.Length; j++)
                {
                    string key = fields[j];
                    ICell cell = row.CreateCell(j);//创建单元格
                    cell.CellStyle = style;
                    var value = stus[i].GetType().GetProperty(key).GetValue(stus[i], null);//根据属性名称获取属性值
                    string strValue = "";
                    if (value is DateTime)//数据类型转换，日期型
                    {
                        DateTime date = (DateTime)value;
                        strValue = date.ToString("yyyyMMdd");
                    }
                    else if (value is bool)//布尔型
                    {
                        strValue = (bool)value ? "是" : "否";
                    }
                    else if (Convert.ToString(value).IndexOf("http://") == 0)
                    {
                        row.Height = 14 * 256;
                        SetPic(hssfworkbook, patriarch, Convert.ToString(value), sheet, i + 1, j);
                        continue;
                    }
                    else
                    {
                        strValue = Convert.ToString(value);//这样如果value为null，则返回string.empty;
                                                           //value.ToString()则会抛NullReferenceException异常
                    }
                    cell.SetCellValue(strValue);//设置单元格值
                }
            }
            #endregion
            #region 表格后统计信息
            IRow rowTotal = sheet.CreateRow(stus.Length + 3);
            ICell cell00 = rowTotal.CreateCell(0);
            cell00.SetCellValue("共计：" + stus.Length + "人");
            ICell cell01 = rowTotal.CreateCell(1);
            int boy = stus.Count(t => t.Gender == false);
            cell01.SetCellValue("其中男：" + boy.ToString());
            int girl = stus.Count(t => t.Gender == true);
            ICell cell02 = rowTotal.CreateCell(2);
            cell02.SetCellValue("女：" + girl.ToString());
            #endregion
            sheet.ForceFormulaRecalculation = true;
            using (FileStream filess = System.IO.File.OpenWrite(filePath))
            {
                hssfworkbook.Write(filess);
            }
        }
        #endregion
        //根据模板导出
        [HttpPost]
        public ActionResult ExportExcleWithModel(long[] selectedIds)
        {
            var students = GetStudents(selectedIds);
            if (students == null || students.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "没有符合条件的学生信息" });
            }
            string path = "";
            //WriteToExcel(students ,model.DicFields, out path);
            WriteToExcel(students, out path);
            path = path.Remove(0, 1);
            long adminId = (long)AdminHelper.GetUserId(HttpContext);
            logSvc.AddNew(adminId, "用全省模板导出幼儿信息");
            return Json(new AjaxResult { Status = "ok", Data = path });
        }
        #region 导出省系统模板

        /// <summary>
        /// 导出省系统模板
        /// </summary>
        /// <param name="stus">数据</param>
        /// <param name="path"></param>
        public void WriteToExcel(StudentDTO[] stus, out string path)
        {
            var guid = Guid.NewGuid();

            var filePath = string.Empty;
            var filename = guid.ToString() + ".xls";
            path = "~/Download/File/" + DateTime.Now.ToString("yyyyMMdd") + "/" + filename;
            //临时存放路径
            filePath = Server.MapPath(path);
            new FileInfo(filePath).Directory.Create(); //尝试创建路径，即使存在也不会报错
            //Excel模版
            string masterPath = Server.MapPath("~/Files/childModel.xls");
            //复制Excel模版
            System.IO.File.Copy(masterPath, filePath);

            // 先把文件的属性读取出来 
            FileAttributes attrs = System.IO.File.GetAttributes(filePath);

            // 下面表达式中的 1 是 FileAttributes.ReadOnly 的值 
            // 此表达式是把 ReadOnly 所在的位改成 0, 
            attrs = (FileAttributes)((int)attrs & ~(1));

            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            IWorkbook hssfworkbook = new HSSFWorkbook(file);

            //the first sheet 幼儿基本信息
            ISheet sheet1 = hssfworkbook.GetSheet("幼儿基本信息");
            for (int i = 0; i < stus.Count(); i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue("LG" + stus[i].IdCardNum); //标识码
                row.CreateCell(1).SetCellValue(stus[i].Name); //姓名
                row.CreateCell(2).SetCellValue(stus[i].GenderName); //性别
                row.CreateCell(3).SetCellValue(stus[i].InKindergartenDate.ToString("yyyyMMdd")); //入园日期
                

                row.CreateCell(4).SetCellValue(stus[i].StudyTypeName); //就读方式
                row.CreateCell(5).SetCellValue(stus[i].BirthDate.ToString("yyyyMMdd")); //出生日期
                row.CreateCell(6).SetCellValue(stus[i].NationName); //民族
                row.CreateCell(7).SetCellValue(stus[i].BloodTypeName); //血型
                row.CreateCell(8).SetCellValue(stus[i].IdCardTypeName); //证件类型
                row.CreateCell(9).SetCellValue(stus[i].IdCardNum); //证件号码
                row.CreateCell(10).SetCellValue(stus[i].PriginPlace); //籍贯
                row.CreateCell(11).SetCellValue(stus[i].HomePlace); //住址
                row.CreateCell(12).SetCellValue(stus[i].BirthPlaceCode); //出生地代码
                row.CreateCell(13).SetCellValue(stus[i].HuKouPlaceCode); //户口代码
                row.CreateCell(14).SetCellValue(stus[i].HuKouXingZhiName); //户口性质
                row.CreateCell(15).SetCellValue(stus[i].FeiNongHuKouTypeName); //非农户口类型
                row.CreateCell(16).SetCellValue(stus[i].CountryName); //国籍地区
                row.CreateCell(17).SetCellValue(stus[i].IdentityName); //港澳台侨外
                row.CreateCell(18).SetCellValue(stus[i].HealthyTypeName); //健康状况

                if (stus[i].IsOnlyBaby) //独生子女
                {
                    row.CreateCell(19).SetCellValue("是");
                }
                else
                {
                    row.CreateCell(19).SetCellValue("否");
                }

                row.CreateCell(20).SetCellValue(stus[i].IsStayAtHomeName); //留守儿童

                if (stus[i].IsFollowWorkInCity) //进城务工子女
                {
                    row.CreateCell(21).SetCellValue("是");
                }
                else
                {
                    row.CreateCell(21).SetCellValue("否");
                }

                if (stus[i].IsOrphan) //孤儿
                {
                    row.CreateCell(22).SetCellValue("是");
                }
                else
                {
                    row.CreateCell(22).SetCellValue("否");
                }

                if (stus[i].IsDisability) //残疾
                {
                    row.CreateCell(23).SetCellValue("是");
                }
                else
                {
                    row.CreateCell(23).SetCellValue("否");
                }

                row.CreateCell(24).SetCellValue(stus[i].DisabilityTypeName); //残疾类型
            }

            sheet1.ForceFormulaRecalculation = true;

            //the second sheet 幼儿家庭成员信息

            ISheet sheet2 = hssfworkbook.GetSheet("幼儿家庭成员基本信息");
            for (int i = 0; i < stus.Count(); i++)
            {
                IRow row = sheet2.CreateRow(i + 1);
                /*
                 * 幼儿个人标识码	
                 * 家庭成员姓名	
                 * 家庭成员身份证件类型	
                 * 家庭成员身份证件号码	
                 * 是否监护人	
                 * 家庭成员手机号
                 */
                row.CreateCell(0).SetCellValue("LG" + stus[i].IdCardNum);
                row.CreateCell(1).SetCellValue(stus[i].GuardianName);
                row.CreateCell(2).SetCellValue(stus[i].GuardianCardTypeName);
                row.CreateCell(3).SetCellValue(stus[i].GuardianCardNum);
                row.CreateCell(4).SetCellValue("是");
                string tel = stus[i].FatherPhoneNum == string.Empty
                    ? (stus[i].MotherPhoneNum == string.Empty ? stus[i].OtherTel : stus[i].MotherPhoneNum)
                    : stus[i].FatherPhoneNum;
                row.CreateCell(5).SetCellValue(tel);
            }

            sheet2.ForceFormulaRecalculation = true;

            using (FileStream filess = System.IO.File.OpenWrite(filePath))
            {
                hssfworkbook.Write(filess);
            }
        }

        #endregion
        private void SetPic(HSSFWorkbook workbook, HSSFPatriarch patriarch, string path, ISheet sheet, int rowline, int col)
        {
            if (string.IsNullOrEmpty(path)) return;
            using (WebClient webClient = new WebClient())
            {
                webClient.Credentials = CredentialCache.DefaultCredentials;
                //byte[] bytes = System.IO.File.ReadAllBytes(Server.MapPath(path));//相对路径
                byte[] bytes = webClient.DownloadData(path);
                int pictureIdx = workbook.AddPicture(bytes, PictureType.PNG);
                /*
                  参数的解析: HSSFClientAnchor（int dx1,int dy1,int dx2,int dy2,int col1,int row1,int col2,int row2)

                    dx1:图片左边相对excel格的位置(x偏移) 范围值为:0~1023;即输100 偏移的位置大概是相对于整个单元格的宽度的100除以1023大概是10分之一

                    dy1:图片上方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。

                    dx2:图片右边相对excel格的位置(x偏移) 范围值为:0~1023; 原理同上。

                    dy2:图片下方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。

                    col1和row1 :图片左上角的位置，以excel单元格为参考,比喻这两个值为(1,1)，那么图片左上角的位置就是excel表(1,1)单元格的右下角的点(A,1)右下角的点。

                    col2和row2:图片右下角的位置，以excel单元格为参考,比喻这两个值为(2,2)，那么图片右下角的位置就是excel表(2,2)单元格的右下角的点(B,2)右下角的点。
                 */
                HSSFClientAnchor anchor = new HSSFClientAnchor(70, 10, 0, 0, col, rowline, col + 1, rowline + 1);
                //把图片插到相应的位置
                HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
            }
        }

        /*
        [HttpPost]
        //[CheckPermission("Stu.List")]
        public ActionResult AllSetClass(long classId, long[] selectedIds)
        {
            if (selectedIds == null || selectedIds.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "请选择要设置的学生" });
            }
            int err = 0;
            foreach (long id in selectedIds)
            {
                using (var tran = new TransactionScope())//事务开启
                {
                    try
                    {
                        var stu = stuSvc.GetById(id);
                        stuSvc.UpdateClass(id, classId);
                        stuSvc.UpdateStatus(id, ShenHeZhuangTai.TongGuo);

                        stuSvc.AddGoUpRecord(id, stu.ClassId, classId);
                        long adminId = (long)AdminHelper.GetUserId(HttpContext);
                        logSvc.AddNew(adminId, "设置幼儿班级，studentId：" + id + "，classId:" + classId);
                        tran.Complete();
                    }
                    catch (Exception ex)
                    {
                        //如何处理此处的异常呢？
                        err++;
                    }
                }
            }
            return Json(new AjaxResult { Status = "ok" });
        }*/
        [HttpPost]
        [CheckPermission("Stu.Delete")]
        public ActionResult PatchDel(long[] selectedIds)
        {
            if (selectedIds == null || selectedIds.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "未选中任何信息" });
            }
            foreach (long id in selectedIds)
            {
                stuSvc.MarkDeleted(id);
                long adminId = (long)AdminHelper.GetUserId(HttpContext);
                logSvc.AddNew(adminId, "删除幼儿信息，studentId：" + id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("Stu.Delete")]
        public ActionResult Del(long id)
        {
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    stuSvc.MarkDeleted(id);
                    long adminId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(adminId, "删除幼儿信息，studentId：" + id);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch (Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }
        }
        [HttpGet]
        [CheckPermission("Stu.List")]
        public ActionResult UnPassList()
        {
            var students = stuSvc.GetAll().Where(t => t.Status == ShenHeZhuangTai.BoHui).ToArray();//审核未通过
            return View(students);
        }
        [HttpPost]
        //[CheckPermission("Stu.List")]
        public ActionResult ShenHe(long id, bool pass)
        {
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    stuSvc.ShenHe(id, pass);
                    long adminId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(adminId, "审核幼儿信息，studentId：" + id);
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
        public ActionResult PiLiangShenHe(long[] selectedIds, bool pass)
        {
            foreach (long id in selectedIds)
            {
                using (var tran = new TransactionScope())//事务开启
                {
                    try
                    {
                        stuSvc.ShenHe(id, pass);
                        long adminId = (long)AdminHelper.GetUserId(HttpContext);
                        logSvc.AddNew(adminId, "审核幼儿信息，studentId：" + id);
                        tran.Complete();
                    }
                    catch (Exception ex)
                    {
                        return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                    }
                }
            }
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        public ActionResult ChooseField()
        {
            var dic = GetClass();
            ChooseFieldViewModel model = new ChooseFieldViewModel
            {
                //SelectIds= selectedIds,
                //ClassId = classId,
                Dictionary = dic,
                //Status=status                
            };

            return View(model);
        }
        private Dictionary<string, string> GetClass()
        {
            Type type = typeof(StudentDTO);
            PropertyInfo[] properties = type.GetProperties();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var property in properties)
            {
                var displayName = property.GetCustomAttribute<DisplayNameAttribute>();
                if (displayName == null)
                {
                    continue;
                }
                string chineseName = displayName.DisplayName;//---“性别”
                string englishName = property.Name;//---“Gender”
                dic.Add(englishName, chineseName);
            }
            return dic;
        }
        [HttpGet]
        public ActionResult PiLiangFenBan()
        {
            var classes = dataSvc.GetByName("ClassType");
            return View(classes);
        }
        [HttpPost]
        public ActionResult PiLiangFenBan(long[] selectedIds, long classId, DateTime changeTime)
        {
            if (selectedIds == null || selectedIds.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "未选中任何信息" });
            }
            foreach (long id in selectedIds)
            {
                var stu = stuSvc.GetById(id);
                if (stu == null)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "不存在的学生信息,id=" + id });
                }
                if (stu.Status != ShenHeZhuangTai.TongGuo)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "学生信息未审核通过,id=" + id });
                }
                using (var tran = new TransactionScope())//事务开启
                {
                    try
                    {
                        stuSvc.UpdateClass(id, classId);
                        stuSvc.AddGoUpRecord(id, stu.ClassId, classId, changeTime);
                        long adminId = (long)AdminHelper.GetUserId(HttpContext);
                        logSvc.AddNew(adminId, "调整幼儿班级，studentId：" + id);
                        tran.Complete();

                    }
                    catch (Exception ex)
                    {
                        return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                    }
                }
            }
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        public ActionResult FenBan(long id)
        {
            var classes = dataSvc.GetByName("ClassType");
            ClassUpViewModel model = new ClassUpViewModel();
            model.StudentId = id;
            model.Classes = classes;
            return View(model);
        }
        [HttpPost]
        public ActionResult FenBan(long id, long classId, DateTime changeDateTime)
        {
            var stu = stuSvc.GetById(id);
            if (stu == null)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "不存在的学生信息,Id=" + id });
            }
            if (stu.Status != ShenHeZhuangTai.TongGuo)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "学生信息未审核通过,Id=" + id });
            }
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    stuSvc.UpdateClass(id, classId);
                    stuSvc.AddGoUpRecord(id, stu.ClassId, classId, changeDateTime);
                    long adminId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(adminId, "调整幼儿班级，studentId：" + id);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch (Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }
        }
        [HttpGet]
        public ActionResult Record(long id)
        {
            var records = stuSvc.GetGoUpRecords(id);

            return View(records);
        }
    }
}