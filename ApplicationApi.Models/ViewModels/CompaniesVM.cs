using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.Models.ViewModels
{
    public class CompaniesVM
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public int TotalProducts { get; set; }
        public int ExpiredProducts { get; set; }
    }
}
