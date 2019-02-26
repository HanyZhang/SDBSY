using SDBSY.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;
using SDBSY.Service.Entities;

namespace SDBSY.Service
{
    public class DataDictionaryService : IDataDictionaryService
    {
        public long AddNew(string name, string value)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<DataDictionaryEntity> bs = new BaseService<DataDictionaryEntity>(mc);
                bool exsit= bs.GetAll().Any(t => t.Name == name && t.Value == value);
                if(exsit)
                {
                    throw new ArgumentException("已存在相同数据：Name="+name+",value="+value);
                }
                DataDictionaryEntity entity = new DataDictionaryEntity();
                entity.Name = name;
                entity.Value = value;
                mc.DataDictionaries.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }
        private DataDictionaryDTO ToDTO(DataDictionaryEntity entity)
        {
            DataDictionaryDTO dto = new DataDictionaryDTO();
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Value = entity.Value;
            return dto;
        }
        public DataDictionaryDTO GetById(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<DataDictionaryEntity> bs = new BaseService<DataDictionaryEntity>(mc);
                var data= bs.GetById(id);
                return data == null ? null : ToDTO(data);
            }
        }
        
        public DataDictionaryDTO[] GetByName(string name)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<DataDictionaryEntity> bs = new BaseService<DataDictionaryEntity>(mc);
                var data = bs.GetAll().Where(t=>t.Name==name).ToList();
                if (data.Count() <= 0)
                {
                    return null;
                }
                List<DataDictionaryDTO> list = new List<DataDictionaryDTO>();
                foreach(var item in data)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }

        public bool Exsits(string name, string value)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<DataDictionaryEntity> bs = new BaseService<DataDictionaryEntity>(mc);
                return bs.GetAll().Any(t => t.Name == name && t.Value == value);
            }
        }

        public void UpdateValue(string name, string value)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<DataDictionaryEntity> bs = new BaseService<DataDictionaryEntity>(mc);
                var data = bs.GetAll().Single(t => t.Name == name);
                data.Value = value;
                mc.SaveChanges();
            }
        }

        public DataDictionaryDTO GetSingleByName(string name)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<DataDictionaryEntity> bs = new BaseService<DataDictionaryEntity>(mc);
                var data = bs.GetAll().Single(t => t.Name == name);
                return ToDTO(data);
            }
        }

        public void MarkDeleted(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<DataDictionaryEntity> bs = new BaseService<DataDictionaryEntity>(mc);
                bs.MarkDeleted(id);
            }
        }

        public void UpdateValue(long id, string value)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<DataDictionaryEntity> bs = new BaseService<DataDictionaryEntity>(mc);
                var data = bs.GetById(id);
                if(data==null)
                {
                    throw new ArgumentException("不存在的数据，id="+id);
                }
                data.Value = value;
                mc.SaveChanges();
            }
        }
    }
}
