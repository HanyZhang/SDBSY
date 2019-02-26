#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.Service
* 项目描述 ：
* 类 名 称 ：InvoiceService
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.Service
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2019-01-09 10:48:24
* 更新时间 ：2019-01-09 10:48:24
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Hany 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.Common;
using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.Service.Entities;

namespace SDBSY.Service
{
    class InvoiceService : IInvoiceService
    {
        public long AddNew(InvoiceAddNewDTO dto)
        {
            var entity=new InvoiceEntity();
            entity.ClassId = dto.ClassId;
            entity.Detail = dto.Detail;
            entity.GoodsName = dto.GoodsName;
            entity.TeacherId = dto.TeacherId;
            entity.Total = dto.Total;
            entity.BuyDateTime = dto.BuyDateTime;
            entity.Status = ShenHeZhuangTai.MoRen;
            using (var mc=new MyDBContext())
            {
                mc.Invoices.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        public void Delete(long id)
        {
            using (var mc = new MyDBContext())
            {
                var bs=new BaseService<InvoiceEntity>(mc);
                var invoice= bs.GetById(id);
                if (invoice == null)
                {
                    throw  new ArgumentException("不存在的发票信息，id="+id);
                }
                bs.MarkDeleted(id);
                mc.SaveChanges();
            }
        }

        private InvoiceDTO ToDto(InvoiceEntity entity)
        {
            var dto=new InvoiceDTO();
            dto.Id = entity.Id;
            dto.TeacherId = entity.TeacherId;
            dto.TeacherName = entity.Teacher.Name;
            dto.BuyDateTime = entity.BuyDateTime;
            dto.BuyDateTimeStr = entity.BuyDateTime.ToString("yyyy-MM-dd");
            dto.CreateDateTime = entity.CreateDateTime;
            dto.ClassId = entity.ClassId;
            dto.ClassName = entity.Class.Value;
            dto.GoodsName = entity.GoodsName;
            dto.Detail = entity.Detail;
            dto.Total = entity.Total;
            dto.TotalStr = entity.Total.ToString();
            dto.Status = entity.Status;
            dto.NoPassReason = entity.NoPassReason;
            switch (entity.Status)
            {
                case ShenHeZhuangTai.MoRen:
                    dto.StatusStr = "审核中";
                    break;
                case ShenHeZhuangTai.BoHui:
                    dto.StatusStr = "未通过";
                    break;
                case ShenHeZhuangTai.TongGuo:
                    dto.StatusStr = "通过";
                    break;
                default:
                    dto.StatusStr = "未知状态";
                    break;
            }
            return dto;
        }

        public InvoiceDTO[] GetAll()
        {
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<InvoiceEntity>(mc);
                var invoices= bs.GetAll().Include(t => t.Class).Include(t => t.Teacher).ToList();
                var list = new List<InvoiceDTO>();
                foreach (var invoice in invoices)
                {
                    list.Add(ToDto(invoice));
                }

                return list.ToArray();
            }
        }

        public InvoiceDTO[] GetByClassId(long classId)
        {
            throw new NotImplementedException();
        }

        public InvoiceDTO GetById(long id)
        {
            using (var mc=new MyDBContext())
            {
                var bs=new BaseService<InvoiceEntity>(mc);
                var invoice= bs.GetAll().Include(t => t.Teacher).Include(t => t.Class).SingleOrDefault(t => t.Id == id);
                return invoice == null ? null : ToDto(invoice);
                //throw new NotImplementedException();
            }
        }

        public InvoiceDTO[] GetByTeacherId(long teacherId)
        {
            using (var mc =new MyDBContext())
            {
                var bs=new BaseService<InvoiceEntity>(mc);
                var invoices= bs.GetAll().Include(t => t.Teacher).Include(t => t.Class).Where(t => t.TeacherId == teacherId).ToList();
                var list = new List<InvoiceDTO>();
                foreach (var invoice in invoices)
                {
                    list.Add(ToDto(invoice));
                }

                return list.ToArray();
            }
        }

        public void Shenhe(long id, int status, string msg)
        {
            using (var mc = new MyDBContext())
            {
                var bs =new BaseService<InvoiceEntity>(mc);
                var invoice = bs.GetById(id);
                if (invoice == null)
                {
                    throw  new ArgumentException("不存在票据信息，id="+id);
                }

                invoice.Status = status;
                invoice.NoPassReason = status == ShenHeZhuangTai.TongGuo ? string.Empty : msg;
                mc.SaveChanges();
            }
        }

        public long AddNewPic(InvoicePicAddNewDTO dto)
        {
            var entity =new InvoicePicEntity();
            entity.Url = dto.Url;
            entity.ThumbUrl = dto.ThumbUrl;
            entity.InvoiceId = dto.InvocieId;
            using (var mc = new MyDBContext())
            {
                mc.InvoicePics.Add(entity);
                mc.SaveChanges();
                return entity.Id;
            }
        }

        private InvoicePicDTO ToDto(InvoicePicEntity entity)
        {
            var dto=new InvoicePicDTO();
            dto.Id = entity.Id;
            dto.InvoiceId = entity.InvoiceId;
            dto.Url = entity.Url;
            dto.ThumbUrl = entity.ThumbUrl;
            return dto;
        }
        public InvoicePicDTO[] GetAllPics()
        {
            using (var mc = new MyDBContext())
            {
                var bs =new BaseService<InvoicePicEntity>(mc);
                var pics=  bs.GetAll().Include(t => t.Invoice).ToList();
                var list=new List<InvoicePicDTO>();
                foreach (var pic in pics)
                {
                    list.Add(ToDto(pic));
                }

                return list.ToArray();
            }
        }

        public InvoicePicDTO[] GetPics(long id)
        {
            using (var mc = new MyDBContext())
            {
                var bs = new BaseService<InvoicePicEntity>(mc);
                var pics = bs.GetAll().Include(t => t.Invoice).Where(t=>t.InvoiceId==id).ToList();
                var list = new List<InvoicePicDTO>();
                foreach (var pic in pics)
                {
                    list.Add(ToDto(pic));
                }

                return list.ToArray();
            }
        }

        public void Update(InvoiceEditDTO dto)
        {
            using (var mc = new MyDBContext())
            {
                var bs=new BaseService<InvoiceEntity>(mc);
                var invoice= bs.GetById(dto.Id);
                if (invoice == null)
                {
                    throw  new ArgumentException("不存在的票据信息，Id="+dto.Id);
                }

                invoice.BuyDateTime = dto.BuyDateTime;
                invoice.ClassId = dto.ClassId;
                invoice.Detail = dto.Detail;
                invoice.GoodsName = dto.GoodsName;
                invoice.Total = dto.Total;
                mc.SaveChanges();
            }
        }

        public void DeletePics(long invoiceId)
        {
            using (var mc = new MyDBContext())
            {
                var bs=new BaseService<InvoicePicEntity>(mc);
                var pics = bs.GetAll().Where(t => t.InvoiceId == invoiceId).ToList();
                if (pics.Any())
                {
                    foreach (var pic in pics)
                    {
                        pic.IsDeleted = true;
                    }

                    mc.SaveChanges();
                }
                
            }
        }
    }
}
