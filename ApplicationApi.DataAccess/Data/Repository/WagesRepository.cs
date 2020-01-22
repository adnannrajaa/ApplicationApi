using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
    public class WagesRepository : Repository<Wages>, IWagesRepository
    {
        private readonly ApplicationDbContext _db;

        public WagesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Wages wages)
        {
            var objFromDb = _db.Wages.FirstOrDefault(s => s.WagesId == wages.WagesId);
            objFromDb.CurrentSalary = wages.CurrentSalary;
            objFromDb.SalaryPaid = wages.SalaryPaid;
            objFromDb.Incentives = wages.Incentives;
            objFromDb.otherBenifits = wages.otherBenifits;
            objFromDb.Description = wages.Description;
            objFromDb.CreatedBy = wages.CreatedBy;
            objFromDb.CreatedDate = wages.CreatedDate;
            objFromDb.OutStanding = wages.OutStanding;
            _db.SaveChanges();

        }
    }
}
