using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    public class FavoritesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
