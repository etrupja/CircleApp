using CircleApp.Controllers.Base;
using CircleApp.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    public class NotificationsController : BaseController
    {
        private readonly INotificationsService _notificationsService;
        public NotificationsController(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCount()
        {
            var userId = GetUserId();
            if (!userId.HasValue) RedirectToLogin();

            var count = await _notificationsService.GetUnreadNotificationsCountAsync(userId.Value);
            return Json(count);
        }

        public async Task<IActionResult> GetNotifications()
        {
            var userId = GetUserId();
            if (!userId.HasValue) RedirectToLogin();

            var notifications = await _notificationsService.GetNotifications(userId.Value);
            return PartialView("Notifications/_Notifications", notifications);
        }
    }
}
