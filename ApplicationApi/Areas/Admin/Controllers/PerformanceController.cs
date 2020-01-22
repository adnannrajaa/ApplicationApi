using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationApi.CustomAttributes;
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using ApplicationApi.Models.ViewModels;
using ApplicationApi.Utility;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationApi.Areas.Admin.Controllers
{
    [Authorize(StaticControllers.Category)]
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PerformanceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //get All the Employee Performance
        [HttpGet("GetEmployeePerformance")]
        public IActionResult GetEmployeePerformance()
        {
            JsonResult result = new JsonResult(new { });
            DynamicParameters parameter = new DynamicParameters();
            var objFromDb = _unitOfWork.SP_Call.ReturnList<PerformanceVM>(SP.usp_GetAllEmployeePerformance);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { result = objFromDb };
            return result;
        }


        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
            EmployeePerformance performance = new EmployeePerformance();
            JsonResult result = new JsonResult(new { });
            if (id == 0)
            {
                result.Value = new { Data = true, message = "Its Insert Call" };
                return result;
            }
            performance = _unitOfWork.Performance.Get(id);
            if (performance == null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = performance, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveTargetData")]
        public IActionResult Upsert([FromBody]EmployeePerformance performance)
        {
            JsonResult result = new JsonResult(new { });
            if (performance.PerformanceId == 0)
            {
                _unitOfWork.Performance.Add(performance);
            }
            else
            {
                _unitOfWork.Performance.Update(performance);
            }
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }
    }
}