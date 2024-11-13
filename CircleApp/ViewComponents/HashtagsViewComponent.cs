using Microsoft.AspNetCore.Mvc;

namespace CircleApp.ViewComponents
{
    public class HashtagsViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
