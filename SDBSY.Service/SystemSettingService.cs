using SDBSY.DTO;
using SDBSY.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.Service.Entities;

namespace SDBSY.Service
{
    public class SystemSettingService : ISystemSettingService
    {
        public long AddNew(string name, string val, string discription)
        {
            throw new NotImplementedException();
        }
        public SystemSettingDTO ToDTO(SystemSettingEntity entity)
        {
            SystemSettingDTO dto = new SystemSettingDTO();
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Discription = entity.Discription;
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Value = entity.Value;
            return dto;
        }
        public SystemSettingDTO[] GetAll()
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<SystemSettingEntity> bs = new BaseService<SystemSettingEntity>(mc);
                var settings=bs.GetAll();
                if(settings==null||settings.Count()<=0)
                {
                    return null;
                }
                List<SystemSettingDTO> list = new List<SystemSettingDTO>();
                foreach(var item in settings)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }

        public void Update(long id, string val)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<SystemSettingEntity> bs = new BaseService<SystemSettingEntity>(mc);
                var setting = bs.GetById(id);
                if(setting==null)
                {
                    throw new ArgumentException("不存在的设置项,id=" + id);
                }
                setting.Value = val;
                mc.SaveChanges();
            }
        }

        public void Update(string name, string val)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<SystemSettingEntity> bs = new BaseService<SystemSettingEntity>(mc);
                var setting = bs.GetAll().Single(t => t.Name == name);
                if (setting == null)
                {
                    throw new ArgumentException("不存在的设置项,name=" + name);
                }
                setting.Value = val;
                mc.SaveChanges();
            }
        }

        public string GetVal(string name)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<SystemSettingEntity> bs = new BaseService<SystemSettingEntity>(mc);
                var setting= bs.GetAll().Single(t => t.Name == name);
                //return setting == null ? null : setting.Value;//可以简化为下面一句
                return setting?.Value;
            }
        }

        public string GetVal(long id)
        {
            throw new NotImplementedException();
        }
    }
}
