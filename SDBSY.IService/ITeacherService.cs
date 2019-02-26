using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.IService
{
    public interface ITeacherService:IServiceSupport
    {
        CertificateDTO GetCertificate(long id);
        long AddNew(long certificateId,string url,string thumbUrl);//add pic
        void DelPics(long certificateId);
        CertificatePicDTO[] GetPics();
        CertificatePicDTO[] GetPics(long certificateId);
        CertificateDTO[] GetCertificates(long teacherId);
        CertificateDTO[] GetCertificates(long[] teacherIds);
        void Update(CertificateEditDTO dto);
        TeacherDTO GetByAdminId(long adminId);
        long AddNew(TeacherAddNewDTO dto);
        TeacherDTO[] GetAll();
        TeacherDTO GetById(long id);

        TeacherDTO[] GetById(long[] ids);
        //TeacherDTO GetByAdminId(long adminId);
        void Update(TeacherEditDTO dto);
        void DelTeacher(long teacherId);
        EducateDTO[] GetEducates(long teacherId);
        EducateDTO GetByEducateId(long id);
        long AddNewEducate(EducateDTO dto);
        void UpdateEducate(long id, string schoolName,int type, DateTime startTime, DateTime endTime);
        void DelEducate(long educateId);
        WorkDTO[] GetWorks(long teacherId);
        WorkDTO GetByWorkId(long id);
        long AddNewWork(WorkDTO dto);
        void UpdateWork(long id, string unitName,string jobName, DateTime startTime, DateTime endTime);
        void DelWork(long workId);
        long AddNew(CertificateAddNewDTO dto);
        //CertificateDTO[] GetEducateCertificates(long educateId);
        void DelCertificate(long certificateId);
        long AddNew(TrainingAddNewDTO dto);
        TrainingDTO[] GetTrainings(long teacherId);
        TrainingDTO[] GetTrainings(long[] teacherIds);
        TrainingDTO GetTraining(long id);
        void DelTraining(long id);
        void Update(TrainingEditDTO dto);
    }
}
