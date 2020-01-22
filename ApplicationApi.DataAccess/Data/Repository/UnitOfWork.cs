using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new ProductCategoryRepository(_db);
            Company = new CompanyDetailsRepository(_db);
            Customer = new CustomerDetailsRepository(_db);
            DailyExpenses = new DailyExpensesRepository(_db);
            MonthlyExpenses = new MonthlyExpensesRepository(_db);
            Products = new ProductsRepository(_db);
            ProductUnits = new ProductsUnitsRepository(_db);
            SalesDetail = new SaleDetailsRepository(_db);
            Sale = new SalesRepository(_db);
            ShopDetail = new ShopesDetailsRepository(_db);
            User = new UserRepository(_db);
            wages = new WagesRepository(_db);
            AllowedLinks = new AllowedLinksRepository(_db);
            ActionLinks = new ActionLinksRepository(_db);
            Performance = new PerformanceRepository(_db);
            SP_Call = new SP_Call(_db);
        }
        public IProductCategoryRepository Category { get; private set; }

        public ICompanyDetailsRepository Company { get; private set; }

        public ICustomerDetailsRepository Customer { get; private set; }

        public IDailyExpensesRepository DailyExpenses { get; private set; }

        public IMonthlyExpensesRepository MonthlyExpenses { get; private set; }

        public IProductsRepository Products { get; private set; }
        public IProductsUnitsRepository ProductUnits { get; private set; }

        public ISaleDetailsRepository SalesDetail { get; private set; }

        public ISalesRepository Sale { get; private set; }
        public IShopesDetailsRepository ShopDetail { get; private set; }

        public IUserRepository User { get; private set; }
        public IWagesRepository wages { get; private set; }

        public IAllowedLinksRepository AllowedLinks { get; private set; }
        public IActionLinksRepository ActionLinks { get; private set; }
        public IPerformanceRepository Performance { get; private set; }
        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
