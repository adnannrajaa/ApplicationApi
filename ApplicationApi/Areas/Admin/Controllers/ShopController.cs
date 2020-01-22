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
    [Authorize(StaticControllers.Shop)]
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //........................Get all avilable shops

        [HttpGet("GetAllShops")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.ShopDetail.GetAll(orderBy:o=>o.OrderByDescending(o=>o.ShopId));
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Get existing shop by {id}

        [HttpGet("GetShopsById/{id}")]
        public IActionResult GetShopsById(int id)
        {
            JsonResult result = new JsonResult(new { });

            var objFromDb = _unitOfWork.ShopDetail.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Delete existing shop by {id}
        [HttpDelete("DeleteShop/{id}")]
        public IActionResult Delete(int id)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.ShopDetail.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.ShopDetail.Remove(objFromDb);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
            ShopsDetails shop = new ShopsDetails();
            JsonResult result = new JsonResult(new { });
            if (id == 0)
            {
                result.Value = new { Data = true, message = "Its Insert Call" };
                return result;
            }
            shop = _unitOfWork.ShopDetail.Get(id);
            if (shop == null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = shop, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveShopsData")]
        public IActionResult Upsert(ShopsDetails shop)
        {
            JsonResult result = new JsonResult(new { });
            if (shop.ShopId == 0)
            {
                _unitOfWork.ShopDetail.Add(shop);
            }
            else
            {
                _unitOfWork.ShopDetail.Update(shop);
            }
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................VerifyShopName

        [HttpGet("VerifyShopName/{ShopName}")]
        public IActionResult VerifyShopName(string ShopName)
        {
            JsonResult result = new JsonResult(new { });
            if (ShopName != null)
            {
                var recordFound = _unitOfWork.ShopDetail.GetAll(o => o.ShopName == ShopName).Count();
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

        //........................enableToggel

        [HttpGet("enableDisable/{id}")]
        public IActionResult enableDisable(int id)
        {
            JsonResult result = new JsonResult(new { });
            var status = _unitOfWork.ShopDetail.GetAll().Where(s => s.ShopId == id).FirstOrDefault();
            if(status.IsActive=="true")
            {
                status.IsActive = "false";
                _unitOfWork.Save();
                result.Value = new { Data = true };
                return result;
            }
            else if (status.IsActive == "false")
            {
                status.IsActive = "true";
                _unitOfWork.Save();
                result.Value = new { Data = true };
                return result;
            }
            result.Value = new { Data = false };
            return result;
        }

    }
}