 
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository.IRepository
{
    public interface IProductsUnitsRepository : IRepository<ProductUnits>
    {
        void Update(ProductUnits productUnits);
    }

}
