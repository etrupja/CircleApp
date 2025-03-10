using CircleApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CircleApp.Data.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly AppDbContext _context;
        public NotificationsService(AppDbContext context)
        {
            _context = context;
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
