
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ApplicationApi.DataAccess.Data.Repository
{
    public class SalesRepository : Repository<Sales>, ISalesRepository
    {
        private readonly ApplicationDbContext _db;

        public SalesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Sales sale)
        {
            var objFromDb = _db.Sale.FirstOrDefault(s => s.SaleId == sale.SaleId);
            objFromDb.CustomerOrShopId = sale.CustomerOrShopId;
            objFromDb.SaleType = sale.SaleType;
            objFromDb.CashReceived = sale.CashReceived;
            objFromDb.RemainingAmount = sale.RemainingAmount;
            objFromDb.Discount = sale.Discount;
            objFromDb.DueDate = sale.DueDate;
            objFromDb.InvoiceDate = sale.InvoiceDate;
            objFromDb.TotalBill = sale.TotalBill;
            objFromDb.BookerOrUserId = sale.BookerOrUserId;
            _db.SaveChanges();
        }
    }
}
