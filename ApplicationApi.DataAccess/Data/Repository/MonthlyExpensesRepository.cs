 
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
    public class MonthlyExpensesRepository : Repository<MonthlyExpenses>, IMonthlyExpensesRepository
    {
        private readonly ApplicationDbContext _db;

        public MonthlyExpensesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MonthlyExpenses monthlyExpenses)
        {
            var objFromDb = _db.MonthlyExpense.FirstOrDefault(s => s.ExpensesId == monthlyExpenses.ExpensesId);

            objFromDb.TelephoneBill = monthlyExpenses.TelephoneBill;
            objFromDb.ElectricityBill = monthlyExpenses.ElectricityBill;
            objFromDb.Rent = monthlyExpenses.Rent;
            objFromDb.InternetCharges = monthlyExpenses.InternetCharges;
            objFromDb.month = monthlyExpenses.month;
            objFromDb.CreatedDate = monthlyExpenses.CreatedDate;
            objFromDb.CreatedById = objFromDb.CreatedById;
            _db.SaveChanges();
        }
    }
}
