#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.IService
* 项目描述 ：
* 类 名 称 ：INewPlaceService
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.IService
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2018-12-10 10:11:47
* 更新时间 ：2018-12-10 10:11:47
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Hany 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;

namespace SDBSY.IService
{
    public interface INewPlaceService:IServiceSupport
    {
        NewPlaceDTO[] GetByParent(long parentId);
        NewPlaceDTO GetById(long id);
        NewPlaceDTO[] GetAll();
    }
}
