using CircleApp.Controllers.Base;
using CircleApp.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService _userService;
        public UsersController(IUsersService usersService) 
        {
            _userService = usersService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int userId)
        {
            var userPosts = await _userService.GetUserPosts(userId);
            return View(userPosts);
        }
    }
}
