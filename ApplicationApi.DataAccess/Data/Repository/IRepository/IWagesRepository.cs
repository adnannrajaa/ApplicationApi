using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository.IRepository
{
    public interface IWagesRepository : IRepository<Wages>
    {
        void Update(Wages wages);
    }
}
