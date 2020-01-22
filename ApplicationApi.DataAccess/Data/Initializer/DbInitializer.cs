using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationApi.Models;
using ApplicationApi.Utility;
using ApplicationApi.Models.DataModels;

namespace ApplicationApi.DataAccess.Data.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }catch(Exception ex)
            {

            }

            if (_db.Roles.Any(r => r.Name == Roles.ADMIN)) return;

            _roleManager.CreateAsync(new IdentityRole(Roles.ADMIN)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.DIRECTOR)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.MANAGER)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.SUPERVISOR)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.EMPLOYEE)).GetAwaiter().GetResult();

            //_userManager.CreateAsync(new ApplicationUser
            //{
            //    UserName = "admin@gmail.com",
            //    Email = "admin@gmail.com",
            //    EmailConfirmed = true,
            //    Name = "Adnan Raja"

            //}, "1").GetAwaiter().GetResult();

            //ApplicationUser user = _db.ApplicationUsers.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();
            //_userManager.AddToRoleAsync(user, Roles.ADMIN).GetAwaiter().GetResult();

        }
    }
}
