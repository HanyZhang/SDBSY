#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.Service
* 项目描述 ：
* 类 名 称 ：MenuService
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.Service
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2018-12-05 16:43:18
* 更新时间 ：2018-12-05 16:43:18
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
    public class MenuService : IMenuService
    {
        public long AddNew(string name, string url, string icon, int order, long parentId, string number)
        {
            using (var mc = new MyDBContext())
            {
                BaseService<MenuEntity> bs =new BaseService<MenuEntity>(mc);
                var exsit = bs.GetAll().Any(t => t.Number == number);
                if (exsit)
                {
                    return -1;//已存在相同序号的菜单项，number="User.Delete"
                }

                var entity = new MenuEntity()
                {
                    Name = name,
                    Number = number,
                    Url = url,
                    Icon = icon,
                    Order = order,
                    ParentId = parentId
                };
                mc.Menus.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        private MenuDTO ToDto(MenuEntity entity)
        {
            var dto = new MenuDTO()
            {
                Id=entity.Id,
                CreateDateTime = entity.CreateDateTime,
                Name = entity.Name,
                Number = entity.Number,
                Icon = entity.Icon,
                Url=entity.Url,
                ParentId = entity.ParentId,
                Order = entity.Order
            };
            return dto;
        }

        public MenuDTO[] GetAll()
        {
            using (var mc = new MyDBContext())
            {
                var bs=new BaseService<MenuEntity>(mc);
                var menus= bs.GetAll().ToList();
                var list=new List<MenuDTO>();
                foreach (var menu in menus)
                {
                    list.Add(ToDto(menu));
                }

                return list.ToArray();
            }
        }

        public MenuDTO GetDto(long id)
        {
            using (var mc = new MyDBContext())
            {
                var bs=new BaseService<MenuEntity>(mc);
                var menu = bs.GetAll().SingleOrDefault(t => t.Id == id);
                return menu == null ? null : ToDto(menu);
            }
        }

        public MenuDTO GetDto(string number)
        {
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<MenuEntity>(mc);
                var menu = bs.GetAll().SingleOrDefault(t => t.Number == number);
                return menu == null ? null : ToDto(menu);
            }
        }

        public void Update(long id, string name, string url, string icon, int order, long parentId, string number)
        {
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<MenuEntity>(mc);
                var menu = bs.GetAll().SingleOrDefault(t => t.Id == id);
                if (menu == null)
                {
                    throw  new ArgumentException("不存在的菜单项：id="+id);
                }

                menu.Name = name;
                menu.Url = url;
                menu.Icon = icon;
                menu.Order = order;
                menu.ParentId = parentId;
                menu.Number = number;
                mc.SaveChanges();
            }
        }
    }
}
