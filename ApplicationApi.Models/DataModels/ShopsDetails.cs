using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class ShopsDetails
    {
        [Key]
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string City { get; set; }
        public string Section { get; set; }
        public string Route { get; set; }
        public string ShopOwnerName { get; set; }
        public string FullAddress { get; set; }
        public string ContactNumber { get; set; }
        public string RecentDelivery { get; set; }
        public string IsActive { get; set; }
    }
}
