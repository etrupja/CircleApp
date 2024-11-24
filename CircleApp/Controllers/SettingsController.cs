using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
