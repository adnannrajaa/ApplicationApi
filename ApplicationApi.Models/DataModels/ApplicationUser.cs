using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }
        public double HiringSalary { get; set; }
        public double CurrentSalary { get; set; }
        public string Image { get; set; }
        public string Section { get; set; }
        public string Route { get; set; }
        public string Designation { get; set; }
        public string HireDate { get; set; }
        public string RoleId { get; set; }
        public string CompanyId { get; set; }

        public DateTime currentDateTime { get; set; }






    }
}
