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
   // [Authorize(StaticControllers.Company)]
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //........................Get all avilable companies

        [HttpGet("GetAllCompanies")]
        public IActionResult GetAll()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.Company.GetAll(orderBy:o=>o.OrderByDescending(o => o.CompanyId));
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //..................................................................Get Companies Detail
        [HttpGet("GetAllCompaniesDetails")]
        public IActionResult GetCompanieDetail()
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.SP_Call.ReturnList<CompaniesVM>(SP.usp_CompaniesGetAllCompanies);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Get existing company by {id}

        [HttpGet("GetCompanyById/{id}")]
        public IActionResult GetCompnayById(int id)
        {
            JsonResult result = new JsonResult(new { });

            var objFromDb = _unitOfWork.Company.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            result.Value = new { Data = objFromDb };
            return result;
        }

        //........................Delete existing company by {id}
        [HttpDelete("DeleteCompany/{id}")]
        public IActionResult Delete(int id)
        {
            JsonResult result = new JsonResult(new { });
            var objFromDb = _unitOfWork.Company.Get(id);
            if (objFromDb == null)
            {
                result.Value = new { Data = false };
                return result;
            }
            _unitOfWork.Company.Remove(objFromDb);
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform Get Insert and Update Actions

        [HttpGet("Upsert/{id?}")]
        public IActionResult Upsert(int? id = 0)
        {
            CompanyDetails company = new CompanyDetails();
            JsonResult result = new JsonResult(new { });
            if (id == 0)
            {
                result.Value = new { Data = true, message = "Its Insert Call" };
                return result;
            }
            company = _unitOfWork.Company.Get(id);
            if (company == null)
            {
                result.Value = new { Data = "Not Found" };
                return result;
            }
            result.Value = new { Data = company, message = "Its Update Call" };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpPost("SaveCompanyData")]
        public IActionResult Upsert(CompanyDetails company)
        {
            JsonResult result = new JsonResult(new { });
            if (company.CompanyId == 0)
            {
                _unitOfWork.Company.Add(company);
            }
            else
            {
                _unitOfWork.Company.Update(company);
            }
            _unitOfWork.Save();
            result.Value = new { Data = true };
            return result;
        }

        //........................Perform POST Insert and Update Actions

        [HttpGet("VerifyCompanyName/{companyName}")]
        public IActionResult VerifyCompanyName(string companyName)
        {
            JsonResult result = new JsonResult(new { });
            if (companyName!=null)
            {
               var dataGet =  _unitOfWork.Company.GetAll(o => o.CompanyName == companyName).Count();
                if(dataGet>0)
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