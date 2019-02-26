using SDBSY.Common;
using SDBSY.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using SDBSY.UserWeb.App_Start;
using SDBSY.DTO;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Drawing;
using SDBSY.UserWeb.Models;
using System.Transactions;

namespace SDBSY.UserWeb.Controllers
{
    public class HomeController : Controller
    {
        public static readonly string serverUrl = WebConfigurationManager.AppSettings["fileserver"];
        public IUserService userSvc { get; set; }
        public IDataDictionaryService dataSvc { get; set; }
        public IPlaceService placeSvc { get; set; }
        public INewPlaceService newplaceSvc { get; set; }
        public INationService nationSvc { get; set; }
        public ICountryService couSvc { get; set; }
        public IStudentService stuSvc { get; set; }
        public IParentService pareSvc { get; set; }
        public IGuardianService guaSvc { get; set; }
        public ISystemSettingService settingSvc { get; set; }
        //public IUserService userSvc;
        //public IDataDictionaryService dataSvc;
        //public IPlaceService placeSvc;
        //public INationService nationSvc;
        //public ICountryService couSvc;
        //public IStudentService stuSvc;
        //public IParentService pareSvc;
        //public IGuardianService guaSvc;
        //public ISystemSettingService settingSvc;

        //public HomeController(IUserService userService, IDataDictionaryService dataService, IPlaceService placeService,
        //    INationService nationService, ICountryService couService, IStudentService stuService,
        //    IParentService pareService,
        //    IGuardianService guaService, ISystemSettingService settingService)
        //{
        //    userSvc = userService;
        //    dataSvc = dataService;
        //    placeSvc = placeService;
        //    nationSvc = nationService;
        //    couSvc = couService;
        //    stuSvc = stuService;
        //    pareSvc = pareService;
        //    guaSvc = guaService;
        //    settingSvc = settingService;
        //}
        // GET: Mobile
        [HttpGet]
        [CheckSystem]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [CheckSystem]
        public ActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        [CheckSystem]
        public ActionResult Signin(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            string loginChatcha = (string)TempData["Captcha"];
            if (string.IsNullOrEmpty(loginChatcha) || loginChatcha != model.LoginCaptcha)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误，请重试。" });
            }
            else
            {
                bool b = userSvc.CheckLogin(model.PhoneNum, model.Pwd);
                var user = userSvc.GetByPhoneNum(model.PhoneNum);
                
                if (!b)//登陆失败
                {
                    if (user == null)//用户名不存在
                    {
                        return Json(new AjaxResult { Status = "error", ErrorMsg = "用户名或密码错误。" });
                    }
                    //用户名存在
                    userSvc.MarkLoginError(user.Id);//标记登录错误
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "用户名或密码错误。" });
                }
                if (userSvc.IsLocked(user.Id))
                {
                    TimeSpan? timeSpan = TimeSpan.FromMinutes(30) - (DateTime.Now - user.LastLoginErrorDateTime);
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "账号已被锁定，请" + (int)timeSpan.Value.TotalMinutes + "分钟后再试。" });
                }
                userSvc.ResetLoginError(user.Id);//清除登录错误
                Session["LoginUserId"] = userSvc.GetByPhoneNum(model.PhoneNum).Id;
                Session["UserRole"] = "user";
                return Json(new AjaxResult { Status = "ok" });
            }

        }
        [HttpGet]
        [CheckSystem]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [CheckSystem]
        public ActionResult Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            if (!CheckPhoneNum(model.PhoneNum))//手机号查重
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "手机号已被占用" });
            }
            string regChatcha = (string)TempData["Captcha"];
            if (string.IsNullOrEmpty(regChatcha) || regChatcha != model.RegCaptcha)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误，请重试。" });
            }
            else
            {
                long id = userSvc.AddNew(model.PhoneNum, model.Pwd);
                return Json(new AjaxResult { Status = "ok" });

            }
        }
        /// <summary>
        /// 注册手机号查重
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public bool CheckPhoneNum(string phoneNum)
        {
            var user = userSvc.GetByPhoneNum(phoneNum);
            return user == null;
        }
        public void CreateCaptcha()
        {
            string captcha = CommonHelper.CreateVerifyCode(4);
            //验证码使用一次就应该销毁，避免恶意
            TempData["Captcha"] = captcha;//验证码
            CommonHelper.CreateImage(captcha);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return Redirect("/Home/Index");
        }
        [CheckPermission("user")]
        [CheckSystem]
        public ActionResult Main()
        {
            //增加了权限检查，走到这里肯定已经登录
            long userId = (long)UserHelper.GetUserId(HttpContext);
            var user = userSvc.GetById(userId);
            var phone = user.PhoneNum;
            var substring = phone.Substring(3, 4);
            user.PhoneNum = phone.Replace(substring, "****");
            return View(user);
        }
        [CheckPermission("user")]
        [CheckSystem]
        public ActionResult ChildList(long id)
        {
            long userId = (long)UserHelper.GetUserId(HttpContext);
            if(userId!=id)
            {
                return View("BaseView", (object)"您无权查看其他用户信息");
            }
            var children = stuSvc.GetByUserId(id);
            if (children.Count() <= 0)
            {
                return View("BaseView", (object)"暂未添加幼儿信息");
            }
            return View(children);
        }
        [HttpGet]
        [CheckSystem]
        [CheckPermission("user")]
        public ActionResult AddInfo()
        {
            var chooseClass = settingSvc.GetVal("ChooseClass");
            long userId = (long)UserHelper.GetUserId(HttpContext);
            var classes = dataSvc.GetByName("ClassType");
            var idCardTypes = dataSvc.GetByName("ChildIdCardType");//幼儿证件类型
            var bloodTypes = dataSvc.GetByName("BloodType");//血型
            var countries = couSvc.GetAll();//国籍
            var nations = nationSvc.GetAll().ToList();//民族
            nations.Insert(0, new NationDTO { Name = "请选择" });

            var identities = dataSvc.GetByName("Identity");//港澳台侨外
            //var places = placeSvc.GetAll().ToList();//旧地市代码
            var places = newplaceSvc.GetByParent(0).ToList();//省
            places.Clear();
            places.Insert(0, new NewPlaceDTO { Name = "请选择" });

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
            AddNewInfoViewModel model = new AddNewInfoViewModel();
            model.UserId = userId;
            model.IdCardTypes = idCardTypes;
            model.BloodTypes = bloodTypes;
            model.Countrties = countries;
            model.Nations = nations.ToArray();
            model.Identities = identities;
            model.Places = places.ToArray();
            model.HuKouXingZhi = huKouXingZhi.ToArray();
            model.FeiNongHuKouTypes = feiNongHuKouTypes.ToArray();
            model.StudyTypes = studyTypes;
            model.IsStayAtHome = isStayAtHome;
            model.HealthyTypes = healthyTypes;
            model.DisabilityTypes = disabilityTypes.ToArray();
            model.AdultIdCardTypes = adultIdCardTypes;
            model.ShowChooseClass = chooseClass.ToLower() == "on";
            model.Classes = classes;
            return View(model);
        }
        [HttpPost]
        [CheckSystem]
        [CheckPermission("user")]
        public async System.Threading.Tasks.Task<ActionResult> UploadFileAsync(string base64string, string path)
        {
            base64string = base64string.Replace("data:image/png;base64,", "");
            byte[] bt = Convert.FromBase64String(base64string);


            Guid guid = Guid.NewGuid();
            using (HttpClient httpClient = new HttpClient())
            using (MemoryStream memoryStream = new MemoryStream(bt))
            {
                //Bitmap bitmap = new Bitmap(memoryStream);
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Headers.Add("uid", "sdbsy");//增加账号密码，防止恶意上传
                content.Headers.Add("pwd", "ysbds");
                content.Headers.Add("path", path);
                content.Add(new StreamContent(memoryStream), "file", guid.ToString() + ".png");
                var respMsg = await httpClient.PostAsync(serverUrl + "/Home/FileUpload/", content);
                string msgBody = await respMsg.Content.ReadAsStringAsync();

                if (respMsg.StatusCode == HttpStatusCode.OK)
                {
                    //请求成功
                    var result = (AjaxResult)Newtonsoft.Json.JsonConvert.DeserializeObject(msgBody, typeof(AjaxResult));
                    result.Data = serverUrl + result.Data;
                    return Json(result);
                }
                else
                {
                    return Json(new AjaxResult { Status = "error" });
                }

            }
        }

        #region 照片上传（通过十六进制同步文件上传）
        /// <summary>
        /// 照片上传
        /// </summary>
        /// <param name="base64string"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckSystem]
        public ActionResult UploadPhoto(string base64string)
        {
            //base64string = base64string.Substring(21);
            base64string = base64string.Replace("data:image/png;base64,", "");
            byte[] bt = Convert.FromBase64String(base64string);
            MemoryStream stream = new MemoryStream(bt);
            Bitmap bitmap = new Bitmap(stream);
            Guid guid = Guid.NewGuid();
            string thumbPath = "/Upload/Images/" + DateTime.Now.ToString("yyyyMMdd") + "/" + guid.ToString() + ".png";
            string thumbFullPath = HttpContext.Server.MapPath("~" + thumbPath);//获取全路径
            //string thumbFullPath = "http://localhost:8008"+thumbPath;
            new FileInfo(thumbFullPath).Directory.Create();//尝试创建可能不存在的文件夹，如果存在也不会报错
            bitmap.Save(thumbFullPath, System.Drawing.Imaging.ImageFormat.Png);
            stream.Close();
            return Json(new AjaxResult { Status = "ok", Data = thumbPath });
        }
        #endregion
        [HttpPost]
        [CheckSystem]
        [CheckPermission("user")]
        public ActionResult AddInfo(AddNewInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }

            //联动判断
            if (model.IdCardTypeId != 7)
            {
                if (string.IsNullOrEmpty(model.IdCardNum))
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "幼儿证件号码必填。" });
                }
                else
                {
                    if (!CheckStuIdCardNumExsits(model.IdCardNum))
                    {
                        return Json(new AjaxResult { Status = "error", ErrorMsg = "幼儿证件号码已经被使用。" });
                    }
                    bool b = CommonHelper.CheckIDCard18(model.IdCardNum);
                    if (!b)
                    {
                        return Json(new AjaxResult { Status = "error", ErrorMsg = "幼儿证件号码验证错误。" });
                    }
                }
            }
            //中国国籍 ，民族必填，户口性质必填
            if (model.CountryId == 34154)//中国
            {
                if (model.NationId == null || model.NationId.Value <= 0)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "中国国籍必须选择民族。" });
                }
                if (model.HuKouXingZhiId == null || model.HuKouXingZhiId <= 0)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "中国国籍必须选择户口性质。" });
                }
                else
                {
                    if (model.HuKouXingZhiId == 38)//非农业户口
                    {
                        if (model.FeiNongHuKouTypeId == null || model.FeiNongHuKouTypeId <= 0)
                        {
                            return Json(new AjaxResult { Status = "error", ErrorMsg = "非农业户口必须选择类型。" });
                        }
                    }
                }
            }
            if (model.IsDisability)
            {
                if (model.DisabilityTypeId == null || model.DisabilityTypeId.Value <= 0)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "残疾幼儿必须选择残疾类型。" });
                }
            }
            long? fatherId = null;
            long? motherId = null;
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    if (!string.IsNullOrEmpty(model.FatherName))
                    {
                        //填写姓名后必须填写单位、手机号、证件号等信息
                        if (string.IsNullOrEmpty(model.FatherWorkUnit) ||
                            string.IsNullOrEmpty(model.FatherPhoneNum) || string.IsNullOrEmpty(model.FatherCardNum))
                        {
                            return Json(new AjaxResult { Status = "error", ErrorMsg = "父亲工作单位、手机号码、证件号码必填。" });
                        }
                        if (model.FatherCardTypeId == 8 && !CheckIdCard(model.FatherCardNum))
                        {
                            return Json(new AjaxResult { Status = "error", ErrorMsg = "父亲证件号码错误。" });
                        }
                        fatherId = AddNewParent(model.FatherName, model.FatherWorkUnit, model.FatherPhoneNum,
                            model.FatherCardTypeId, model.FatherCardNum);
                    }
                    if (!string.IsNullOrEmpty(model.MotherName))
                    {
                        if (string.IsNullOrEmpty(model.MotherWorkUnit) ||
                            string.IsNullOrEmpty(model.MotherPhoneNum) || string.IsNullOrEmpty(model.MotherCardNum))
                        {
                            return Json(new AjaxResult { Status = "error", ErrorMsg = "母亲工作单位、手机号码、证件号码必填。" });
                        }
                        if (model.MotherCardTypeId == 8 && !CheckIdCard(model.MotherCardNum))
                        {
                            return Json(new AjaxResult { Status = "error", ErrorMsg = "母亲证件号码错误。" });
                        }
                        motherId = AddNewParent(model.MotherName, model.MotherWorkUnit, model.MotherPhoneNum,
                            model.MotherCardTypeId, model.MotherCardNum);
                    }
                    if (model.GuardianCardTypeId == 8 && !CheckIdCard(model.GuardianCardNum))
                    {
                        return Json(new AjaxResult { Status = "error", ErrorMsg = "监护人证件号码错误。" });
                    }
                    long guardianId = AddNewGuardian(model.GuardianName, model.GuardianCardTypeId, model.GuardianCardNum);
                    StudentAddNewDTO dto = ToDTO(model, guardianId, fatherId, motherId);
                    stuSvc.AddNew(dto);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch(Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
                
            }
        }
        private StudentAddNewDTO ToDTO(AddNewInfoModel model, long guardianId, long? fatherId, long? motherId)
        {
            StudentAddNewDTO dto = new StudentAddNewDTO();
            dto.PhotoUrl = model.PhotoUrl;
            dto.TijianUrl = model.TijianUrl;
            dto.BankCardNum = model.BankCardNum;
            dto.BirthDate = model.BirthDate;
            var birthYear = dto.BirthDate.Year;
            var birthMonth = dto.BirthDate.Month;
            
            if (birthMonth < 9)
            {
                dto.InKindergartenDate=new DateTime(birthYear+3,9,1);
            }
            else
            {
                dto.InKindergartenDate = new DateTime(birthYear + 4, 9, 1);
            }
            
            dto.BirthPlaceId = model.BirthPlaceId == 0 ? null : model.BirthPlaceId;
            dto.BloodTypeId = model.BloodTypeId;
            dto.ClassId = model.ClassId;
            dto.CountryId = model.CountryId;
            dto.DisabilityTypeId = model.DisabilityTypeId == 0 ? null : model.DisabilityTypeId;
            dto.FatherId = fatherId;
            dto.FeiNongHuKouTypeId = model.FeiNongHuKouTypeId == 0 ? null : model.FeiNongHuKouTypeId;
            dto.Gender = model.Gender;
            dto.GuardianId = guardianId;
            dto.HasInfection = model.HasInfection;
            dto.HealthyTypeId = model.HealthyTypeId;
            dto.HomePlace = model.HomePlace;
            dto.HuKouPlaceId = model.HuKouPlaceId == 0 ? null : model.HuKouPlaceId;
            dto.HuKouXingZhiId = model.HuKouXingZhiId == 0 ? null : model.HuKouXingZhiId;
            dto.IdCardNum = model.IdCardNum;
            dto.IdCardTypeId = model.IdCardTypeId;
            dto.IdentityId = model.IdentityId;
            dto.IsDisability = model.IsDisability;
            dto.IsFollowWorkInCity = model.IsFollowWorkInCity;
            dto.IsOnlyBaby = model.IsOnlyBaby;
            dto.IsOrphan = model.IsOrphan;
            dto.IsStayAtHomeId = model.IsStayAtHomeId;
            dto.MotherId = motherId;
            dto.Name = model.Name;
            dto.NationId = model.NationId == 0 ? null : model.NationId;
            dto.OtherTel = model.OtherTel;
            dto.PriginPlace = model.PriginPlace;
            dto.StudyTypeId = model.StudyTypeId;
            dto.UserId = model.UserId == 0 ? null : model.UserId;
            return dto;
        }
        private long AddNewParent(string name, string workUnit, string phone, long cardTypeId, string cardNum)
        {
            long id = pareSvc.AddNew(name, workUnit, phone, cardTypeId, cardNum);
            if (id == -1)//已存在父母的身份证号
            {
                id = pareSvc.GetByIdCardNum(cardNum);
            }
            return id;
        }
        private long AddNewGuardian(string name, long idCardTypeId, string idCardNum)
        {
            long id = guaSvc.AddNew(name, idCardTypeId, idCardNum);
            if (id == -1)
            {
                id = guaSvc.GetByIdCardNum(idCardNum);
            }
            return id;
        }
        [HttpPost]
        [CheckSystem]

        public ActionResult CheckIdCardNum(string idCardNum)
        {
            if (!CheckIdCard(idCardNum))//合法性验证
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "身份证验证错误，请重新填写" });
            }
            bool isOK = CheckStuIdCardNumExsits(idCardNum);
            return Json(new AjaxResult { Status = isOK ? "ok" : "exists" });
        }
        private bool CheckStuIdCardNumExsits(string idCardNum)
        {
            var stu = stuSvc.GetByIdCardNum(idCardNum);
            return stu == null;
        }
        private bool CheckIdCard(string idCardNum)
        {
            return CommonHelper.CheckIDCard18(idCardNum);
        }
        public ActionResult SystemOff()
        {
            return View();
        }
        [HttpGet]
        [CheckPermission("user")]
        [CheckSystem]
        public ActionResult EditInfo(long id)
        {
            var chooseClass = settingSvc.GetVal("ChooseClass");
            //该幼儿是否是当前登录用户的？
            //增加了权限检查，走到这里肯定已经登录
            long userId = (long)UserHelper.GetUserId(HttpContext);
            var stu = stuSvc.GetById(id);
            if(stu.UserId!=userId)
            {
                return View("BaseView", (object)"您无权查看其他幼儿信息");
            }
            if(stu.Status==ShenHeZhuangTai.BoHui)
            {
                return View("BaseView",(object)"非常抱歉，该幼儿信息未通过审核，请尽快联系其他幼儿园进行报名，如有异议请咨询滨州市实验幼儿园招生办公室。");
            }

            var classes = dataSvc.GetByName("ClassType");
            var idCardTypes = dataSvc.GetByName("ChildIdCardType");//幼儿证件类型
            var bloodTypes = dataSvc.GetByName("BloodType");//血型
            var countries = couSvc.GetAll();//国籍
            var nations = nationSvc.GetAll().ToList();//民族
            nations.Insert(0, new NationDTO { Name = "请选择" });

            var identities = dataSvc.GetByName("Identity");//港澳台侨外
            var places = placeSvc.GetAll().ToList();//地市代码
            places.Insert(0, new PlaceDTO { Name = "请选择" });

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
            model.Places = places.ToArray();
            model.HuKouXingZhi = huKouXingZhi.ToArray();
            model.FeiNongHuKouTypes = feiNongHuKouTypes.ToArray();
            model.StudyTypes = studyTypes;
            model.IsStayAtHome = isStayAtHome;
            model.HealthyTypes = healthyTypes;
            model.DisabilityTypes = disabilityTypes.ToArray();
            model.AdultIdCardTypes = adultIdCardTypes;
            model.ShowChooseClass = chooseClass.ToLower() == "on";
            model.FileServerUrl = serverUrl;
            return View(model);
        }
        [HttpPost]
        public ActionResult EditInfo(StudentEditDTO model)
        {
            var stu = stuSvc.GetById(model.Id);
            if (stu == null)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "不存在的学生信息" });
            }
            //bool b = CommonHelper.CheckIDCard18(model.IdCardNum);
            if (!CheckIdCard(model.IdCardNum))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "幼儿证件号码验证错误。" });
            }
            using (TransactionScope tran = new TransactionScope())//事务开启
            {
                try
                {
                    //1、更新监护人
                    guaSvc.Update(model.GuardianId, model.GuardianName, model.GuardianPhoneNum, model.GuardianCardTypeId, model.GuardianCardNum);
                    //2、更新父母
                    if (model.FatherId != null && model.FatherId.Value > 0)
                    {
                        pareSvc.Update(model.FatherId.Value, model.FatherName, model.FatherWorkUnit, model.FatherPhoneNum, model.FatherCardTypeId.Value, model.FatherCardNum);
                    }
                    if (model.MotherId != null && model.MotherId.Value > 0)
                    {
                        pareSvc.Update(model.MotherId.Value, model.MotherName, model.MotherWorkUnit, model.MotherPhoneNum, model.MotherCardTypeId.Value, model.MotherCardNum);
                    }

                    //3、更新学生
                    stuSvc.UpdateStudent(model);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch(Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }

        }
        [HttpGet]
        [CheckSystem]
        public ActionResult LosePass()
        {
            return View();
        }
        [HttpPost]
        [CheckSystem]
        public ActionResult SendSms(string phoneNum,string imgCaptcha)
        {
            #region 首先验证图形验证码
            string losePassChatcha = (string)TempData["Captcha"];
            if(string.IsNullOrEmpty(losePassChatcha)||imgCaptcha!=losePassChatcha)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "图形验证码错误，请重试。" });
            }
            #endregion
            #region 检查手机号是否存在
            var user = userSvc.GetByPhoneNum(phoneNum);
            if(user==null)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = $"此用户{phoneNum}不存在"
                });
            }
            #endregion
            Random r = new Random();
            string code= r.Next(100000, 999999).ToString();
            //Session保存短信验证码和接受短信的手机号
            TempData["SmsCaptcha"] = code;
            Session["PhoneNum"] = phoneNum;
            
            var result= SendSMS.Send(phoneNum, code);
            if(result.result==0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = result.errMsg});
            }
        }
        [HttpPost]
        [CheckSystem]
        public ActionResult LosePass(string phoneNum,string smsVerifyCode)
        {
            if(phoneNum!=(string)Session["PhoneNum"])
            {
                return Json(new AjaxResult { Status="error",ErrorMsg="请填写接受短信的手机号" });
            }
            if(smsVerifyCode!=(string)TempData["SmsCaptcha"])
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "短信验证码错误" });
            }
            TempData["LosePass_OK"] = true;
            return Json(new AjaxResult { Status="ok"});
        }
        [HttpGet]
        [CheckSystem]
        public ActionResult LosePassTwo()
        {
            return View();
        }
        [HttpPost]
        [CheckSystem]
        public ActionResult LosePassTwo(string pwd)
        {
            string phoneNum = (string)Session["PhoneNum"];
            if(string.IsNullOrEmpty(phoneNum))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "操作超时" });
            }
            var user = userSvc.GetByPhoneNum(phoneNum);
            if (user == null)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = $"此用户{phoneNum}不存在"
                });
            }
            userSvc.UpdatePassword(user.Id, pwd);
            return Json(new AjaxResult { Status="ok"});
        }
        [HttpGet]
        public ActionResult LosePassThree()
        {
            return View();
        }
    }
}