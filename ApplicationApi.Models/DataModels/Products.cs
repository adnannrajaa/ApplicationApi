using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double CostPrice { get; set; }
        public double WholeSalePrice { get; set; }
        public double RetailPrice { get; set; }
        public DateTime ExpeiryDate { get; set; }

        public bool Status { get; set; }
    }
}
