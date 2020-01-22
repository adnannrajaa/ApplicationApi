 
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository.IRepository
{
    public interface ICustomerDetailsRepository:IRepository<CustomerDetails>
    {
        void Update(CustomerDetails customer);
    }
}
