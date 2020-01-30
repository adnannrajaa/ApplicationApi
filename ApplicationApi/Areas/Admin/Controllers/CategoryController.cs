using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationApi.Models.DataModels;
using ApplicationApi.Utility;
using ApplicationApi.CustomAttributes;

namespace ApplicationApi.Areas.Admin.Controllers
{
    //[Authorize(StaticControllers.Category)]
    [Area("Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //........................Get all avilable Categories
        [HttpGet("GetAllCategories")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.Category.GetAll(orderBy: o => o.OrderByDescending(o => o.CategoryId));
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Get existing category by {id}
        [HttpGet("GetCategoryById/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            JsonResult result = new JsonResult(new { });

            var objFromDb = _unitOfWork.Category.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Delete existing category by {id}
        [HttpDelete("DeleteCategory/{id}")]
        public IActionResult Delete(int id)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.Category.Get(id);
            if(objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
            ProductCategory category = new ProductCategory();
            JsonResult result = new JsonResult(new { });
            if (id==0)
            {
                result.Value = new { Data = true, message="Its Insert Call" };
                return result;
            }
            category = _unitOfWork.Category.Get(id);
            if(category==null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = category, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveCategoryData")]
        public IActionResult Upsert(ProductCategory category)
        {
            JsonResult result = new JsonResult(new { });
            if (category.CategoryId == 0)
            {
                _unitOfWork.Category.Add(category);
            }
            else
            {
                _unitOfWork.Category.Update(category);
            }
            _unitOfWork.Save();
            result.Value = new { Data =true};
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpGet("VerifyCategoryName/{CategoryName}")]
        public IActionResult verifyCategoryName(string CategoryName)
        {
            JsonResult result = new JsonResult(new { });
            if (CategoryName != null)
            {
               var recordFound =  _unitOfWork.Category.GetAll(o=>o.CategoryName==CategoryName).Count();
                if(recordFound>0)
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