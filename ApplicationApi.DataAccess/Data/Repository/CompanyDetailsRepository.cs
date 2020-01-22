 
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
    public class CompanyDetailsRepository : Repository<CompanyDetails>, ICompanyDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(CompanyDetails company)
        {
            var objFromDb = _db.CompanyDetail.FirstOrDefault(s => s.CompanyId == company.CompanyId);
            objFromDb.CompanyName = company.CompanyName;
            objFromDb.Description = company.Description;
            _db.SaveChanges();
        }
    }
}
