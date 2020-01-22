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
    [Authorize(StaticControllers.ActionLinks)]
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ActionLinksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActionLinksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //........................Get all Action Links

        [HttpGet("GetAllActionLinks")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.ActionLinks.GetAll(orderBy:o=>o.OrderByDescending(o=>o.ActionLinkId));
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Get existing ActionLink by {id}

        [HttpGet("GetActionLinksById/{id}")]
        public IActionResult GetAllowdLinksById(int id)
        {
            JsonResult result = new JsonResult(new { });

            var objFromDb = _unitOfWork.ActionLinks.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Delete existing ActionLinks by {id}
        [HttpDelete("DeleteActionLink/{id}")]
        public IActionResult Delete(int id)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.ActionLinks.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.ActionLinks.Remove(objFromDb);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
            ActionLinks link = new ActionLinks();
            JsonResult result = new JsonResult(new { });
            if (id == 0)
            {
                result.Value = new { Data = true, message = "Its Insert Call" };
                return result;
            }
            link = _unitOfWork.ActionLinks.Get(id);
            if (link == null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = link, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveActionLinksData")]
        public IActionResult Upsert(ActionLinks link)
        {
            JsonResult result = new JsonResult(new { });
            if (link.ActionLinkId == 0)
            {
                _unitOfWork.ActionLinks.Add(link);
            }
            else
            {
                _unitOfWork.ActionLinks.Update(link);
            }
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................enableToggel

        [HttpGet("enableDisable/{id}")]
        public IActionResult enableDisable(int id)
        {
            JsonResult result = new JsonResult(new { });
            var status = _unitOfWork.ActionLinks.GetAll().Where(s => s.ActionLinkId == id).FirstOrDefault();
            if (status.IsActiveLink == true)
            {
                status.IsActiveLink = false;
                _unitOfWork.Save();
                result.Value = new { Data = true };
                return result;
            }
            else if (status.IsActiveLink == false)
            {
                status.IsActiveLink = true;
                _unitOfWork.Save();
                result.Value = new { Data = true };
                return result;
            }
            result.Value = new { Data = false };
            return result;
        }

    }
}