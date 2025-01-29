using Microsoft.AspNetCore.Mvc;

namespace CircleApp.ViewComponents
{
    public class SuggestedFriendsViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
