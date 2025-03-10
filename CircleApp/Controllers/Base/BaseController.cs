using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CircleApp.Controllers.Base
{
    public abstract class BaseController:Controller
    {
        protected int? GetUserId()
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(loggedInUserId))
                return null;
            return int.Parse(loggedInUserId);
        }

        protected IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "Authentication");
        }
    }
}
