using SDBSY.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.Service.Entities;
using SDBSY.DTO;

namespace SDBSY.Service
{
    public class ParentService : IParentService
    {
        public long AddNew(string name, string workUnit, string phoneNum, long idCardTypeId, string idCardNum)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<ParentEntity> bs = new BaseService<ParentEntity>(mc);
                bool exsit= bs.GetAll().Any(t => t.IdCardNum == idCardNum);
                if (exsit)
                {
                    //throw new ArgumentException("已存在相同的身份证号码："+idCardNum);
                    //已存在
                    return -1;
                }
                ParentEntity entity = new ParentEntity();
                entity.IdCardNum = idCardNum;
                entity.IdCardTypeId = idCardTypeId;
                entity.Name = name;
                entity.WorkUnit = workUnit;
                entity.PhoneNum = phoneNum;
                mc.Parents.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        public ParentDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public long GetByIdCardNum(string idCardNum)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<ParentEntity> bs = new BaseService<ParentEntity>(mc);
                var entity = bs.GetAll().Single(t => t.IdCardNum == idCardNum);
                return entity.Id;
            }
        }

        public void Update(long id, string name, string workUnit, string phoneNum, long idCardTypeId, string idCardNum)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<ParentEntity> bs = new BaseService<ParentEntity>(mc);
                var par= bs.GetById(id);
                if(par==null)
                {
                    throw new ArgumentException("不存在的家长信息");
                }
                par.Name = name;
                par.WorkUnit = workUnit;
                par.PhoneNum = phoneNum;
                par.IdCardTypeId = idCardTypeId;
                par.IdCardNum = idCardNum;
                mc.SaveChanges();
            }
        }
    }
}
