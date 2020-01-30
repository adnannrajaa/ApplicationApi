using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using ApplicationApi.Models.ViewModels;
using ApplicationApi.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationApi.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplictionUsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public ApplictionUsersController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        //get All the users axcept Logged in User
        [HttpGet("GetAllUsers")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.Name);

            var objFromDb = _unitOfWork.User.GetAll(u => u.Id != claims.Value);

            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;


        }

        //get All the users axcept Logged in User
        [Authorize(Roles =Roles.EMPLOYEE)]
        [HttpGet("DisplayUsersInfo")]
        public IActionResult GetAllUserInfo()
        {
            JsonResult result = new JsonResult(new { });
            DynamicParameters parameter = new DynamicParameters();
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.Name);
            parameter.Add("@UserId", claims.Value);
            var objFromDb = _unitOfWork.SP_Call.ReturnList<UsersVM>(SP.usp_UsersGetAllUsers, parameter);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //get All the users Roles.
        [HttpGet("GetAllUserRoles")]
        public IActionResult GetAllUserRoles()
        {
            JsonResult result = new JsonResult(new { });
            var Roles = _unitOfWork.User.GetAllRoles();
            if (Roles != null)
            {
                result.Value = new { Data = true, UserRoles = Roles };
                return result;
            }
            result.Value = new { Data = false };
            return result;
        }
        [AllowAnonymous]
        [HttpPost("VerifyUserLogin")]
        public IActionResult LoginUser(ApplicationUser user)
        {
            JsonResult result = new JsonResult(new { });
            var userToken = _userService.Authenticate(user);
            if(userToken != null)
            {
                var token = userToken.Token;
                result.Value = new { Data = true, myToken = token };
                return result;
            }
            result.Value = new { Data = false };
            return result;
        }

        [HttpPost("CreateNewUser")]
        public IActionResult CreateUser([FromForm]ApplicationUser user)
        {
            JsonResult result = new JsonResult(new { });
            var password = _unitOfWork.User.hashPassword(user.PasswordHash);
            user.PasswordHash = password;
            _unitOfWork.User.Add(user);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        [HttpGet("LogoutUser")]
        public IActionResult Logoff()
        {
            JsonResult result = new JsonResult(new { });
            HttpContext.Session.Clear();
            result.Value = new { Data = true };
            return result;
        }
        [HttpGet("LockToggle/{id}")]
        public IActionResult LockToggle(string Id)
        {
            JsonResult result = new JsonResult(new { });

            if (Id == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.User.LockToggle(Id);
            result.Value = new { Data = true };
            return result;
        }
    }
}