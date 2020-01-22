using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class Sales
    {
        [Key]
        public int SaleId { get; set; }

        public int CustomerOrShopId { get; set; }
        public int SaleType { get; set; }
        public double CashReceived { get; set; }
        public double RemainingAmount { get; set; }
        public double Discount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double TotalBill { get; set; }
        public int BookerOrUserId { get; set; }
    }
}
