using SDBSY.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;
using SDBSY.Service.Entities;
using SDBSY.Common;

namespace SDBSY.Service
{
    public class UserService : IUserService
    {
        public long AddNew(string phoneNum, string password)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                bool exsit= bs.GetAll().Any(t => t.PhoneNum == phoneNum);
                if(exsit)
                {
                    throw new ArgumentException("手机号已被占用："+phoneNum);
                }
                UserEntity entity = new UserEntity();
                entity.PhoneNum = phoneNum;
                string salt = CommonHelper.CreateVerifyCode(5);
                string pwdHash = CommonHelper.CalcMD5(salt + password);
                entity.PasswordHash = pwdHash;
                entity.PasswordSalt = salt;
                ctx.Users.Add(entity);
                ctx.SaveChanges();
                return entity.Id;
            }
        }

        public bool CheckLogin(string phoneNum, string password)
        {
            using(MyDBContext ctx = new MyDBContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                var user = bs.GetAll().SingleOrDefault(u => u.PhoneNum == phoneNum);
                if (user == null)
                {
                    return false;
                }
                else
                {
                    string dbPwdHash = user.PasswordHash;
                    string salt = user.PasswordSalt;
                    string userPwdHash = CommonHelper.CalcMD5(salt + password);
                    return dbPwdHash == userPwdHash;
                }
            }
        }
        private UserDTO ToDTO(UserEntity entity)
        {
            UserDTO dto = new UserDTO();
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Id = entity.Id;
            dto.LastLoginErrorDateTime = entity.LastLoginErrorDateTime;
            dto.LoginErrorTimes = entity.LoginErrorTimes;
            dto.PhoneNum = entity.PhoneNum;
            return dto;
        }
        public UserDTO GetById(long id)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                var entity= bs.GetById(id);
                if (entity == null)
                    return null;
                else
                    return ToDTO(entity);
            }
        }

        public UserDTO GetByPhoneNum(string phoneNum)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                var entity = bs.GetAll().SingleOrDefault(t => t.PhoneNum == phoneNum);
                if (entity == null)
                    return null;
                else
                    return ToDTO(entity);
            }
        }

        public void MarkLoginError(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(mc);
                var user = bs.GetById(id);
                if(user==null)
                {
                    throw new ArgumentException("用户不存在： " + id);
                }
                user.LoginErrorTimes++;
                user.LastLoginErrorDateTime = DateTime.Now;
                mc.SaveChanges();
            }
        }

        public bool IsLocked(long id)
        {
            var user = GetById(id);
            if (user == null)
            {
                throw new ArgumentException("用户不存在" + id);
            }
            //登录错误次数为5次，最后一次错误时间在30分钟内
            return user.LoginErrorTimes >= 5 && user.LastLoginErrorDateTime >= DateTime.Now.AddMinutes(-30);
        }

        public void ResetLoginError(long id)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(mc);
                var user = bs.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("用户不存在： " + id);
                }
                user.LoginErrorTimes=0;
                user.LastLoginErrorDateTime = null;
                mc.SaveChanges();
            }

        }

        public UserDTO[] GetAll()
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(mc);
                var users=bs.GetAll();
                List<UserDTO> list = new List<UserDTO>();
                if (users != null && users.Count() > 0)
                {
                    foreach (var item in users)
                    {
                        list.Add(ToDTO(item));
                    }
                }
                return list.ToArray();
            }
        }

        public void UpdatePassword(long id,string pwd)
        {
            using (MyDBContext mc = new MyDBContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(mc);
                var user= bs.GetById(id);
                if(user==null)
                {
                    throw new ArgumentException("不存在的id："+id);
                }
                string salt = CommonHelper.CreateVerifyCode(5);
                user.PasswordSalt = salt;
                user.PasswordHash = CommonHelper.CalcMD5(salt + pwd);
                mc.SaveChanges();
            }
        }
    }
}
