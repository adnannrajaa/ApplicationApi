using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApi.CustomAttributes;
using ApplicationApi.DataAccess.Data.Repository.IRepository;
using ApplicationApi.Models.DataModels;
using ApplicationApi.Models.ViewModels;
using ApplicationApi.Utility;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationApi.Areas.Admin.Controllers
{
    [Authorize(StaticControllers.AllowedLinks)]
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AllowedLinksController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public AllowedLinksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //........................Get all Allowed Links

        [HttpGet("GetAllAllowedLinks")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.AllowedLinks.GetAll();
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Get all Allowed Links

        [HttpGet("GetAssignedPages/{UserId}/{Status}")]
        public IActionResult AssignedPages(string UserId, int Status)
        { 
            JsonResult result = new JsonResult(new { });
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@UserId", UserId);
            if(Status==1)
            {
                parameter.Add("@Condition", StaticData.Assign);
            }
            else if (Status == 2)
            {
                parameter.Add("@Condition", StaticData.NotAssign);
            }
            else if (Status == 3)
            {
                parameter.Add("@Condition", StaticData.AllPages);
            }
            var objFromDb = _unitOfWork.SP_Call.ReturnList<ActionLinks>(SP.usp_GetAssignedOrUnAssignedPages, parameter);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;

        }

        //........................Get existing AllowedLink by {id}

        [HttpGet("GetAllowedLinksById/{id}")]
        public IActionResult GetAllowdLinksById(int id)
        {
            JsonResult result = new JsonResult(new { });

            var objFromDb = _unitOfWork.AllowedLinks.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Delete existing AllowedLinks by {id}
        [HttpDelete("DeleteAllowedLink/{id}")]
        public IActionResult Delete(int id)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.AllowedLinks.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.AllowedLinks.Remove(objFromDb);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
            AllowedLinks link = new AllowedLinks();
            JsonResult result = new JsonResult(new { });
            if (id == 0)
            {
                result.Value = new { Data = true, message = "Its Insert Call" };
                return result;
            }
            link = _unitOfWork.AllowedLinks.Get(id);
            if (link == null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = link, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveAllowedLinksData")]
        public IActionResult Upsert(AllowedLinks link)
        {
            JsonResult result = new JsonResult(new { });
            if (link.AllowedLinkId == 0)
            {
                _unitOfWork.AllowedLinks.Add(link);
            }
            else
            {
                _unitOfWork.AllowedLinks.Update(link);
            }
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................enableToggel

        [HttpGet("enableDisable/{id}/{UserId}")]
        public IActionResult enableDisable(int id,string UserId)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.AllowedLinks.GetAll().Where(s => (s.ActionLinkId == id && s.UserId== UserId)).FirstOrDefault();
             if(objFromDb ==null)
            {
                AllowedLinks obj = new AllowedLinks();
                obj.UserId = UserId;
                obj.ActionLinkId = id;
                obj.IsAssinged = true;
                _unitOfWork.AllowedLinks.Add(obj);
                _unitOfWork.Save();
            }
            else
            {
                if (objFromDb.IsAssinged == true)
                {
                    objFromDb.IsAssinged = false;
                    _unitOfWork.Save();
                    result.Value = new { Data = true };
                    return result;
                }
                else if (objFromDb.IsAssinged == false)
                {
                    objFromDb.IsAssinged = true;
                    _unitOfWork.Save();
                    result.Value = new { Data = true };
                    return result;
                }
            }
           
            result.Value = new { Data = false };
            return result;
        }
    }
}