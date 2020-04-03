using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AwesomeCare.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace AwesomeCare.Web.Controllers
{
    public class BaseController : Controller
    {
        public const string cacheKey = "baserecord_key";

        public BaseController()
        {

        }

        public void SetOperationStatus(OperationStatus operationStatus)
        {
            TempData["OperationStatus"] = JsonConvert.SerializeObject(operationStatus);
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                //Check if user is registered as Staff
                var hasStaffProfile = context.HttpContext.User.Claims.GetClaimValue("hasStaffInfo");
                if (string.Equals("false", hasStaffProfile, StringComparison.InvariantCultureIgnoreCase))
                {
                    var controller = context.ActionDescriptor.RouteValues["controller"];
                    var action = context.ActionDescriptor.RouteValues["action"];
                    if (!string.Equals(controller, "Staff", StringComparison.InvariantCultureIgnoreCase) && !string.Equals(action, "Registration", StringComparison.InvariantCultureIgnoreCase))
                        context.Result = new RedirectToActionResult("Registration", "Staff", null);

                }

            }
            base.OnActionExecuting(context);
        }
    }
}