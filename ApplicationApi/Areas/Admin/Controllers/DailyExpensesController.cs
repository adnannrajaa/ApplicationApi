using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApi.CustomAttributes;
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using ApplicationApi.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationApi.Areas.Admin.Controllers
{
    [Authorize(StaticControllers.DailyExpenses)]
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DailyExpensesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DailyExpensesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //........................Get all avilable DailyExpenses

        [HttpGet("GetAllDailyExpenses")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.DailyExpenses.GetAll(orderBy: o => o.OrderByDescending(o => o.ExpensesId)).Take(31);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Get existing DailyExpenses by {id}

        [HttpGet("GetDailyExpensesById/{id}")]
        public IActionResult GetCompnayById(int id)
        {
            JsonResult result = new JsonResult(new { });

            var objFromDb = _unitOfWork.DailyExpenses.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Delete existing DailyExpenses by {id}
        [HttpDelete("DeleteDailyExpenses/{id}")]
        public IActionResult Delete(int id)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.DailyExpenses.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.DailyExpenses.Remove(objFromDb);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
            DailyExpenses Expenses = new DailyExpenses();
            JsonResult result = new JsonResult(new { });
            if (id == 0)
            {
                result.Value = new { Data = true, message = "Its Insert Call" };
                return result;
            }
            Expenses = _unitOfWork.DailyExpenses.Get(id);
            if (Expenses == null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = Expenses, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveDailyExpensesData")]
        public IActionResult Upsert(DailyExpenses Expenses)
        {
            JsonResult result = new JsonResult(new { });
            if (Expenses.ExpensesId == 0)
            {
                Expenses.CreatedDate = DateTime.Now;
                Expenses.CreatedByUserId = 1;
                _unitOfWork.DailyExpenses.Add(Expenses);
            }
            else
            {
                Expenses.CreatedDate = DateTime.Now;
                Expenses.CreatedByUserId = 1;
                _unitOfWork.DailyExpenses.Update(Expenses);
            }
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        
    }
}