using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.IService
{
    public interface IRoleService:IServiceSupport
    {
        //新增角色
        long AddNew(string roleName);
        void Update(long roleId, string roleName);
        void MarkDeleted(long roleId);
        RoleDTO GetById(long id);
        RoleDTO GetByName(string name);
        RoleDTO[] GetAll();
        //给用户adminuserId增加权限roleIds
        void AddRoleIds(long adminUserId, long[] roleIds);

        //更新权限，先删再加
        void UpdateRoleIds(long adminUserId, long[] roleIds);

        //获取用户的角色
        RoleDTO[] GetByAdminUserId(long adminUserId);
    }
}
