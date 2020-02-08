using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using ApplicationApi.SecureWebApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApi
{
    public interface IUserService
    {

        ApplicationUser Authenticate(ApplicationUser _User);
        IEnumerable<ApplicationUser> GetAll();

    }

    public class UserService : IUserService
    {
        private readonly AppSetting _appsettings;
        private readonly IUnitOfWork _db;

        public UserService(IOptions<AppSetting> appSettings, IUnitOfWork db)
        {
            _appsettings = appSettings.Value;
            _db = db;
        }
        public ApplicationUser Authenticate(ApplicationUser _User)
        {
            var user = _db.User.GetFirstOrDefault(u => u.Email == _User.Email);
            // return null if user not found
            if (user == null)
                return null;
             if(decodeHashPassword(user.PasswordHash, _User.PasswordHash))
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var RoleName = _db.User.GetRoleById(user.RoleId).Name;
                var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
             
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, RoleName)

                    }),
                    Expires = DateTime.UtcNow.AddHours(12),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return user;
            }
            else
            {
                return null;
            }
           
        }
    public IEnumerable<ApplicationUser> GetAll()
        {
            return _db.User.GetAll();
        }
        public bool decodeHashPassword(string existingPassword, string enteredPassword)
        {
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(existingPassword);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

    }
}
