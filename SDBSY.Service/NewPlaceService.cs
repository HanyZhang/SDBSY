#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.Service
* 项目描述 ：
* 类 名 称 ：NewPlaceService
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.Service
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2018-12-10 10:11:26
* 更新时间 ：2018-12-10 10:11:26
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Hany 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.Service.Entities;

namespace SDBSY.Service
{
    class NewPlaceService : INewPlaceService
    {
        public NewPlaceDTO[] GetAll()
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<NewPlaceEntity> bs = new BaseService<NewPlaceEntity>(mc);
                var palces = bs.GetAll().ToList();
                var list = new List<NewPlaceDTO>();
                foreach (var p in palces)
                {
                    list.Add(ToDTO(p));
                }

                return list.ToArray();
            }
        }

        public NewPlaceDTO GetById(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<NewPlaceEntity> bs = new BaseService<NewPlaceEntity>(mc);
                var place= bs.GetById(id);
                return place == null ? null : ToDTO(place);
            }
        }

        public NewPlaceDTO[] GetByParent(long parentId)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<NewPlaceEntity> bs =new BaseService<NewPlaceEntity>(mc);
                var palces= bs.GetAll().Where(t => t.ParentId == parentId).ToList();
                var list=new List<NewPlaceDTO>();
                foreach (var p in palces)
                {
                    list.Add(ToDTO(p));
                }

                return list.ToArray();
            }
        }
        private NewPlaceDTO ToDTO(NewPlaceEntity entity)
        {
            NewPlaceDTO dto = new NewPlaceDTO();
            dto.ParentId = entity.ParentId;
            dto.Code = entity.Code;
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            return dto;
        }
    }
}
