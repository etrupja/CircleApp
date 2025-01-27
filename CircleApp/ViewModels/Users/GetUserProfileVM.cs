using CircleApp.Data.Models;

namespace CircleApp.ViewModels.Users
{
    public class GetUserProfileVM
    {
        public User User { get; set; }
        public List<Post> Posts { get; set; }
    }
}
