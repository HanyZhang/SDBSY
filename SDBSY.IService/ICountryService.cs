using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;

namespace SDBSY.IService
{
    public interface ICountryService:IServiceSupport
    {
        CountryDTO[] GetAll();
        CountryDTO GetById(long id);
    }
}
