using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;

namespace SDBSY.IService
{
    public interface IStudentService:IServiceSupport
    {
        void MarkDeleted(long id);//软删除
        StudentDTO GetByIdCardNum(string idCardNum);
        long AddNew(StudentAddNewDTO dto);
        void UpdateStudent(StudentEditDTO dto);
        void UpdateClass(long id,long classId);//升班、设置班级
        long AddGoUpRecord(long stuId, long? oldClassId, long newClassId,DateTime dateTime);//添加一条升班记录
        void UpdateDateTime(long id, DateTime date);//入园时间设置
        void UpdateStatus(long id, int status);//设置状态
        void StayInClass(long id,bool b);//留班，恢复
        void FinishSchool(long id,DateTime time);//毕业
        StudentDTO[] GetAll();
        StudentDTO GetById(long id);
        StudentDTO[] GetByUserId(long userId);//根据登录用户获取幼儿信息，有可能存在多个
        StudentDTO[] GetByClassId(long classId);
        void ShenHe(long id,bool pass);//审核
        StudentDTO[] GetById(long[] selectIds);
        GoUpRecordDTO[] GetGoUpRecords(long stuId);
    }
}
