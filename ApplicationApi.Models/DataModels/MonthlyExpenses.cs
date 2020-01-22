using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class MonthlyExpenses
    {
        [Key]
        public int ExpensesId { get; set; }
        public double TelephoneBill { get; set; }
        public double ElectricityBill { get; set; }
        public double Rent { get; set; }
        public double InternetCharges { get; set; }
        public string month { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
    }
}
