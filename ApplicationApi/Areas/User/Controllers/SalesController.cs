using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApi.CustomAttributes;
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Extenstions;
using ApplicationApi.Models.DataModels;
using ApplicationApi.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationApi.Areas.User.Controllers
{
    [Authorize(StaticControllers.Sales)]
    [Area("User")]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //........................Get all avilable Sales

        [HttpGet("GetAllSales")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.Sale.GetAll();
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Get existing sale by {id}

        [HttpGet("GetSaleById/{id}")]
        public IActionResult GetSaleById(int id)
        {
            JsonResult result = new JsonResult(new { });

            var objFromDb = _unitOfWork.Sale.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Delete existing sale by {id}
        [HttpDelete("DeleteSale/{id}")]
        public IActionResult Delete(int id)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.Sale.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.Sale.Remove(objFromDb);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
            Sales sale = new Sales();
            JsonResult result = new JsonResult(new { });
            if (id == 0)
            {
                result.Value = new { Data = true, message = "Its Insert Call" };
                return result;
            }
            sale = _unitOfWork.Sale.Get(id);
            if (sale == null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = sale, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveSalesData")]
        public IActionResult Upsert(Sales sale)
        {
            JsonResult result = new JsonResult(new { });
            if (sale.SaleId == 0)
            {
                _unitOfWork.Sale.Add(sale);
            }
            else
            {
                _unitOfWork.Sale.Update(sale);
            }
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }
        //.......................................Add to Cart
        [HttpPost("addToCart/{ProductId?}")]
        public IActionResult AddToCart(int ProductId)
        {
            JsonResult result = new JsonResult(new { });
            List<int> sessionList = new List<int>();
            if(string.IsNullOrEmpty(HttpContext.Session.GetString(StaticData.sessionCart)))
            {
                sessionList.Add(ProductId);
                HttpContext.Session.SetObject(StaticData.sessionCart, sessionList);
            }
            else
            {
                sessionList = HttpContext.Session.GetObject<List<int>>(StaticData.sessionCart);
                if(!sessionList.Contains(ProductId))
                {
                    sessionList.Add(ProductId);
                    HttpContext.Session.SetObject(StaticData.sessionCart, sessionList);

                }
            }
            result.Value = new { Data = sessionList };
            return result;
        }
    }
}