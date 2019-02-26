using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service
{
    public class FoodService : IFoodService
    {
        public long AddNew(string name, string unit, string supplier)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                FoodEntity entity = new FoodEntity()
                {
                    Name = name,
                    Unit=unit,
                    Supplier=supplier,
                };
                mc.Foods.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        public long AddNewBuyRecord(long foodId, decimal unitPrice, decimal amount, DateTime buyTime, decimal totalPrice, string remark)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                FoodBuyRecordEntity entity = new FoodBuyRecordEntity() {
                    FoodId=foodId,
                    BuyTime=buyTime,
                    Amount=amount,
                    UnitPrice=unitPrice,
                    TotalPrice=totalPrice,
                    Remark=remark,
                };
                mc.FoodBuyRecords.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        private FoodDTO ToDTO(FoodEntity entity)
        {
            FoodDTO dto = new FoodDTO() {
                CreateDateTime = entity.CreateDateTime,
                Id=entity.Id,
                Name=entity.Name,
                Unit=entity.Unit,
                
            };
            dto.Supplier = string.IsNullOrEmpty(entity.Supplier) ?"未知": entity.Supplier;
            return dto;
        }

        public FoodDTO[] GetAll()
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<FoodEntity> bs = new BaseService<FoodEntity>(mc);
                var foods =  bs.GetAll();
                List<FoodDTO> list = new List<FoodDTO>();
                foreach(var item in foods)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }

        private FoodBuyRecordDTO ToDTO(FoodBuyRecordEntity entity)
        {
            FoodBuyRecordDTO dto = new FoodBuyRecordDTO() {
                Id=entity.Id,
                FoodId = entity.FoodId,
                FoodName = entity.Food.Name,
                FoodUnit=entity.Food.Unit,
                Supplier=entity.Food.Supplier,
                BuyTime=entity.BuyTime,
                Amount=entity.Amount,
                UnitPrice=entity.UnitPrice,
                TotalPrice=entity.TotalPrice,
                Remark=entity.Remark,
            };
            return dto;
        }

        public FoodBuyRecordDTO[] GetAllRecords(long foodId)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<FoodBuyRecordEntity> bs = new BaseService<FoodBuyRecordEntity>(mc);
                var records = bs.GetAll().Include(t=>t.Food).Where(t => t.FoodId == foodId);
                List<FoodBuyRecordDTO> list = new List<FoodBuyRecordDTO>();
                foreach(var item in records)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }

        public FoodBuyRecordDTO[] GetAllRecords(DateTime startTime, DateTime endTime)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<FoodBuyRecordEntity> bs = new BaseService<FoodBuyRecordEntity>(mc);
                var end = endTime.AddDays(1);
                var records = bs.GetAll().Include(t => t.Food).Where(t => t.BuyTime >=startTime &&t.BuyTime<end);
                List<FoodBuyRecordDTO> list = new List<FoodBuyRecordDTO>();
                foreach (var item in records)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }

        public FoodBuyRecordDTO[] GetAllRecords()
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<FoodBuyRecordEntity> bs = new BaseService<FoodBuyRecordEntity>(mc);
                var records = bs.GetAll().Include(t => t.Food);
                List<FoodBuyRecordDTO> list = new List<FoodBuyRecordDTO>();
                foreach (var item in records)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }

        public FoodDTO GetById(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<FoodEntity> bs = new BaseService<FoodEntity>(mc);
                var food= bs.GetById(id);
                return food == null ? null : ToDTO(food);
            }
        }

        public void Delete(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<FoodEntity> bs = new BaseService<FoodEntity>(mc);
                bs.MarkDeleted(id);
            }
        }

        public void RecordDelete(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<FoodBuyRecordEntity> bs = new BaseService<FoodBuyRecordEntity>(mc);
                bs.MarkDeleted(id);
            }
        }

        public FoodBuyRecordDTO[] GetAllRecords(long[] ids)
        {
            if(ids==null||ids.Length<=0)
            {
                throw new ArgumentException("没有选择任何信息");
            }
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<FoodBuyRecordEntity> bs = new BaseService<FoodBuyRecordEntity>(mc);
                var records= bs.GetAll().Include(t => t.Food).AsNoTracking().Where(t => ids.Contains(t.Id)).OrderBy(t=>t.BuyTime).ToArray();
                List<FoodBuyRecordDTO> list = new List<FoodBuyRecordDTO>();
                foreach(var item in records)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }
    }
}
