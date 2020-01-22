using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class DailyExpenses
    {
        [Key]
        public int ExpensesId { get; set; }
        public double FoodExpenses { get; set; }
        public double OtherExpenses { get; set; }
        public DateTime TodayDate { get; set; }
        public double Day { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
