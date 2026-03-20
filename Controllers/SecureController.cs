using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ElderCareApp.Controllers
{
    public class SecureController : Controller
    {

       protected string? UserRole => HttpContext.Session.GetString("Role");
        protected int? UserId => HttpContext.Session.GetInt32("UserId");

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // If session is empty, kick them to login
            if (string.IsNullOrEmpty(UserRole))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
            base.OnActionExecuting(context);
        }
    }
}