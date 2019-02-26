using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;

namespace SDBSY.IService
{
    public interface ISystemSettingService:IServiceSupport
    {
        SystemSettingDTO[] GetAll();
        void Update(long id, string val);
        void Update(string name,string val);
        long AddNew(string name, string val, string discription);
        string GetVal(string name);
        string GetVal(long id);
    }
}
