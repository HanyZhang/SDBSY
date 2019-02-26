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
    public class CountryService : ICountryService
    {
        private CountryDTO ToDTO(CountryEntity entity)
        {
            CountryDTO dto = new CountryDTO();
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            return dto;
        }
        public CountryDTO[] GetAll()
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<CountryEntity> bs = new BaseService<CountryEntity>(mc);
                var items= bs.GetAll().ToList();
                List<CountryDTO> list = new List<CountryDTO>();
                foreach(var item in items)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }

        public CountryDTO GetById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
