using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.IService
{
    public interface IFoodService:IServiceSupport
    {
        long AddNew(string name,string unit,string supplier);
        FoodDTO[] GetAll();
        FoodDTO GetById(long id);
        void Delete(long id);
        long AddNewBuyRecord(long foodId, decimal unitPrice, decimal amount, DateTime buyTime, decimal totalPrice, string remark);
        FoodBuyRecordDTO[] GetAllRecords(long foodId);
        FoodBuyRecordDTO[] GetAllRecords(DateTime startTime,DateTime endTime);
        FoodBuyRecordDTO[] GetAllRecords();
        FoodBuyRecordDTO[] GetAllRecords(long[] ids);
        void RecordDelete(long id);
    }
}
