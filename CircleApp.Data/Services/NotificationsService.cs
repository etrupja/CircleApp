using CircleApp.Data.Hubs;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CircleApp.Data.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationsService(AppDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task AddNewNotificationAsync(int userId, string message, string notificationType)
        {
            var newNotification = new Notification()
            {
                UserId = userId,
                Message = message,
                Type = notificationType,
                IsRead = false,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            await _context.Notifications.AddAsync(newNotification);
            await _context.SaveChangesAsync();

            var notificationNumber = await GetUnreadNotificationsCountAsync(userId);

            await _hubContext.Clients.User(userId.ToString())
                .SendAsync("ReceiveNotification", notificationNumber);

        }

        public async Task<int> GetUnreadNotificationsCountAsync(int userId)
        {
            var count = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .CountAsync();

            return count;
        }
    }
}
