using SDBSY.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;
using SDBSY.Service.Entities;
using System.Data.Entity;
using SDBSY.Common;

namespace SDBSY.Service
{
    public class StudentService : IStudentService
    {
        public void ShenHe(long id,bool pass)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                var entity = bs.GetById(id);
                if (entity == null)
                {
                    throw new ArgumentException("不存在的学生信息：" + id);
                }
                entity.Status =pass?ShenHeZhuangTai.TongGuo: ShenHeZhuangTai.BoHui;
                ctx.SaveChanges();
            }
        }
        public long AddNew(StudentAddNewDTO dto)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<StudentEntity> bss = new BaseService<StudentEntity>(mc);
                //身份证号码查重
                bool exsit= bss.GetAll().Any(t => t.IdCardNum == dto.IdCardNum);
                if(exsit)
                {
                    throw new ArgumentException("已存在相同的身份证号："+dto.IdCardNum);
                }
                StudentEntity entity = new StudentEntity();
                entity.BankCardNum = dto.BankCardNum;
                entity.BirthDate = dto.BirthDate;
                entity.BirthPlaceId = dto.BirthPlaceId;
                entity.BloodTypeId = dto.BloodTypeId;
                entity.ClassId = dto.ClassId;
                entity.CountryId = dto.CountryId;
                entity.DisabilityTypeId = dto.DisabilityTypeId;
                entity.FatherId = dto.FatherId;
                entity.FeiNongHuKouTypeId = dto.FeiNongHuKouTypeId;
                entity.Gender = dto.Gender;
                entity.GuardianId = dto.GuardianId;
                entity.HasInfection = dto.HasInfection;
                entity.HealthyTypeId = dto.HealthyTypeId;
                entity.HomePlace = dto.HomePlace;
                entity.HuKouPlaceId = dto.HuKouPlaceId;
                entity.HuKouXingZhiId = dto.HuKouXingZhiId;
                entity.IdCardNum = dto.IdCardNum;
                entity.IdCardTypeId = dto.IdCardTypeId;
                entity.IdentityId = dto.IdentityId;
                entity.PhotoUrl = dto.PhotoUrl;
                entity.TijianUrl = dto.TijianUrl;
                entity.InKindergartenDate = dto.InKindergartenDate;
                entity.IsDisability = dto.IsDisability;
                entity.IsFinishSchool = dto.IsFinishSchool;
                entity.IsFollowWorkInCity = dto.IsFollowWorkInCity;
                entity.IsOnlyBaby = dto.IsOnlyBaby;
                entity.IsOrphan = dto.IsOrphan;
                entity.IsStayAtHomeId = dto.IsStayAtHomeId;
                entity.IsStayInClass = dto.IsStayInClass;
                entity.MotherId = dto.MotherId;
                entity.Name = dto.Name;
                entity.NationId = dto.NationId;
                entity.OtherTel = dto.OtherTel;
                entity.PriginPlace = dto.PriginPlace;
                entity.StudyTypeId = dto.StudyTypeId;
                entity.UserId = dto.UserId;
                entity.Status = ShenHeZhuangTai.MoRen;//报名
                mc.Students.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }
        public void UpdateStudent(StudentEditDTO dto)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<StudentEntity> bss = new BaseService<StudentEntity>(mc);
                //身份证号码查重
                bool exsit = bss.GetAll().Any(t => t.IdCardNum == dto.IdCardNum && t.Id != dto.Id);
                if (exsit)
                {
                    throw new ArgumentException("已存在相同的身份证号：" + dto.IdCardNum);
                }
                StudentEntity entity = bss.GetById(dto.Id);
                if (entity == null)
                {
                    throw new ArgumentException("不存在的幼儿信息：id=" + dto.Id);
                }
                entity.BankCardNum = dto.BankCardNum;
                entity.BirthDate = dto.BirthDate;
                entity.BirthPlaceId = dto.BirthPlaceId==0?null:dto.BirthPlaceId;
                entity.BloodTypeId = dto.BloodTypeId;
                entity.CountryId = dto.CountryId;
                entity.DisabilityTypeId = dto.DisabilityTypeId==0?null:dto.DisabilityTypeId;
                entity.FatherId = dto.FatherId==0?null:dto.FatherId;
                entity.FeiNongHuKouTypeId = dto.FeiNongHuKouTypeId==0?null:dto.FeiNongHuKouTypeId;
                entity.Gender = dto.Gender;
                entity.GuardianId = dto.GuardianId;
                entity.HasInfection = dto.HasInfection;
                entity.HealthyTypeId = dto.HealthyTypeId;
                entity.HomePlace = dto.HomePlace;
                entity.HuKouPlaceId = dto.HuKouPlaceId==0?null:dto.HuKouPlaceId;
                entity.HuKouXingZhiId = dto.HuKouXingZhiId==0?null:dto.HuKouXingZhiId;
                entity.IdCardNum = dto.IdCardNum;
                entity.IdCardTypeId = dto.IdCardTypeId;
                entity.IdentityId = dto.IdentityId;
                entity.PhotoUrl = dto.PhotoUrl;
                entity.TijianUrl = dto.TijianUrl;
                //entity.InKindergartenDate = dto.InKindergartenDate;
                entity.IsDisability = dto.IsDisability;
                //entity.IsFinishSchool = dto.IsFinishSchool;
                entity.IsFollowWorkInCity = dto.IsFollowWorkInCity;
                entity.IsOnlyBaby = dto.IsOnlyBaby;
                entity.IsOrphan = dto.IsOrphan;
                entity.IsStayAtHomeId = dto.IsStayAtHomeId;
                //entity.IsStayInClass = dto.IsStayInClass;
                entity.MotherId = dto.MotherId==0?null:dto.MotherId;
                entity.Name = dto.Name;
                entity.NationId = dto.NationId==0?null:dto.NationId;
                entity.OtherTel = dto.OtherTel;
                entity.PriginPlace = dto.PriginPlace;
                entity.StudyTypeId = dto.StudyTypeId;
                //entity.UserId = dto.UserId;
                mc.SaveChanges();
            }
        }
        public void FinishSchool(long id,DateTime dateTime)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                var entity = bs.GetById(id);
                if (entity == null)
                {
                    throw new ArgumentException("不存在的学生信息：" + id);
                }
                string oldClassName = entity.Class.Value;
                entity.IsFinishSchool = true;
                entity.FinishSchoolTime = dateTime;
                entity.ClassId = null;
                GoUpRecordEntity record = new GoUpRecordEntity();
                record.OldClassName = oldClassName;
                record.StudentId = id;
                record.NewClassName = "已毕业";
                record.Time = dateTime;
                ctx.GoUpRecords.Add(record);
                ctx.SaveChanges();
            }
        }

        public StudentDTO[] GetAll()//所有未毕业的学生
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(mc);
                var items = bs.GetAll()
                    .Include(t => t.BirthPlace)
                    .Include(t => t.BloodType).Include(t => t.Class).Include(t => t.Country)
                    .Include(t => t.DisabilityType).Include(t => t.Father).Include(t => t.FeiNongHuKouType)
                    .Include(t => t.Guardian).Include(t => t.HealthyType)
                    .Include(t => t.HuKouPlace)
                    .Include(t => t.HuKouXingZhi)
                    .Include(t => t.IdCardType).Include(t => t.Identity).Include(t => t.IsStayAtHome).Include(t => t.Mother)
                    .Include(t => t.Nation).Include(t => t.StudyType).Include(t => t.User).ToList();
                
                List<StudentDTO> list = new List<StudentDTO>();
                if (items != null && items.Count() > 0)
                {
                    foreach (var item in items)
                    {
                        list.Add(ToDTO(item));
                    }
                }
                return list.ToArray();
            }
        }

        public StudentDTO[] GetByClassId(long classId)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(mc);
                var items= bs.GetAll().Include(t => t.BirthPlace).Include(t => t.BloodType).Include(t => t.Class).Include(t => t.Country)
                    .Include(t => t.DisabilityType).Include(t => t.Father).Include(t => t.FeiNongHuKouType)
                    .Include(t => t.Guardian).Include(t => t.HealthyType).Include(t => t.HuKouPlace).Include(t => t.HuKouXingZhi)
                    .Include(t => t.IdCardType).Include(t => t.Identity).Include(t => t.IsStayAtHome).Include(t => t.Mother)
                    .Include(t => t.Nation).Include(t => t.StudyType).Include(t => t.User).Where(t => t.ClassId == classId).ToList();
                
                List<StudentDTO> list = new List<StudentDTO>();
                if (items != null && items.Count() > 0)
                {
                    foreach (var item in items)
                    {
                        list.Add(ToDTO(item));
                    }
                }
                return list.ToArray();
            }
        }

        
        private StudentDTO ToDTO(StudentEntity entity)
        {
            StudentDTO dto = new StudentDTO();
            dto.BankCardNum = entity.BankCardNum;
            dto.BirthDate = entity.BirthDate;
            dto.BirthPlaceId = entity.BirthPlaceId;
            if (dto.BirthPlaceId == null)
            {
                dto.BirthPlaceCode = null;
                dto.BirthPlaceName = null;
            }
            else
            {
                dto.BirthPlaceCode = entity.BirthPlace.Code;
                dto.BirthPlaceName = entity.BirthPlace.Name;
            }
            dto.BloodTypeId = entity.BloodTypeId;
            dto.BloodTypeName = entity.BloodType.Value;
            dto.ClassId = entity.ClassId;
            if (dto.ClassId == null)
            {
                dto.ClassName = "未分班";
            }
            else
            {
                dto.ClassName = entity.Class.Value;
            }
            dto.CountryId = entity.CountryId;
            dto.CountryName = entity.Country.Name;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.IsDisability = entity.IsDisability;
            if (dto.IsDisability)
            {
                dto.DisabilityTypeId = entity.DisabilityTypeId;
                dto.DisabilityTypeName = entity.DisabilityType.Value;
            }
            else {
                dto.DisabilityTypeId = null;
                dto.DisabilityTypeName = null;
            }
            dto.FatherId = entity.FatherId;
            if(dto.FatherId==null)
            {
                dto.FatherCardTypeId = null;
                dto.FatherCardTypeName = null;
                dto.FatherCardNum = null;
                dto.FatherName = null;
                dto.FatherPhoneNum = null;
                dto.FatherWorkUnit = null;
            }
            else
            {
                dto.FatherCardTypeId = entity.Father.IdCardTypeId;
                dto.FatherCardTypeName = entity.Father.IdCardType.Value;
                dto.FatherCardNum = entity.Father.IdCardNum;
                dto.FatherName = entity.Father.Name;
                dto.FatherPhoneNum = entity.Father.PhoneNum;
                dto.FatherWorkUnit = entity.Father.WorkUnit;
            }
            dto.MotherId = entity.MotherId;
            if (dto.MotherId == null)
            {
                dto.MotherCardTypeId = null;
                dto.MotherCardNum = null;
                dto.MotherName = null;
                dto.MotherPhoneNum = null;
                dto.MotherWorkUnit = null;
            }
            else
            {
                dto.MotherCardTypeId = entity.Mother.IdCardTypeId;
                dto.MotherCardTypeName = entity.Mother.IdCardType.Value;
                dto.MotherCardNum = entity.Mother.IdCardNum;
                dto.MotherName = entity.Mother.Name;
                dto.MotherPhoneNum = entity.Mother.PhoneNum;
                dto.MotherWorkUnit = entity.Mother.WorkUnit;
            }
            dto.FeiNongHuKouTypeId = entity.FeiNongHuKouTypeId;
            if(dto.FeiNongHuKouTypeId==null)
            {
                dto.FeiNongHuKouTypeName = null;
            }
            else
            {
                dto.FeiNongHuKouTypeName = entity.FeiNongHuKouType.Value;
            }
            dto.Gender = entity.Gender;
            if(dto.Gender)
            {
                dto.GenderName = "女";
            }
            else
            {
                dto.GenderName = "男";
            }
            dto.GuardianId = entity.GuardianId;
            dto.GuardianCardNum = entity.Guardian.IdCardNum;
            dto.GuardianCardTypeId = entity.Guardian.IdCardTypeId;
            dto.GuardianCardTypeName = entity.Guardian.IdCardType.Value;
            dto.GuardianName = entity.Guardian.Name;
            dto.HasInfection = entity.HasInfection;
            dto.HealthyTypeId = entity.HealthyTypeId;
            dto.HealthyTypeName = entity.HealthyType.Value;
            dto.HomePlace = entity.HomePlace;
            dto.HuKouPlaceId = entity.HuKouPlaceId;
            if(dto.HuKouPlaceId==null)
            {
                dto.HuKouPlaceName = null;
                dto.HuKouPlaceCode = null;
            }
            else
            {
                dto.HuKouPlaceName = entity.HuKouPlace.Name;
                dto.HuKouPlaceCode = entity.HuKouPlace.Code;
            }
            dto.HuKouXingZhiId = entity.HuKouXingZhiId;
            if(dto.HuKouXingZhiId==null)
            {
                dto.HuKouXingZhiName = null;
            }
            else
            {
                dto.HuKouXingZhiName = entity.HuKouXingZhi.Value;
            }
            dto.Id = entity.Id;
            dto.IdCardTypeId = entity.IdCardTypeId;
            dto.IdCardTypeName = entity.IdCardType.Value;
            dto.IdCardNum = entity.IdCardNum;
            dto.IdentityId = entity.IdentityId;
            dto.IdentityName = entity.Identity.Value;
            dto.PhotoUrl = entity.PhotoUrl;
            dto.TijianUrl = entity.TijianUrl;
            dto.InKindergartenDate = entity.InKindergartenDate;
            dto.IsFinishSchool = entity.IsFinishSchool;
            dto.FinishSchoolTime = entity.FinishSchoolTime;
            dto.IsFollowWorkInCity = entity.IsFollowWorkInCity;
            dto.IsOnlyBaby = entity.IsOnlyBaby;
            dto.IsOrphan = entity.IsOrphan;
            dto.IsStayAtHomeId = entity.IsStayAtHomeId;
            dto.IsStayAtHomeName = entity.IsStayAtHome.Value;
            dto.IsStayInClass = entity.IsStayInClass;
            dto.Name = entity.Name;
            dto.NationId = entity.NationId;
            if (dto.NationId == null)
            {
                dto.NationName = null;
            }
            else
            {
                dto.NationName = entity.Nation.Name;
            }
            dto.OtherTel = entity.OtherTel;
            dto.PriginPlace = entity.PriginPlace;
            dto.StudyTypeId = entity.StudyTypeId;
            dto.StudyTypeName = entity.StudyType.Value;
            dto.UserId = entity.UserId;
            if(dto.UserId==null)
            {
                dto.UserPhoneNum = null;
            }
            else
            {
                dto.UserPhoneNum = entity.User.PhoneNum;
            }
            dto.Status = entity.Status;
            return dto;
        }
        public StudentDTO[] GetByUserId(long userId)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                var items= bs.GetAll().AsNoTracking().Include(t => t.BloodType).Include(t => t.Class).Include(t => t.Country)
                    .Include(t => t.DisabilityType).Include(t => t.Father).Include(t => t.FeiNongHuKouType)
                    .Include(t => t.Guardian).Include(t => t.HealthyType).Include(t => t.HuKouPlace).Include(t => t.HuKouXingZhi)
                    .Include(t => t.IdCardType).Include(t => t.Identity).Include(t => t.IsStayAtHome).Include(t => t.Mother)
                    .Include(t => t.Nation).Include(t => t.StudyType).Include(t => t.User).Include(t=>t.BirthPlace)
                    .Where(t => t.UserId == userId).ToList();
                
                List<StudentDTO> list = new List<StudentDTO>();
                if (items != null && items.Count() > 0)
                {
                    foreach (var item in items)
                    {
                        list.Add(ToDTO(item));
                    }
                }
                return list.ToArray();
            }
        }

        public void StayInClass(long id,bool b)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                var entity = bs.GetById(id);
                if (entity == null)
                {
                    throw new ArgumentException("不存在的学生信息：" + id);
                }
                entity.IsStayInClass = b;
                ctx.SaveChanges();
            }
        }

        public void UpdateClass(long id, long classId)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                var entity= bs.GetById(id);
                if(entity==null)
                {
                    throw new ArgumentException("不存在的学生信息："+id);
                }
                entity.ClassId = classId;
                if(entity.InKindergartenDate==null)
                {
                    entity.InKindergartenDate = DateTime.Now;//
                }
                ctx.SaveChanges();
            }
        }

        public StudentDTO GetByIdCardNum(string idCardNum)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(mc);
                var entity = bs.GetAll().SingleOrDefault(t => t.IdCardNum == idCardNum);
                if (entity == null)
                    return null;
                else
                    return ToDTO(entity);
            }
        }

        public void UpdateDateTime(long id, DateTime date)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                var entity = bs.GetById(id);
                if (entity == null)
                {
                    throw new ArgumentException("不存在的学生信息：" + id);
                }
                entity.InKindergartenDate = date;
                ctx.SaveChanges();
            }
        }

        public void MarkDeleted(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(mc);
                bs.MarkDeleted(id);
            }
        }
        public StudentDTO GetById(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(mc);
                var item = bs.GetAll().Include(t => t.BirthPlace).Include(t => t.BloodType).Include(t => t.Class).Include(t => t.Country)
                    .Include(t => t.DisabilityType).Include(t => t.Father).Include(t => t.FeiNongHuKouType)
                    .Include(t => t.Guardian).Include(t => t.HealthyType).Include(t => t.HuKouPlace).Include(t => t.HuKouXingZhi)
                    .Include(t => t.IdCardType).Include(t => t.Identity).Include(t => t.IsStayAtHome).Include(t => t.Mother)
                    .Include(t => t.Nation).Include(t => t.StudyType).Include(t => t.User).SingleOrDefault(t => t.Id == id);
                return item == null ? null : ToDTO(item);

            }
        }
        public StudentDTO[] GetById(long[] selectIds)
        {
            if(selectIds.Length<=0)
            {
                throw new ArgumentException("未选中任何学生");
            }
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(mc);
                var stus = bs.GetAll().Include(t => t.BirthPlace).Include(t => t.BloodType).Include(t => t.Class).Include(t => t.Country)
                    .Include(t => t.DisabilityType).Include(t => t.Father).Include(t => t.FeiNongHuKouType)
                    .Include(t => t.Guardian).Include(t => t.HealthyType).Include(t => t.HuKouPlace).Include(t => t.HuKouXingZhi)
                    .Include(t => t.IdCardType).Include(t => t.Identity).Include(t => t.IsStayAtHome).Include(t => t.Mother)
                    .Include(t => t.Nation).Include(t => t.StudyType).Include(t => t.User).Where(t => selectIds.Contains(t.Id)).ToArray();
                List<StudentDTO> list = new List<StudentDTO>();
                if (stus != null && stus.Count() > 0)
                {
                    foreach (var item in stus)
                    {
                        list.Add(ToDTO(item));
                    }
                }
                return list.ToArray();
            }
        }

        public void UpdateStatus(long id, int status)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(mc);
                var stu= bs.GetById(id);
                if(stu==null)
                {
                    throw new ArgumentException("不存在的学生信息,id="+id);
                }
                stu.Status = status;
                mc.SaveChanges();
            }
        }

        public long AddGoUpRecord(long stuId, long? oldClassId, long newClassId,DateTime dateTime)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<DataDictionaryEntity> bs = new BaseService<DataDictionaryEntity>(mc);
                var newClass = bs.GetById(newClassId);
                if(newClass==null)
                {
                    throw new ArgumentException("新设置的班级不存在。");
                }
                GoUpRecordEntity entity = new GoUpRecordEntity();
                entity.StudentId = stuId;
                if (oldClassId == null)
                {
                    entity.OldClassName = "新入园";
                }
                else
                {
                    var oldClass = bs.GetById(oldClassId.Value);
                    entity.OldClassName = oldClass.Value;
                }
                entity.NewClassName = newClass.Value;
                entity.Time = dateTime;
                mc.GoUpRecords.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        public GoUpRecordDTO[] GetGoUpRecords(long stuId)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<GoUpRecordEntity> bs = new BaseService<GoUpRecordEntity>(mc);
                var records= bs.GetAll().Include(t => t.Student).AsNoTracking().Where(t => t.StudentId == stuId);
                List<GoUpRecordDTO> list = new List<GoUpRecordDTO>();
                foreach(var item in records)
                {
                    GoUpRecordDTO dto = new GoUpRecordDTO() {
                        Id=item.Id,
                        CreateDateTime=item.CreateDateTime,
                        StudentId=item.StudentId,
                        OldClassName=item.OldClassName,
                        NewClassName=item.NewClassName,
                        Time=item.Time,
                    };
                    list.Add(dto);

                }
                return list.ToArray();
            }
        }
    }
}
