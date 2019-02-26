using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace SDBSY.Service
{
    public class TeacherService : ITeacherService
    {
        public long AddNew(TeacherAddNewDTO dto)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                //身份证号查重
                BaseService<TeacherEntity> bs = new BaseService<TeacherEntity>(mc);
                bool exsits= bs.GetAll().Any(t => t.IdCardNum == dto.IdCardNum);
                if(exsits)
                {
                    return -1;//已存在相同的身份证号
                }
                TeacherEntity entity = new TeacherEntity();

                entity.AdminUserId = dto.AdminUserId;
                entity.BasePay = dto.BasePay;
                entity.BirthDay = dto.BirthDay;
                entity.ComeDate = dto.ComeDate;
                entity.EmployTypeId = dto.EmployTypeId;
                entity.Gender = dto.Gender;
                entity.HomePlace = dto.HomePlace;
                entity.IdCardNum = dto.IdCardNum;
                entity.IdCardTypeId = dto.IdCardTypeId;
                entity.IsPartyMember = dto.IsPartyMember;
                entity.Name = dto.Name;
                entity.TelPhone = dto.TelPhone;
                entity.HasTeacherCard = dto.HasTeacherCard;
                entity.SchoolName = dto.SchoolName;
                entity.ZhiChengId = dto.ZhiChengId;
                entity.WorkStartTime = dto.WorkStartTime;
                entity.WorkEndTime = dto.WorkEndTime;
                entity.TeacherTypeId = dto.TeacherTypeId;
                entity.TeacherCardTypeId = dto.TeacherCardTypeId;
                entity.TeacherCardNum = dto.TeacherCardNum;
                entity.TeacherCardAwardUnit = dto.TeacherCardAwardUnit;
                entity.TeacherCardAwardTime = dto.TeacherCardAwardTime;
                entity.PostTypeId = dto.PostTypeId;
                entity.IsYangLao = dto.IsYangLao;
                entity.IsGongShang = dto.IsGongShang;
                entity.IsYiLiao = dto.IsYiLiao;
                entity.IsShengYu = dto.IsShengYu;
                entity.IsShiYe = dto.IsShiYe;
                entity.IsGongJiJin = dto.IsGongJiJin;
                entity.IsLeader = dto.IsLeader;
                entity.IsPreschoolEducation = dto.IsPreschoolEducation;
                entity.HighestEducationId = dto.HighestEducationId;
                mc.Teachers.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }
        private TeacherDTO ToDTO (TeacherEntity entity)
        {
            TeacherDTO dto = new TeacherDTO
            {
                AdminUserId = entity.AdminUserId,
                //AdminUserName = entity.AdminUser.UserName,
                BirthDay = entity.BirthDay,
                BasePay = entity.BasePay,
                BasePayStr =entity.BasePay==null?"":entity.BasePay.Value.ToString("yyyyMMdd"),
                ComeDate = entity.ComeDate,
                ComeDateStr = entity.ComeDate.ToString("yyyyMMdd"),
                CreateDateTime = entity.CreateDateTime,
                Gender = entity.Gender,
                GenderName = entity.Gender?"女":"男",
                HomePlace=entity.HomePlace,
                //HuKouPlace=entity.HuKouPlace,
                Id=entity.Id,
                IdCardNum=entity.IdCardNum,
                IdCardTypeId=entity.IdCardTypeId,
                IdCardTypeName=entity.IdCardType.Value,
                //IsOfficial=entity.IsOfficial,
                //IsOfficialStr=entity.IsOfficial?"正式教师编制":"非正式教师编制",
                IsPartyMember=entity.IsPartyMember,
                IsPartyMemberStr=entity.IsPartyMember?"中共党员":"非中共党员",
                Name=entity.Name,
                //PriginPlace=entity.PriginPlace,
                TelPhone=entity.TelPhone,
                TeacherTypeId = entity.TeacherTypeId,
                TeacherTypeName = entity.TeacherType.Value,
                PostTypeId = entity.PostTypeId,
                PostTypeName = entity.PostType.Value,
                IsLeader = entity.IsLeader,
                IsLeaderStr = entity.IsLeader?"是":"否",
                LeaderAuthUnit = entity.LeaderAuthUnit,
                IsPreschoolEducation = entity.IsPreschoolEducation,
                IsPreschoolEducationStr = entity.IsPreschoolEducation?"是":"否",
                ZhiChengId = entity.ZhiChengId,
                ZhiChengName = entity.ZhiCheng.Value,
                EmployTypeId = entity.EmployTypeId,
                EmployTypeName = entity.EmployType.Value,
                IsYangLao = entity.IsYangLao,
                IsYangLaoStr = entity.IsYangLao?"是":"",
                IsYiLiao = entity.IsYiLiao,
                IsYiLiaoStr = entity.IsYiLiao ? "是" : "",
                IsGongShang = entity.IsGongShang,
                IsGongShangStr = entity.IsGongShang ? "是" : "",
                IsShengYu = entity.IsShengYu,
                IsShengYuStr = entity.IsShengYu ? "是" : "",
                IsShiYe = entity.IsShiYe,
                IsShiYeStr = entity.IsShiYe ? "是" : "",
                IsGongJiJin = entity.IsGongJiJin,
                IsGongJiJinStr = entity.IsGongJiJin ? "是" : "",
                WeiGouMai = !entity.IsYangLao ? "是" : "",
                WorkStartTime = entity.WorkStartTime,
                WorkStartTimeStr = entity.WorkStartTime==null?"":entity.WorkStartTime.Value.ToString("yyyyMMdd"),
                WorkEndTime = entity.WorkEndTime,
                WorkEndTimeStr = entity.WorkEndTime==null?"":entity.WorkEndTime.Value.ToString("yyyyMMdd"),
                HasTeacherCard = entity.HasTeacherCard,
                HasTeacherCardStr = entity.HasTeacherCard?"是":"否",
                TeacherCardTypeId = entity.TeacherCardTypeId,
                TeacherCardTypeName = entity.TeacherCardType==null?string.Empty : entity.TeacherCardType.Value,
                TeacherCardNum = entity.TeacherCardNum,
                TeacherCardAwardUnit = entity.TeacherCardAwardUnit,
                TeacherCardAwardTime = entity.TeacherCardAwardTime,
                TeacherCardAwardTimeStr = entity.TeacherCardAwardTime==null?"":entity.TeacherCardAwardTime.Value.ToString("yyyyMMdd"),
                SchoolName = entity.SchoolName,
                HighestEducationId = entity.HighestEducationId,
                HighestEducationName = entity.HighestEducation.Value
            };
            return dto;
        }
        public TeacherDTO[] GetAll()
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<TeacherEntity> bs = new BaseService<TeacherEntity>(mc);
                var teachers= bs.GetAll().Include(t => t.HighestEducation)
                    .Include(t => t.TeacherCardType)
                    .Include(t => t.AdminUser).Include(t => t.EmployType)
                    .Include(t => t.IdCardType).Include(t => t.PostType)
                    .Include(t => t.TeacherType).Include(t => t.ZhiCheng).ToList();
                List<TeacherDTO> list = new List<TeacherDTO>();
                if (teachers != null && teachers.Count() > 0)
                {
                    foreach (var item in teachers)
                    {
                        list.Add(ToDTO(item));
                    }
                }
                return list.ToArray();
            }
        }

        public TeacherDTO GetById(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<TeacherEntity> bs = new BaseService<TeacherEntity>(mc);
                var teac= bs.GetAll().Include(t => t.HighestEducation).Include(t => t.TeacherCardType)
                    .Include(t => t.AdminUser).Include(t => t.EmployType).Include(t => t.IdCardType).Include(t => t.PostType)
                    .Include(t => t.TeacherType).Include(t => t.ZhiCheng).SingleOrDefault(t=>t.Id==id);
                if(teac==null)
                {
                    return null;
                }
                else
                {
                    return ToDTO(teac);
                }
            }
        }
        

        public void Update(TeacherEditDTO dto)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<TeacherEntity> bs = new BaseService<TeacherEntity>(mc);
                var entity= bs.GetById(dto.Id);
                if(entity==null)
                {
                    throw new ArgumentException("不存在的教师信息：id="+dto.Id);
                }
                entity.BasePay = dto.BasePay;
                entity.BirthDay = dto.BirthDay;
                entity.ComeDate = dto.ComeDate;
                entity.EmployTypeId = dto.EmployTypeId;
                entity.Gender = dto.Gender;
                entity.HomePlace = dto.HomePlace;
                entity.IdCardNum = dto.IdCardNum;
                entity.IdCardTypeId = dto.IdCardTypeId;
                entity.IsPartyMember = dto.IsPartyMember;
                entity.Name = dto.Name;
                entity.TelPhone = dto.TelPhone;
                entity.HasTeacherCard = dto.HasTeacherCard;
                entity.SchoolName = dto.SchoolName;
                entity.ZhiChengId = dto.ZhiChengId;
                entity.WorkStartTime = dto.WorkStartTime;
                entity.WorkEndTime = dto.WorkEndTime;
                entity.TeacherTypeId = dto.TeacherTypeId;
                entity.TeacherCardTypeId = dto.TeacherCardTypeId;
                entity.TeacherCardNum = dto.TeacherCardNum;
                entity.TeacherCardAwardUnit = dto.TeacherCardAwardUnit;
                entity.TeacherCardAwardTime = dto.TeacherCardAwardTime;
                entity.PostTypeId = dto.PostTypeId;
                entity.IsYangLao = dto.IsYangLao;
                entity.IsGongShang = dto.IsGongShang;
                entity.IsYiLiao = dto.IsYiLiao;
                entity.IsShengYu = dto.IsShengYu;
                entity.IsShiYe = dto.IsShiYe;
                entity.IsGongJiJin = dto.IsGongJiJin;
                entity.IsLeader = dto.IsLeader;
                entity.IsPreschoolEducation = dto.IsPreschoolEducation;
                mc.SaveChanges();
            }
        }

        public EducateDTO[] GetEducates(long teacherId)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<EducateEntity> bs = new BaseService<EducateEntity>(mc);
                var educates= bs.GetAll().Where(t => t.TeacherId == teacherId).Include(t => t.Teacher).AsNoTracking().ToList();
                List<EducateDTO> list = new List<EducateDTO>();
                foreach(var item in educates)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }
        private EducateDTO ToDTO(EducateEntity entity)
        {
            EducateDTO dto = new EducateDTO() {
                Id = entity.Id,
                CreateDateTime = entity.CreateDateTime,
                TeacherId = entity.TeacherId,
                
                StartTime=entity.StartTime,
                EndTime=entity.EndTime,
                SchoolName=entity.SchoolName,
                Type=entity.Type
            };
            switch (entity.Type) {
                case 1:
                    dto.TypeName = "学历教育";
                    break;
                case 2:
                    dto.TypeName = "在职培训";
                    break;
                case 3:
                    dto.TypeName = "其他";
                    break;
                default:
                    dto.TypeName = "未知";
                    break;
            }
            return dto;
        }
        public EducateDTO GetByEducateId(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<EducateEntity> bs = new BaseService<EducateEntity>(mc);
                var educate= bs.GetAll().Include(t => t.Teacher).Single(t => t.Id == id);
                return educate == null ? null : ToDTO(educate);
            }
        }

        public long AddNewEducate(EducateDTO dto)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                EducateEntity entity = new EducateEntity() {
                    TeacherId=dto.TeacherId,
                    StartTime=dto.StartTime,
                    EndTime=dto.EndTime,
                    SchoolName=dto.SchoolName,
                    Type=dto.Type,
                };
                mc.Educates.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        public WorkDTO[] GetWorks(long teacherId)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<WorkEntity> bs = new BaseService<WorkEntity>(mc);
                var educates = bs.GetAll().Where(t => t.TeacherId == teacherId).Include(t => t.Teacher).AsNoTracking().ToList();
                List<WorkDTO> list = new List<WorkDTO>();
                foreach (var item in educates)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }
        private WorkDTO ToDTO(WorkEntity entity)
        {
            WorkDTO dto = new WorkDTO() {
                TeacherId = entity.TeacherId,
                TeacherName=entity.Teacher.Name,
                CreateDateTime=entity.CreateDateTime,
                Id=entity.Id,
                EndTime=entity.EndTime,
                StartTime=entity.StartTime,
                JobName=entity.JobName,
                UnitName=entity.UnitName,
            };
            return dto;
        }
        public WorkDTO GetByWorkId(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<WorkEntity> bs = new BaseService<WorkEntity>(mc);
                var item= bs.GetAll().Include(t => t.Teacher).Single(t => t.Id == id);
                return item == null ? null : ToDTO(item);
            }
        }

        public long AddNewWork(WorkDTO dto)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                WorkEntity entity = new WorkEntity() {
                    TeacherId = dto.TeacherId,
                    StartTime = dto.StartTime,
                    EndTime=dto.EndTime,
                    UnitName=dto.UnitName,
                    JobName=dto.JobName,
                };
                mc.Works.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        //public CertificateDTO[] GetEducateCertificates(long educateId)
        //{
        //    using (MyDBContext mc = new MyDBContext())
        //    {
        //        BaseService<CertificateEntity> bs = new BaseService<CertificateEntity>(mc);
        //        return bs.GetAll().AsNoTracking().Where(t => t.EducateId == educateId).Select(t => new CertificateDTO {
        //            EducateId=t.EducateId,
        //            Id=t.Id,
        //            CreateDateTime=t.CreateDateTime,
        //            Url=t.Url,
        //            ThumbUrl=t.ThumbUrl
        //        }).ToArray();
        //    }
        //}

        public long AddNew(CertificateAddNewDTO dto)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                CertificateEntity entity = new CertificateEntity() {
                    TeacherId = dto.TeacherId,
                    //Url=dto.Url,
                    //ThumbUrl=dto.ThumbUrl,
                    Name = dto.Name,
                    AwardDateTime = dto.AwardDateTime,
                    AwardUnit = dto.AwardUnit,
                    Number = dto.Number
                };
                mc.Certificates.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        public void DelTeacher(long teacherId)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<TeacherEntity> bs = new BaseService<TeacherEntity>(mc);
                bs.MarkDeleted(teacherId);
            }
        }

        public void DelEducate(long educateId)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<EducateEntity> bs = new BaseService<EducateEntity>(mc);
                bs.MarkDeleted(educateId);
            }
        }

        public void DelWork(long workId)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<WorkEntity> bs = new BaseService<WorkEntity>(mc);
                bs.MarkDeleted(workId);
            }
        }

        public void DelCertificate(long certificateId)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<CertificateEntity> bs = new BaseService<CertificateEntity>(mc);
                bs.MarkDeleted(certificateId);
            }
        }

        public void UpdateEducate(long id, string schoolName,int type, DateTime startTime, DateTime endTime)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<EducateEntity> bs = new BaseService<EducateEntity>(mc);
                var edu= bs.GetById(id);
                if(edu==null)
                {
                    throw new ArgumentException("不存在的教育经历，id="+id);
                }
                edu.SchoolName = schoolName;
                edu.Type = type;
                edu.StartTime = startTime;
                edu.EndTime = endTime;
                mc.SaveChanges();
            }
        }

        public void UpdateWork(long id, string unitName, string jobName, DateTime startTime, DateTime endTime)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<WorkEntity> bs = new BaseService<WorkEntity>(mc);
                var work = bs.GetById(id);
                if (work == null)
                {
                    throw new ArgumentException("不存在的工作经历，id=" + id);
                }
                work.UnitName = unitName;
                work.JobName = jobName;
                work.StartTime = startTime;
                work.EndTime = endTime;
                mc.SaveChanges();
            }
        }

        public TeacherDTO GetByAdminId(long adminId)
        {
            using (var mc = new MyDBContext())
            {
                var bs=new BaseService<TeacherEntity>(mc);
                var teachers= bs.GetAll();
                var teacher=teachers.Include(t=>t.HighestEducation)
                    .Include(t=>t.TeacherCardType)
                    .Include(t=>t.AdminUser).Include(t=>t.EmployType)
                    .Include(t=>t.IdCardType).Include(t=>t.PostType)
                    .Include(t=>t.TeacherType).Include(t=>t.ZhiCheng)
                    .SingleOrDefault(t => t.AdminUserId == adminId);
                return teacher == null ? null : ToDTO(teacher);
            }
        }

        public CertificateDTO GetCertificate(long id)
        {
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<CertificateEntity>(mc);
                var cer = bs.GetAll().Include(t => t.Teacher).SingleOrDefault(t => t.Id == id);

                return cer == null ? null : ToDTO(cer);
            }
        }

        public CertificateDTO[] GetCertificates(long teacherId)
        {
            using (var mc=new MyDBContext())
            {
                var bs=new BaseService<CertificateEntity>(mc);
                var cers= bs.GetAll().Include(t => t.Teacher).Where(t => t.TeacherId == teacherId).ToList();
                var list=new List<CertificateDTO>();
                foreach (var cer in cers)
                {
                    list.Add(ToDTO(cer));
                }

                return list.ToArray();
            }
        }

        private CertificateDTO ToDTO(CertificateEntity entity)
        {
            var dto=new CertificateDTO();
            dto.Id = entity.Id;
            //dto.Url = entity.Url;
            dto.AwardDateTime = entity.AwardDateTime;
            dto.AwardDateTimeStr = entity.AwardDateTime.ToString("yyyyMMdd");
            dto.AwardUnit = entity.AwardUnit;
            dto.Name = entity.Name;
            dto.Number = entity.Number;
            dto.TeacherId = entity.TeacherId;
            dto.TeacherName = entity.Teacher.Name;
            //dto.ThumbUrl = entity.ThumbUrl;
            dto.CreateDateTime = entity.CreateDateTime;
            return dto;
        }

        public TeacherDTO[] GetById(long[] ids)
        {
            if (ids.Length <= 0)
            {
                throw new ArgumentException("未选中任何教师");
            }
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<TeacherEntity>(mc);
                var teachers = bs.GetAll().Include(t => t.HighestEducation)
                    .Include(t => t.TeacherCardType)
                    .Include(t => t.AdminUser).Include(t => t.EmployType)
                    .Include(t => t.IdCardType).Include(t => t.PostType)
                    .Include(t => t.TeacherType).Include(t => t.ZhiCheng)
                    .Where(t => ids.Contains(t.Id)).ToArray();
                var list = new List<TeacherDTO>();
                if (teachers.Any())
                {
                    foreach (var item in teachers)
                    {
                        list.Add(ToDTO(item));
                    }
                }
                return list.ToArray();
            }
        }

        public long AddNew(TrainingAddNewDTO dto)
        {
            using (var mc = new MyDBContext())
            {
                var entity=new TrainingEntity();
                entity.TeacherId = dto.TeacherId;
                entity.TrainingContent = dto.TrainingContent;
                entity.TrainingLevelId = dto.TrainingLevelId;
                entity.TrainingTime = dto.TrainingTime;
                entity.TrainingTypeId = dto.TrainingTypeId;
                entity.UnitName = dto.UnitName;
                entity.Year = dto.Year;
                mc.Trainings.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        public TrainingDTO[] GetTrainings(long teacherId)
        {
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<TrainingEntity>(mc);
                var trainings = bs.GetAll().Include(t => t.Teacher)
                    .Include(t=>t.TrainingLevel).Include(t=>t.TrainingType)
                    .Where(t => t.TeacherId == teacherId).ToList();
                var list = new List<TrainingDTO>();
                foreach (var cer in trainings)
                {
                    list.Add(ToDTO(cer));
                }

                return list.ToArray();
            }
        }

        private TrainingDTO ToDTO(TrainingEntity entity)
        {
            var dto = new TrainingDTO()
            {
                Id = entity.Id,
                Year=entity.Year,
                UnitName = entity.UnitName,
                TeacherId = entity.TeacherId,
                TeacherName = entity.Teacher.Name,
                TrainingContent = entity.TrainingContent,
                TrainingTime = entity.TrainingTime,
                TrainingTypeId = entity.TrainingTypeId,
                TrainingLevelId = entity.TrainingLevelId,
                TrainingLevelName = entity.TrainingLevel.Value,
                TrainingTypeName = entity.TrainingType.Value
            };
            return dto;
        }

        public void DelTraining(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<TrainingEntity> bs = new BaseService<TrainingEntity>(mc);
                bs.MarkDeleted(id);
            }
        }

        public CertificateDTO[] GetCertificates(long[] teacherIds)
        {
            if (teacherIds.Length <= 0)
            {
                throw new ArgumentException("未选中任何教师");
            }
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<CertificateEntity>(mc);
                var cers = bs.GetAll().Include(t => t.Teacher).Where(t => teacherIds.Contains(t.TeacherId )).ToList();
                var list = new List<CertificateDTO>();
                foreach (var cer in cers)
                {
                    list.Add(ToDTO(cer));
                }

                return list.ToArray();
            }
        }

        public TrainingDTO[] GetTrainings(long[] teacherIds)
        {
            if (teacherIds.Length <= 0)
            {
                throw new ArgumentException("未选中任何教师");
            }
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<TrainingEntity>(mc);
                var trainings = bs.GetAll().Include(t => t.Teacher)
                    .Include(t => t.TrainingLevel).Include(t => t.TrainingType)
                    .Where(t=>teacherIds.Contains(t.TeacherId)).ToList();
                var list = new List<TrainingDTO>();
                foreach (var cer in trainings)
                {
                    list.Add(ToDTO(cer));
                }

                return list.ToArray();
            }
        }

        public TrainingDTO GetTraining(long id)
        {
            using (var mc =new MyDBContext())
            {
                var bs = new BaseService<TrainingEntity>(mc);
                var training = bs.GetAll().Include(t => t.Teacher)
                    .Include(t => t.TrainingLevel).Include(t => t.TrainingType).SingleOrDefault(t => t.Id == id);
                return training == null ? null : ToDTO(training);
            }
        }

        public void Update(TrainingEditDTO dto)
        {
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<TrainingEntity>(mc);
                var training = bs.GetById(dto.Id);
                if (training == null)
                {
                    throw  new ArgumentException("不存在的培训信息，id="+dto.Id);
                }

                training.TrainingContent = dto.TrainingContent;
                training.TrainingLevelId = dto.TrainingLevelId;
                training.TrainingTime = dto.TrainingTime;
                training.TrainingTypeId = dto.TrainingTypeId;
                training.UnitName = dto.UnitName;
                training.Year = dto.Year;
                mc.SaveChanges();
            }
        }

        public void Update(CertificateEditDTO dto)
        {
            using (var mc = new MyDBContext())
            {
                var bs=new BaseService<CertificateEntity>(mc);
                var cer = bs.GetById(dto.Id);
                if (cer == null)
                {
                    throw new ArgumentException("不存在的证书信息，Id="+dto.Id);
                }

                cer.AwardDateTime = dto.AwardDateTime;
                cer.AwardUnit = dto.AwardUnit;
                cer.Name = dto.Name;
                cer.Number = dto.Number;
                //cer.Url = dto.Url;
                //cer.ThumbUrl = dto.ThumbUrl;
                mc.SaveChanges();
            }
        }

        public long AddNew(long certificateId, string url,string thumbUrl)//add  new pic
        {
            var entity=new CertificatePicEntity();
            entity.Url = url;
            entity.CertificateId = certificateId;
            entity.ThumbUrl = thumbUrl;
            using (var mc = new MyDBContext())
            {
                mc.CertificatePics.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        public void DelPics(long certificateId)
        {
            using (var mc = new MyDBContext())
            {
                var bs =new BaseService<CertificatePicEntity>(mc);
                var pics = bs.GetAll().Where(t => t.CertificateId == certificateId).ToList();
                if (pics.Any())
                {
                    foreach (var pic in pics)
                    {
                        pic.IsDeleted = true;
                    }

                    mc.SaveChanges();
                }
            }
        }

        private CertificatePicDTO ToDto(CertificatePicEntity entity)
        {
            var dto = new CertificatePicDTO();
            dto.Id = entity.Id;
            dto.CertificateId = entity.CertificateId;
            dto.Url = entity.Url;
            dto.ThumbUrl = entity.ThumbUrl;
            return dto;
        }

        public CertificatePicDTO[] GetPics()
        {
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<CertificatePicEntity>(mc);
                var pics = bs.GetAll().Include(t => t.Certificate).ToList();
                var list = new List<CertificatePicDTO>();
                foreach (var pic in pics)
                {
                    list.Add(ToDto(pic));
                }

                return list.ToArray();
            }
        }

        public CertificatePicDTO[] GetPics(long certificateId)
        {
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<CertificatePicEntity>(mc);
                var pics = bs.GetAll().Include(t => t.Certificate).Where(t=>t.CertificateId==certificateId).ToList();
                var list = new List<CertificatePicDTO>();
                foreach (var pic in pics)
                {
                    list.Add(ToDto(pic));
                }

                return list.ToArray();
            }
        }
    }
}
