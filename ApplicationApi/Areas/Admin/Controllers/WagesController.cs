using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApi.CustomAttributes;
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using ApplicationApi.Models.ViewModels;
using ApplicationApi.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationApi.Areas.Admin.Controllers
{
    [Authorize(StaticControllers.Wages)]
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class WagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //........................Get all avilable Wages

        [HttpGet("GetAllWages")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.SP_Call.ReturnList<WagesVM>(SP.usp_WagesGetAllEmployeesWages);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Get existing Wages by {id}

        [HttpGet("GetWagesById/{id}")]
        public IActionResult GetWagesById(int id)
        {
            JsonResult result = new JsonResult(new { });

            var objFromDb = _unitOfWork.wages.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Delete existing Wages by {id}
        [HttpDelete("DeleteWages/{id}")]
        public IActionResult Delete(int id)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.wages.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.wages.Remove(objFromDb);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
            Wages wages = new Wages();
            JsonResult result = new JsonResult(new { });
            if (id == 0)
            {
                result.Value = new { Data = true, message = "Its Insert Call" };
                return result;
            }
            wages = _unitOfWork.wages.Get(id);
            if (wages == null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = wages, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveWagesData")]
        public IActionResult Upsert(Wages wages)
        {
            JsonResult result = new JsonResult(new { });
            if (wages.WagesId == 0)
            {
                wages.CreatedBy = "Admin";
                wages.CreatedDate = Convert.ToString(DateTime.Now);
                wages.WagesYear = Convert.ToString(DateTime.Now.Year);
                wages.OutStanding = Convert.ToString(Convert.ToInt32(wages.CurrentSalary) - Convert.ToInt32(wages.SalaryPaid));
                _unitOfWork.wages.Add(wages);
            }
            else
            {
                wages.CreatedBy = "Admin";
                wages.CreatedDate = Convert.ToString(DateTime.Now);
                wages.WagesYear = Convert.ToString(DateTime.Now.Year);
                wages.OutStanding = Convert.ToString(Convert.ToInt32(wages.CurrentSalary) - Convert.ToInt32(wages.SalaryPaid));
                _unitOfWork.wages.Update(wages);
            }
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }
    }
}