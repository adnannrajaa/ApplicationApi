using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
    public class PerformanceRepository : Repository<EmployeePerformance>, IPerformanceRepository
    {
        private readonly ApplicationDbContext _db;

        public PerformanceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(EmployeePerformance performance)
        {
            var objFromDb = _db.Performances.FirstOrDefault(s => s.PerformanceId == performance.PerformanceId);
            objFromDb.UserId = performance.UserId;
            objFromDb.Month = performance.Month;
            objFromDb.TotalTarget = performance.TotalTarget;
            objFromDb.TargetAchieved = performance.TargetAchieved;
            objFromDb.Completion = performance.Completion;
            objFromDb.IsMonthClosed = performance.IsMonthClosed;
            _db.SaveChanges();
        }
    }
}
