using CircleApp.Data.Models;

namespace CircleApp.ViewModels.Friends
{
    public class FriendshipVM
    {
        public List<FriendRequest> FriendRequestsSent = new List<FriendRequest>();
        public List<FriendRequest> FriendRequestsReceived = new List<FriendRequest>();
    }
}
