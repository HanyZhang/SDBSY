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
    public class NationService : INationService
    {
        private NationDTO ToDTO(NationEntity entity)
        {
            NationDTO dto = new NationDTO();
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            return dto;
        }
        public NationDTO[] GetAll()
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<NationEntity> bs = new BaseService<NationEntity>(mc);
                var nations= bs.GetAll().ToList();
                List<NationDTO> list = new List<NationDTO>();
                foreach(var item in nations)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }

        public NationDTO GetById(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<NationEntity> bs = new BaseService<NationEntity>(mc);
                var nation= bs.GetById(id);
                return nation == null ? null : ToDTO(nation);
            }
         }
    }
}
