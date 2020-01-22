using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyDetailsRepository Company { get; }
        ICustomerDetailsRepository Customer { get; }
        IDailyExpensesRepository DailyExpenses { get; }
        IMonthlyExpensesRepository MonthlyExpenses { get; }
        IProductCategoryRepository Category { get; }
        IProductsRepository Products { get; }
        IProductsUnitsRepository ProductUnits { get; }
        ISaleDetailsRepository SalesDetail { get; }
        ISalesRepository Sale { get; }
        IShopesDetailsRepository ShopDetail { get; }
        IWagesRepository wages { get; }
        IUserRepository User { get; }
        IAllowedLinksRepository AllowedLinks { get; }
        IActionLinksRepository ActionLinks { get; }
        IPerformanceRepository Performance { get; }

        ISP_Call SP_Call { get; }
        void Save();
    }
}
