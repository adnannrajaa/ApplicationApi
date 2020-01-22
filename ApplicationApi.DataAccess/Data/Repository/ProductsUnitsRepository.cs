 
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ApplicationApi.DataAccess.Data.Repository
{
    public class ProductsUnitsRepository : Repository<ProductUnits>, IProductsUnitsRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductsUnitsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductUnits productUnits)
        {
            var objFromDb = _db.ProductUnit.FirstOrDefault(s => s.UnitId == productUnits.UnitId);
            objFromDb.UnitName = productUnits.UnitName;
            objFromDb.Description = productUnits.Description;
            _db.SaveChanges();
        }
    }
}
