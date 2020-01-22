using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class CompanyDetails
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
    }
}
