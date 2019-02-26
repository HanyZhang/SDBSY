using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;

namespace SDBSY.IService
{
    public interface IMenuService:IServiceSupport
    {
        long AddNew(string name,string url,string icon,int order,long parentId,string number);
        MenuDTO[] GetAll();
        MenuDTO GetDto(long id);
        MenuDTO GetDto(string number);
        void Update(long id,string name, string url, string icon, int order, long parentId, string number);
    }
}
