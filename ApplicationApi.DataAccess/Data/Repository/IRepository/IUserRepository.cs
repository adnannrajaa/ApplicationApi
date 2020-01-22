using ApplicationApi.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository.IRepository
{
   public interface IUserRepository : IRepository<ApplicationUser>
    {
        string hashPassword(string enteredPassword);
        void LockToggle(string userId);
        List<IdentityRole> GetAllRoles();
        IdentityRole GetRoleById(string id);
    }
}
