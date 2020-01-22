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
    [Authorize(StaticControllers.MonthlyExpenses)]
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyExpensesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MonthlyExpensesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //........................Get all avilable MonthlyExpenses

        [HttpGet("GetAllMonthlyExpenses")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.MonthlyExpenses.GetAll(orderBy: o => o.OrderByDescending(o => o.ExpensesId)).Where(s=>s.CreatedDate.Year == DateTime.Now.Year);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Get existing MonthlyExpenses by {id}

        [HttpGet("GetMonthlyExpensesById/{id}")]
        public IActionResult GetMonthlyExpensesById(int id)
        {
            JsonResult result = new JsonResult(new { });

            var objFromDb = _unitOfWork.MonthlyExpenses.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Delete existing MonthlyExpenses by {id}
        [HttpDelete("DeleteMonthlyExpenses/{id}")]
        public IActionResult Delete(int id)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.MonthlyExpenses.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.MonthlyExpenses.Remove(objFromDb);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
             MonthlyExpenses Expenses = new MonthlyExpenses();
            JsonResult result = new JsonResult(new { });
            if (id == 0)
            {
                result.Value = new { Data = true, message = "Its Insert Call" };
                return result;
            }
            Expenses = _unitOfWork.MonthlyExpenses.Get(id);
            if (Expenses == null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = Expenses, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveMonthlyExpensesData")]
        public IActionResult Upsert(MonthlyExpenses Expenses)
        {
            JsonResult result = new JsonResult(new { });
            if (Expenses.ExpensesId == 0)
            {
                Expenses.CreatedById =2;
                Expenses.CreatedDate = DateTime.Now;
               _unitOfWork.MonthlyExpenses.Add(Expenses);
            }
            else
            {
                Expenses.CreatedById = 2;
                Expenses.CreatedDate = DateTime.Now;
                 _unitOfWork.MonthlyExpenses.Update(Expenses);
            }
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }
    }
}