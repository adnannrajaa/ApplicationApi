using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
    public class SaleDetailsRepository : Repository<SaleDetails>, ISaleDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public SaleDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SaleDetails saleDetail)
        {
            var objFromDb = _db.SaleDetail.FirstOrDefault(s => s.SaleDetailId == saleDetail.SaleDetailId);
            objFromDb.InvoiceNo = saleDetail.InvoiceNo;
            objFromDb.ProductId = saleDetail.ProductId;
            objFromDb.Quantity = saleDetail.Quantity;
            objFromDb.Price = saleDetail.Price;
            objFromDb.TodayDate = saleDetail.TodayDate;
            objFromDb.UnitId = saleDetail.UnitId;
            objFromDb.Total = saleDetail.Total;
            _db.SaveChanges();
        }
    }
}
