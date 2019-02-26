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
    public class PlaceService : IPlaceService
    {
        private PlaceDTO ToDTO(PlaceEntity entity)
        {
            PlaceDTO dto = new PlaceDTO();
            dto.Code = entity.Code;
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            return dto;
        }
        public PlaceDTO[] GetAll()
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<PlaceEntity> bs = new BaseService<PlaceEntity>(mc);
                var places= bs.GetAll().ToList();
                List<PlaceDTO> list = new List<PlaceDTO>();
                foreach(var item in places)
                {
                    list.Add(ToDTO(item));
                }
                return list.ToArray();
            }
        }

        public PlaceDTO GetById(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<PlaceEntity> bs = new BaseService<PlaceEntity>(mc);
                var place= bs.GetById(id);
                return place == null ? null : ToDTO(place);
            }
        }
    }
}
