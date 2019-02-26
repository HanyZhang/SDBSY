using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;

namespace SDBSY.IService
{
    public interface IGuardianService:IServiceSupport
    {
        GuardianDTO GetById(long id);
        long AddNew(string name,long idCardTypeId,string idCardNum);
        void Update(long id, string name, string phoneNum, long idCardTypeId, string idCardNum);
        long GetByIdCardNum(string idCardNum);
    }
}
