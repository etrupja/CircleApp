using CircleApp.Data.Models;

namespace CircleApp.Data.Services
{
    public interface INotificationsService
    {
        Task AddNewNotificationAsync(int userId, string notificationType, string userFullName, int? postId);
        Task<int> GetUnreadNotificationsCountAsync(int userId);
        Task<List<Notification>> GetNotifications(int userId);

        Task SetNotificationAsReadAsync(int notificationId);
    }
}
