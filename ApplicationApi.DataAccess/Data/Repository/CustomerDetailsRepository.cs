 
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
    public class CustomerDetailsRepository : Repository<CustomerDetails>, ICustomerDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CustomerDetails customer)
        {
            var objFromDb = _db.CustomerDetail.FirstOrDefault(s => s.CustomerId == customer.CustomerId);
            objFromDb.CustomerName = customer.CustomerName;
            objFromDb.ContactNumber = customer.ContactNumber;
            objFromDb.Address = customer.Address;

            _db.SaveChanges();
        }
    }
}
