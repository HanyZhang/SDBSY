using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;

namespace SDBSY.IService
{
    public interface IInvoiceService:IServiceSupport
    {
        long AddNew(InvoiceAddNewDTO dto);
        void Shenhe(long id,int status,string msg);
        void Update(InvoiceEditDTO dto);
        void Delete(long id);
        long AddNewPic(InvoicePicAddNewDTO dto);
        InvoicePicDTO[] GetAllPics();
        InvoicePicDTO[] GetPics(long id);
        InvoiceDTO GetById(long id);
        InvoiceDTO[] GetByTeacherId(long teacherId);
        InvoiceDTO[] GetByClassId(long classId);
        InvoiceDTO[] GetAll();
        void DeletePics(long invoiceId);
    }
}
