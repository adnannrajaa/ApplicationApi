using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.Models.ViewModels
{
    public class UsersVM
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Section { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string RoleName { get; set; }
        public string IsLocked { get; set; }
    }
}
