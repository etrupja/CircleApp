using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace CircleApp.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        public string _userId => Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = _userId;
            await Groups.AddToGroupAsync(userId, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = _userId;
            await Groups.RemoveFromGroupAsync(userId, Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
