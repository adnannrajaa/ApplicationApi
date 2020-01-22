﻿ 
using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository.IRepository
{
    public interface IDailyExpensesRepository : IRepository<DailyExpenses>
    {
        void Update(DailyExpenses dailyExpenses);
    }
   
}
