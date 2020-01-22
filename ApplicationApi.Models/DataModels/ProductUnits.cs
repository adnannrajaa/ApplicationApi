using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class ProductUnits
    {
        [Key]
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
    }
}
