 
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ApplicationApi.DataAccess.Data.Repository
{
    public class DailyExpensesRepository : Repository<DailyExpenses>, IDailyExpensesRepository
    {
        private readonly ApplicationDbContext _db;

        public DailyExpensesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DailyExpenses dailyExpenses)
        {
            var objFromDb = _db.DailyExpense.FirstOrDefault(s => s.ExpensesId == dailyExpenses.ExpensesId);
            objFromDb.FoodExpenses = dailyExpenses.FoodExpenses;
            objFromDb.OtherExpenses = dailyExpenses.OtherExpenses;
            objFromDb.TodayDate = dailyExpenses.TodayDate;
            objFromDb.Day = dailyExpenses.Day;
            objFromDb.CreatedDate = dailyExpenses.CreatedDate;
            objFromDb.CreatedByUserId = dailyExpenses.CreatedByUserId;
            _db.SaveChanges();
        }
    }
}
