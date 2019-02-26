using SDBSY.IService;
using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;
using System.Data.Entity;

namespace SDBSY.Service
{
    public class AdminLogService : IAdminLogService
    {
        public long AddNew(long adminId, string message)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                AdminLogEntity entity = new AdminLogEntity();
                entity.AdminUserId = adminId;
                entity.Message = message;
                mc.AdminLogs.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        public AdminLogDTO[] GetAll()
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<AdminLogEntity> bs = new BaseService<AdminLogEntity>(mc);
                var logs=bs.GetAll().Include(t=>t.AdminUser).ToList();
                List<AdminLogDTO> list = new List<AdminLogDTO>();
                foreach(var item in logs)
                {
                    AdminLogDTO dto = new AdminLogDTO();
                    dto.AdminUserId = item.AdminUserId;
                    dto.AdminUserName = item.AdminUser.UserName;
                    dto.CreateDateTime = item.CreateDateTime;
                    dto.Id = item.Id;
                    dto.Message = item.Message;
                    list.Add(dto);
                }
                return list.ToArray();
            }
        }
    }
}
