using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.IService
{
    public interface IUserService:IServiceSupport
    {
        void UpdatePassword(long id,string pwd);
        UserDTO[] GetAll();
        long AddNew(string phoneNum,string password);
        bool CheckLogin(string phoneNum, string password);
        UserDTO GetById(long id);
        UserDTO GetByPhoneNum(string phoneNum);

        /// <summary>
        /// 记录一次登录失败
        /// </summary>
        /// <param name="id"></param>
        void MarkLoginError(long id);
        /// <summary>
        /// 重置登录失败信息
        /// </summary>
        /// <param name="id"></param>
        void ResetLoginError(long id);

        /// <summary>
        /// 判断用户是否已经被锁定
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsLocked(long id);
    }
}
