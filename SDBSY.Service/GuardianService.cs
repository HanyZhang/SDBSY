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
    public class GuardianService : IGuardianService
    {
        public long AddNew(string name,  long idCardTypeId, string idCardNum)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<GuardianEntity> bs = new BaseService<GuardianEntity>(mc);
                bool exsit= bs.GetAll().Any(t => t.IdCardNum == idCardNum);
                if(exsit)
                {
                    //throw new ArgumentException("已存在相同的监护人身份证号："+idCardNum);
                    //已存在
                    return -1;
                }
                GuardianEntity entity = new GuardianEntity();
                entity.IdCardTypeId = idCardTypeId;
                entity.IdCardNum = idCardNum;
                entity.Name = name;
                //entity.PhoneNum = phoneNum;
                mc.Guardians.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        public GuardianDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public long GetByIdCardNum(string idCardNum)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<GuardianEntity> bs = new BaseService<GuardianEntity>(mc);
                var gua= bs.GetAll().Single(t => t.IdCardNum == idCardNum);
                return gua.Id;
            }
        }

        public void Update(long id, string name, string phoneNum, long idCardTypeId, string idCardNum)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<GuardianEntity> bs = new BaseService<GuardianEntity>(mc);
                var guardian= bs.GetById(id);
                if(guardian==null)
                {
                    throw new ArgumentException("不存在的监护人信息");
                }
                guardian.Name = name;
                guardian.PhoneNum = phoneNum;
                guardian.IdCardTypeId = idCardTypeId;
                guardian.IdCardNum = idCardNum;
                mc.SaveChanges();
            }
        }
    }
}
