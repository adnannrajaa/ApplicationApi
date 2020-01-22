 
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
    public class ProductsRepository : Repository<Products>, IProductsRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Products product)
        {
            var objFromDb = _db.Product.FirstOrDefault(s => s.ProductId == product.ProductId);
            objFromDb.ProductName = product.ProductName;
            objFromDb.CompanyId = product.CompanyId;
            objFromDb.CategoryId = product.CategoryId;
            objFromDb.UnitId = product.UnitId;
            objFromDb.Quantity = product.Quantity;
            objFromDb.CostPrice = product.CostPrice;
            objFromDb.WholeSalePrice = product.WholeSalePrice;
            objFromDb.RetailPrice = product.RetailPrice;
            objFromDb.ExpeiryDate = product.ExpeiryDate;
            _db.SaveChanges();

        }
    }
}
