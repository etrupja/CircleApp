using CircleApp.Data.Helpers.Constants;
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

        public async Task AddNewNotificationAsync(int userId, string notificationType, string userFullName, int? postId)
        {
            var newNotification = new Notification()
            {
                UserId = userId,
                Message = GetPostMessage(notificationType, userFullName),
                Type = notificationType,
                IsRead = false,
                PostId = postId.HasValue ? postId.Value : null,
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

        public async Task<List<Notification>> GetNotifications(int userId)
        {
            var allNotifications = await _context.Notifications.Where(n => n.UserId == userId)
                .OrderBy(n => n.IsRead)
                .ThenByDescending(n => n.DateCreated)
                .ToListAsync();

            return allNotifications;
        }

        public async Task SetNotificationAsReadAsync(int notificationId)
        {
            var notificationDb = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == notificationId);

            if(notificationDb != null)
            {
                notificationDb.DateUpdated = DateTime.UtcNow;
                notificationDb.IsRead = true;

                _context.Notifications.Update(notificationDb);
                await _context.SaveChangesAsync();
            }
        }

        private string GetPostMessage(string notificationType, string userFullName)
        {
            var message = "";

            switch (notificationType)
            {
                case NotificationType.Like:
                    message = $"{userFullName} liked your post";
                    break;

                case NotificationType.Favorite:
                    message = $"{userFullName} favorited your post";
                    break;

                case NotificationType.Comment:
                    message = $"{userFullName} added a coment to your post";
                    break;

                case NotificationType.FriendRequest:
                    message = $"{userFullName} added you as friend";
                    break;

                case NotificationType.FriendRequestApproved:
                    message = $"{userFullName} approved your friendship request";
                    break;

                default:
                    message = "";
                    break;
            }

            return message;
        }
    }
}
