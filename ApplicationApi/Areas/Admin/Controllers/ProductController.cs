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
    [Authorize(StaticControllers.Products)]
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //........................Get all avilable Products

        [HttpGet("GetAllProducts")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.SP_Call.ReturnList<ProductsVM>(SP.usp_PropductGetAllProducts);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Get existing Product by {id}

        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            JsonResult result = new JsonResult(new { });

            var objFromDb = _unitOfWork.Products.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Delete existing product by {id}
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult Delete(int id)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.Products.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.Products.Remove(objFromDb);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
            Products product = new Products();
            JsonResult result = new JsonResult(new { });
            if (id == 0)
            {
                result.Value = new { Data = true, message = "Its Insert Call" };
                return result;
            }
            product = _unitOfWork.Products.Get(id);
            if (product == null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = product, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveProductData")]
        public IActionResult Upsert(Products product)
        {
            JsonResult result = new JsonResult(new { });
            if (product.ProductId == 0)
            {
                _unitOfWork.Products.Add(product);
            }
            else
            {
                _unitOfWork.Products.Update(product);
            }
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................enableToggel

        [HttpPut("enableDisable/{id}")]
        public IActionResult enableDisable(int id)
        {
            JsonResult result = new JsonResult(new { });
            var status = _unitOfWork.Products.GetAll().Where(s => s.ProductId == id).FirstOrDefault();
            if (status.Status == true)
            {
                status.Status = false;
                _unitOfWork.Save();
                result.Value = new { Data = true };
                return result;
            }
            else if (status.Status == false)
            {
                status.Status = true;
                _unitOfWork.Save();
                result.Value = new { Data = true };
                return result;
            }
            result.Value = new { Data = false };
            return result;
        }

    }
}