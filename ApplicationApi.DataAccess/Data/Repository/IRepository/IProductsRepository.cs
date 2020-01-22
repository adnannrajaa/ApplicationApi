 
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository.IRepository
{
    public interface IProductsRepository : IRepository<Products>
    {
        void Update(Products product);
    }
}
