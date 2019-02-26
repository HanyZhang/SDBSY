using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.IService
{
    public interface IAdminUserService:IServiceSupport
    {
        //根据id获取DTO
        AdminUserDTO GetById(long id);
        long AddNew(string userName, string password,int role);
        bool CheckLogin(string userName, string password);
        AdminUserDTO GetByUserName(string userName);
        bool CheckOldPassword(long id, string oldPassword);
        void UpdatePassword(long id, string newPassword);
        AdminUserDTO[] GetAll();
        //判断adminUserId这个用户是否有permissionName这个权限项（举个例子）
        //HasPermission(3,"User.Add")
        bool HasPermission(long adminUserId, string permissionName);
        //软删除
        void MarkDeleted(long adminUserId);
    }
}
