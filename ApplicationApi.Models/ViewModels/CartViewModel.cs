using ApplicationApi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.Models.ViewModels
{
    public class CartViewModel
    {
        public IList<Products> ProductsDetails { get; set; }
        public CustomerDetails CustomerDetail { get; set; }

    }
}
