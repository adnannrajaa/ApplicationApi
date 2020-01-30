using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApi.CustomAttributes;
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Extenstions;
using ApplicationApi.Models.DataModels;
using ApplicationApi.Models.ViewModels;
using ApplicationApi.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationApi.Areas.User.Controllers
{
    //[Authorize(StaticControllers.Cart)]
    [Area("User")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CartViewModel CartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartVM = new CartViewModel
            {
                CustomerDetail = new Models.DataModels.CustomerDetails(),
                ProductsDetails = new List<Products>()
            };
        }
        [HttpGet("GetAllFromCart")]
        public IActionResult GetItemFromCart()
        {
            JsonResult result = new JsonResult(new { });
            if (HttpContext.Session.GetObject<List<int>>(StaticData.sessionCart)!=null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(StaticData.sessionCart);
                foreach(int productId in sessionList)
                {
                    CartVM.ProductsDetails.Add(_unitOfWork.Products.GetFirstOrDefault(u => u.ProductId == productId));
                }
            }
            result.Value = new { Data = CartVM };
            return result;
        }
        [HttpDelete("Remove")]
        public IActionResult RemoveItemFromCart(int ProductId)
        {
            JsonResult result = new JsonResult(new { });

            List<int> sessionList = new List<int>();
            sessionList = HttpContext.Session.GetObject<List<int>>(StaticData.sessionCart);
            sessionList.Remove(ProductId);
            HttpContext.Session.SetObject(StaticData.sessionCart, sessionList);
            result.Value = new { Data = sessionList };
            return result;
        }
    }
}