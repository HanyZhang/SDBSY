using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;

namespace SDBSY.IService
{
    public interface IPlaceService:IServiceSupport
    {
        PlaceDTO[] GetAll();
        PlaceDTO GetById(long id);
    }
}
