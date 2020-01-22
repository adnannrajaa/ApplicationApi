using ApplicationApi.DataAccess.Data;
using ApplicationApi.Extenstions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApplicationApi.CustomAttributes
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string[] claim) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { claim };
        }
        public class AuthorizeFilter : IAuthorizationFilter
        {
            readonly string[] _claim;
            private readonly ApplicationDbContext _db;
            public AuthorizeFilter(ApplicationDbContext db,params string[] claim)
            {
                _db = db;
                _claim = claim;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
                var controllerName = _claim.FirstOrDefault();

                if (IsAuthenticated)
                {
                    var userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                    var ActionLinkId = _db.ActionLinks.FirstOrDefault(s => s.ApiControllerName == controllerName && s.IsActiveLink == true);
                    if (ActionLinkId!=null)
                    {
                        var id = ActionLinkId.ActionLinkId;
                        bool flagClaim = false;
                        var IsAllowed = _db.AllowedLinks.Where(s => (s.ActionLinkId == id && s.UserId == userId && s.IsAssinged == true)).Count();
                        if (IsAllowed == 1)
                            flagClaim = true;
                        if (!flagClaim)
                        {
                            if (context.HttpContext.Request.IsAjaxRequest())
                                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized; //Set HTTP 401 
                            else
                            {
                                context.Result = new UnauthorizedResult(); //Set HTTP 401 
                            }
                        }
                    }
                    else
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
                else
                {
                    if (context.HttpContext.Request.IsAjaxRequest())
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden; //Set HTTP 403
                    }
                    else
                    {
                        context.Result = new NotFoundResult();
                    }
                }
                return;
            }
        }
    }
}
