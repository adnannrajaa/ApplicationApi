using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationApi.Areas.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //........................Get Product for invoice by id

        [HttpGet("GetInvoiceProductById/{id}")]
        public JsonResult GetInvoiceProductById(int id)
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

        //........................Get Product for invoice by Name

        [HttpGet("GetInvoiceProductByName/{Name}")]
        public JsonResult GetInvoiceProductByName(string Name)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.Products.GetFirstOrDefault(s=>s.ProductName== Name);

            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }
    }
}