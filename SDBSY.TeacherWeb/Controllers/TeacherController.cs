using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SDBSY.Common;
using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.TeacherWeb.App_Start;
using SDBSY.TeacherWeb.Models;

namespace SDBSY.TeacherWeb.Controllers
{
    [CheckLogin]
    public class TeacherController : Controller
    {
        public ITeacherService teacherSvc { get; set; }
        public IDataDictionaryService dataSvc { get; set; }
        // GET: Teacher
        public ActionResult Index()
        {
            var id = (long) AdminHelper.GetUserId(HttpContext);
            var teacher = teacherSvc.GetByAdminId(id);
            return teacher == null ? RedirectToAction("Add") : RedirectToAction("Edit");
        }

        [HttpGet]
        public ActionResult Add()
        {
            var adminUserId= (long)AdminHelper.GetUserId(HttpContext);
            var idCardTypes = dataSvc.GetByName("AdultIdCardType");
            var highestEducation = dataSvc.GetByName("HighestEducation");
            var teacherType = dataSvc.GetByName("TeacherType");
            var postType = dataSvc.GetByName("PostType");
            var zhiCheng = dataSvc.GetByName("ZhiCheng");
            var employType = dataSvc.GetByName("EmployType");
            var teacherCardType = dataSvc.GetByName("TeacherCardType");
            var model = new TeacherAddViewModel()
            {
                AdminUserId = adminUserId,
                //证件类型
                IdCardTypes = JsonConvert.SerializeObject(idCardTypes,Formatting.Indented,new JsonSerializerSettings(){ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()}),
                //最高学历
                HighestEducation= JsonConvert.SerializeObject(highestEducation, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                //职工类别
                TeacherType = JsonConvert.SerializeObject(teacherType, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                //任职岗位
                PostType = JsonConvert.SerializeObject(postType, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                //职称
                ZhiCheng = JsonConvert.SerializeObject(zhiCheng, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                //教师类型
                EmployType = JsonConvert.SerializeObject(employType, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                //教师资格证件种类
                TeacherCardType = JsonConvert.SerializeObject(teacherCardType, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() })
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(TeacherAddPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult() { Status = "error",ErrorMsg = MVCHelper.GetValidMsg(ModelState)});
            }

            var dto = Todto(model);
            teacherSvc.AddNew(dto);
            return Json(new AjaxResult() {Status = "ok"});
        }

        private TeacherAddNewDTO Todto(TeacherAddPostModel model)
        {
            var dto = new TeacherAddNewDTO();
            dto.Name = model.Name;
            dto.AdminUserId = model.AdminUserId;
            dto.BasePay = model.BasePay;
            dto.BirthDay = model.BirthDay;
            dto.ComeDate = model.ComeDate;
            dto.EmployTypeId = model.EmployTypeId;
            dto.Gender = model.Gender;
            dto.HasTeacherCard = model.HasTeacherCard;
            dto.HighestEducationId = model.HighestEducationId;
            dto.HomePlace = model.HomePlace;
            dto.IdCardNum = model.IdCardNum;
            dto.IdCardTypeId = model.IdCardTypeId;
            dto.IsGongJiJin = model.IsGongJiJin;
            dto.IsGongShang = model.IsGongShang;
            dto.IsShengYu = model.IsShengYu;
            dto.IsShiYe = model.IsShiYe;
            dto.IsYangLao = model.IsYangLao;
            dto.IsYiLiao = model.IsYiLiao;
            dto.IsPartyMember = model.IsPartyMember;
            dto.IsLeader = model.IsLeader;
            dto.LeaderAuthUnit = model.LeaderAuthUnit;
            dto.IsPreschoolEducation = model.IsPreschoolEducation;
            dto.PostTypeId = model.PostTypeId;
            dto.SchoolName = model.SchoolName;
            dto.TeacherTypeId = model.TeacherTypeId;
            dto.TeacherCardTypeId = model.TeacherCardTypeId;
            dto.TeacherCardNum = model.TeacherCardNum;
            dto.TeacherCardAwardUnit = model.TeacherCardAwardUnit;
            dto.TeacherCardAwardTime = model.TeacherCardAwardTime;
            dto.TelPhone = model.TelPhone;
            dto.WorkStartTime = model.WorkStartTime;
            dto.WorkEndTime = model.WorkEndTime;
            dto.ZhiChengId = model.ZhiChengId;
            return dto;
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var id = (long)AdminHelper.GetUserId(HttpContext);
            var teacher = teacherSvc.GetByAdminId(id);
            var idCardTypes = dataSvc.GetByName("AdultIdCardType");
            var highestEducation = dataSvc.GetByName("HighestEducation");
            var teacherType = dataSvc.GetByName("TeacherType");
            var postType = dataSvc.GetByName("PostType");
            var zhiCheng = dataSvc.GetByName("ZhiCheng");
            var employType = dataSvc.GetByName("EmployType");
            var teacherCardType = dataSvc.GetByName("TeacherCardType");
            var model = new TeacherEditViewModel()
            {
                Teacher = teacher,
                //证件类型
                IdCardTypes = JsonConvert.SerializeObject(idCardTypes, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                //最高学历
                HighestEducation = JsonConvert.SerializeObject(highestEducation, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                //职工类别
                TeacherType = JsonConvert.SerializeObject(teacherType, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                //任职岗位
                PostType = JsonConvert.SerializeObject(postType, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                //职称
                ZhiCheng = JsonConvert.SerializeObject(zhiCheng, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                //教师类型
                EmployType = JsonConvert.SerializeObject(employType, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }),
                //教师资格证件种类
                TeacherCardType = JsonConvert.SerializeObject(teacherCardType, Formatting.Indented, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() })
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TeacherEditPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult() { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }

            var dto = Todto(model);
            teacherSvc.Update(dto);
            return Json(new AjaxResult() { Status = "ok"});
        }

        private TeacherEditDTO Todto(TeacherEditPostModel model)
        {
            var dto = new TeacherEditDTO();
            dto.Name = model.Name;
            dto.Id = model.Id;
            dto.BasePay = model.BasePay;
            dto.BirthDay = model.BirthDay;
            dto.ComeDate = model.ComeDate;
            dto.EmployTypeId = model.EmployTypeId;
            dto.Gender = model.Gender;
            dto.HasTeacherCard = model.HasTeacherCard;
            dto.HighestEducationId = model.HighestEducationId;
            dto.HomePlace = model.HomePlace;
            dto.IdCardNum = model.IdCardNum;
            dto.IdCardTypeId = model.IdCardTypeId;
            dto.IsGongJiJin = model.IsGongJiJin;
            dto.IsGongShang = model.IsGongShang;
            dto.IsShengYu = model.IsShengYu;
            dto.IsShiYe = model.IsShiYe;
            dto.IsYangLao = model.IsYangLao;
            dto.IsYiLiao = model.IsYiLiao;
            dto.IsPartyMember = model.IsPartyMember;
            dto.IsLeader = model.IsLeader;
            dto.LeaderAuthUnit = model.LeaderAuthUnit;
            dto.IsPreschoolEducation = model.IsPreschoolEducation;
            dto.PostTypeId = model.PostTypeId;
            dto.SchoolName = model.SchoolName;
            dto.TeacherTypeId = model.TeacherTypeId;
            dto.TeacherCardTypeId = model.TeacherCardTypeId;
            dto.TeacherCardNum = model.TeacherCardNum;
            dto.TeacherCardAwardUnit = model.TeacherCardAwardUnit;
            dto.TeacherCardAwardTime = model.TeacherCardAwardTime;
            dto.TelPhone = model.TelPhone;
            dto.WorkStartTime = model.WorkStartTime;
            dto.WorkEndTime = model.WorkEndTime;
            dto.ZhiChengId = model.ZhiChengId;
            return dto;
        }
        
    }
}