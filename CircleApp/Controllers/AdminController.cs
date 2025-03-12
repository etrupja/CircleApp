using CircleApp.Data.Helpers.Constants;
using CircleApp.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CircleApp.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var reportedPosts = await _adminService.GetReportedPostsAsync();
            return View(reportedPosts);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveReport(int postId)
        {
            await _adminService.ApproveReportAsync(postId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RejectReport(int postId)
        {
            await _adminService.RejectReportAsync(postId);
            return RedirectToAction("Index");
        }
    }
}
