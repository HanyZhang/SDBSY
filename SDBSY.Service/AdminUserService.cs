using SDBSY.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.Service.Entities;
using SDBSY.Service;
using SDBSY.Common;
using SDBSY.DTO;
using System.Data.Entity;

namespace SDBSY.Service
{
    public class AdminUserService : IAdminUserService
    {
        public long AddNew(string userName, string password,int role)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(mc);
                bool exsit= bs.GetAll().Any(t => t.UserName == userName);
                if (exsit)
                {
                    //throw new ArgumentException("已存在的用户名："+userName);
                    return -1;
                }
                AdminUserEntity user = new AdminUserEntity();
                user.UserName = userName;
                string salt = CommonHelper.CreateVerifyCode(5);//盐
                user.PasswordSalt = salt;
                string pwdHash = CommonHelper.CalcMD5(salt + password);//Md5(盐+用户密码)
                user.PasswordHash = pwdHash;
                user.Role = role;
                mc.AdminUsers.Add(user);
                mc.SaveChanges();
                return user.Id;
            }
        }

        public bool CheckLogin(string userName, string password)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                //出错不可怕，最怕的是有错但是表面“风平浪静”
                var user = bs.GetAll().SingleOrDefault(u => u.UserName == userName);
                if (user == null)
                {
                    return false;
                }
                string dbHash = user.PasswordHash;
                string userHash = CommonHelper.CalcMD5(user.PasswordSalt + password);
                //比较数据库中的PasswordHash是否和MD5(salt+用户输入密码)一致
                return userHash == dbHash;
            }
        }

        public bool CheckOldPassword(long id, string oldPassword)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<AdminUserEntity> bs
                    = new BaseService<AdminUserEntity>(mc);
                var user = bs.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("找不到用户，id=" + id);
                }
                string salt = user.PasswordSalt;
                string hash = user.PasswordHash;
                return CommonHelper.CalcMD5(salt + oldPassword) == hash;
            }
        }
        private AdminUserDTO ToDTO(AdminUserEntity admin)
        {
            AdminUserDTO dto = new AdminUserDTO();
            dto.CreateDateTime = admin.CreateDateTime;
            dto.Id = admin.Id;
            dto.UserName = admin.UserName;
            dto.Role = admin.Role;
            switch (dto.Role)
            {
                case YongHuShenFen.SuperAdmin:
                    dto.RoleName = "超级管理员";
                    break;
                case YongHuShenFen.Teacher:
                    dto.RoleName = "教师";
                    break;
                case YongHuShenFen.Worker:
                    dto.RoleName = "后勤工作人员";
                    break;
                default:
                    dto.RoleName = "未知";
                    break;             
            }
            return dto;
        }
        public AdminUserDTO[] GetAll()
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(mc);
                var admins= bs.GetAll();
                List<AdminUserDTO> list = new List<AdminUserDTO>();
                foreach(var admin in admins)
                {
                    list.Add(ToDTO(admin));
                }
                return list.ToArray();
            }
        }

        public AdminUserDTO GetById(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<AdminUserEntity> bs
                    = new BaseService<AdminUserEntity>(mc);
                var user = bs.GetById(id);
                if (user == null)
                {
                    return null;
                }
                return ToDTO(user);
            }
        }

        public AdminUserDTO GetByUserName(string userName)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<AdminUserEntity> bs
                    = new BaseService<AdminUserEntity>(mc);
                var user = bs.GetAll().SingleOrDefault(u => u.UserName == userName);
                if (user == null)
                {
                    return null;
                }
                else
                {
                    AdminUserDTO dto = new AdminUserDTO();
                    dto.CreateDateTime = user.CreateDateTime;
                    dto.Id = user.Id;
                    dto.UserName = user.UserName;
                    return dto;
                }
            }
        }

        public void UpdatePassword(long id, string newPassword)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<AdminUserEntity> bs
                    = new BaseService<AdminUserEntity>(mc);
                var user = bs.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("找不到用户，id=" + id);
                }
                string salt = CommonHelper.CreateVerifyCode(5);//盐
                user.PasswordSalt = salt;
                string pwdHash = CommonHelper.CalcMD5(salt + newPassword);//Md5(盐+用户密码)
                user.PasswordHash = pwdHash;
                mc.SaveChanges();
            }
        }

        public bool HasPermission(long adminUserId, string permissionName)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<AdminUserEntity> bs
                    = new BaseService<AdminUserEntity>(ctx);
                var user = bs.GetAll().Include(u => u.Roles)
                    .AsNoTracking().SingleOrDefault(u => u.Id == adminUserId);
                if (user == null)
                {
                    throw new ArgumentException("找不到id=" + adminUserId + "的用户");
                }
                //每个Role都有一个Permissions属性
                //Roles.SelectMany(r => r.Permissions)就是遍历Roles的每一个Role
                //然后把每个Role的Permissions放到一个集合中
                //IEnumerable<PermissionEntity>
                return user.Roles.SelectMany(r => r.Permissions)
                    .Any(p => p.Name == permissionName);
            }
        }

        public void MarkDeleted(long adminUserId)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<AdminUserEntity> bs
                    = new BaseService<AdminUserEntity>(ctx);
                bs.MarkDeleted(adminUserId);
            }
        }
    }
}
