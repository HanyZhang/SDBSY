using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;

namespace SDBSY.IService
{
    public interface IDataDictionaryService: IServiceSupport
    {
        bool Exsits(string name, string value);
        DataDictionaryDTO[] GetByName(string name);
        DataDictionaryDTO GetSingleByName(string name);
        DataDictionaryDTO GetById(long id);
        long AddNew(string name, string value);
        void UpdateValue(long id, string value);//
        void UpdateValue( string name, string value);
        void MarkDeleted(long id);

    }
}
