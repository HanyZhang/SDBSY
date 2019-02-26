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
    public class TrainingController : Controller
    {
        public ITeacherService teacherSvc { get; set; }
        public IDataDictionaryService dataSvc { get; set; }
        // GET: Training
        public ActionResult List()
        {
            var id = (long)AdminHelper.GetUserId(HttpContext);
            var teacher = teacherSvc.GetByAdminId(id);
            if (teacher == null)
            {
                return View("Error", (object)"请先添加教师信息");
            }

            var trainings = teacherSvc.GetTrainings(teacher.Id);
            return View(trainings);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var id = (long)AdminHelper.GetUserId(HttpContext);
            var teacher = teacherSvc.GetByAdminId(id);
            var trainingLevels = dataSvc.GetByName("TrainingLevel");
            var trainingTypes = dataSvc.GetByName("TrainingType");
            var model = new TrainingAddViewModel()
            {
                TeacherId = teacher.Id,
                TrainingLevels = JsonConvert.SerializeObject(trainingLevels, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    }),
                TrainingTypes = JsonConvert.SerializeObject(trainingTypes, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    })
            };
            return View(model);
        }

        public ActionResult Add(TrainingAddPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult() { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }

            teacherSvc.AddNew(Todto(model));
            return Json(new AjaxResult {Status = "ok"});
        }
        private TrainingAddNewDTO Todto(TrainingAddPostModel model)
        {
            var dto = new TrainingAddNewDTO()
            {
                Year = model.Year,
                UnitName = model.UnitName,
                TrainingContent = model.TrainingContent,
                TrainingLevelId = model.TrainingLevelId,
                TrainingTime = model.TrainingTime,
                TrainingTypeId = model.TrainingTypeId,
                TeacherId = model.TeacherId
            };
            return dto;
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var training = teacherSvc.GetTraining(id);
            var trainingLevels = dataSvc.GetByName("TrainingLevel");
            var trainingTypes = dataSvc.GetByName("TrainingType");
            var model=new TrainingEditViewModel()
            {
                Training = training,
                TrainingLevels = JsonConvert.SerializeObject(trainingLevels, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    }),
                TrainingTypes = JsonConvert.SerializeObject(trainingTypes, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    })
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TrainingEditPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult() {Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState)});
            }
            teacherSvc.Update(Todto(model));
            return Json(new AjaxResult() {Status = "ok"});
        }

        private TrainingEditDTO Todto(TrainingEditPostModel model)
        {
            var dto=new TrainingEditDTO()
            {
                Year = model.Year,
                UnitName = model.UnitName,
                TrainingContent = model.TrainingContent,
                TrainingLevelId = model.TrainingLevelId,
                TrainingTime = model.TrainingTime,
                TrainingTypeId = model.TrainingTypeId,
                Id = model.Id
            };
            return dto;
        }
        
        [HttpPost]
        public ActionResult Del(long id)
        {
            teacherSvc.DelTraining(id);
            return Json(new AjaxResult() {Status = "ok"});
        }
    }
}