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
    [Authorize(StaticControllers.ProductUnit)]
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUnitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductUnitController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //........................Get all avilable Units

        [HttpGet("GetAllUnits")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.ProductUnits.GetAll();
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Get existing unit by {id}

        [HttpGet("GetUnitById/{id}")]
        public IActionResult GetUnitById(int id)
        {
            JsonResult result = new JsonResult(new { });

            var objFromDb = _unitOfWork.ProductUnits.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Delete existing unit by {id}
        [HttpDelete("DeleteUnit/{id}")]
        public IActionResult Delete(int id)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.ProductUnits.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.ProductUnits.Remove(objFromDb);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
            ProductUnits unit = new ProductUnits();
            JsonResult result = new JsonResult(new { });
            if (id == 0)
            {
                result.Value = new { Data = true, message = "Its Insert Call" };
                return result;
            }
            unit = _unitOfWork.ProductUnits.Get(id);
            if (unit == null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = unit, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveUnitData")]
        public IActionResult Upsert(ProductUnits units)
        {
            JsonResult result = new JsonResult(new { });
            if (units.UnitId == 0)
            {
                _unitOfWork.ProductUnits.Add(units);
            }
            else
            {
                _unitOfWork.ProductUnits.Update(units);
            }
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }
        
        //........................Perform POST Insert and Update Actions

        [HttpGet("VerifyUnitName/{unitName}")]
        public IActionResult VerifyUnitName(string unitName)
        {
            JsonResult result = new JsonResult(new { });
            if (unitName != null)
            {
                var recordFound = _unitOfWork.ProductUnits.GetAll(o => o.UnitName == unitName).Count();
                if (recordFound > 0)
                {
                    result.Value = new { Data = true };
                    return result;
                }
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = false };
            return result;
        }
    }
}