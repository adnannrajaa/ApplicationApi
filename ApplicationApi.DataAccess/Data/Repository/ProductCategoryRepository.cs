using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _db.ProductCategories.Select(i => new SelectListItem()
            {
                Text = i.CategoryName,
                Value = i.CategoryId.ToString()

            });
        }

        public void Update(ProductCategory category)
        {
            var objFromDb = _db.ProductCategories.FirstOrDefault(s => s.CategoryId == category.CategoryId);
            objFromDb.CategoryName = category.CategoryName;
            objFromDb.CategoryDescription = category.CategoryDescription;
            _db.SaveChanges();

        }
    }
}
