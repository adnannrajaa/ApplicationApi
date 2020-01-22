using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApi.CustomAttributes;
using ApplicationApi.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationApi.Areas.Admin.Controllers
{
    [Authorize(StaticControllers.Purchases)]
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
    }
}