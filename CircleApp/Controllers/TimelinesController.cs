using CircleApp.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    public class TimelinesController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IPostsService _postsService;

        public TimelinesController(IUsersService usersService, IPostsService postsService)
        {
            _usersService = usersService;
            _postsService = postsService;
        }

        public async Task<IActionResult> Index(int loggedInUserId)
        {
            var userTimeLineInfo = await _postsService.GetAllTimelinePostsAsync(loggedInUserId);

            return View(userTimeLineInfo);
        }
    }
}
