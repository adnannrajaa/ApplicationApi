using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using ApplicationApi.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApplicationApi.DataAccess.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public string hashPassword(string enteredPassword)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }
        
        public void LockToggle(string userId)
        {
            var userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            if(userFromDb.LockoutEnabled == false)
            {
                userFromDb.LockoutEnabled = true;
            }
            else
            {
                userFromDb.LockoutEnabled = false;
            }
            _db.SaveChanges();
        }

       

        public List<IdentityRole> GetAllRoles()
        {
            var roles = _db.Roles.ToList();
            return roles;
        }

        public IdentityRole GetRoleById(string id)
        {
            var roles = _db.Roles.FirstOrDefault(s=>s.Id== id);
            return roles;
        }
    }
}
