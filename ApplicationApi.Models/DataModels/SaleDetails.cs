using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class SaleDetails
    {
        [Key]
        public int SaleDetailId { get; set; }
        public int InvoiceNo { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public DateTime TodayDate { get; set; }
        public int UnitId { get; set; }
        public int Total { get; set; }
    }
}
