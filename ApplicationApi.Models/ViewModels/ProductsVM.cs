using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.Models.ViewModels
{
    public class ProductsVM
    {
        public int ProductId { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
        public string UnitName { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double CostPrice { get; set; }
        public double WholeSalePrice { get; set; }
        public double RetailPrice { get; set; }
        public DateTime ExpeiryDate { get; set; }

        public bool Status { get; set; }
    }
}
