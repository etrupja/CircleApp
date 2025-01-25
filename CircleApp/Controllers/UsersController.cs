using CircleApp.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    public class UsersController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(string userId)
        {
            return View();
        }
    }
}
