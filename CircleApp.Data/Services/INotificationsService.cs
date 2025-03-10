namespace CircleApp.Data.Services
{
    public interface INotificationsService
    {
        Task AddNewNotificationAsync(int userId, string message, string notificationType);
        Task<int> GetUnreadNotificationsCountAsync(int userId);
    }
}
