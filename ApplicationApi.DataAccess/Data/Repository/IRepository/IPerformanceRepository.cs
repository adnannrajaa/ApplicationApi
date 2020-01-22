using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository.IRepository
{
    public interface IPerformanceRepository : IRepository<EmployeePerformance>
    {
        void Update(EmployeePerformance performance);
    }
}
