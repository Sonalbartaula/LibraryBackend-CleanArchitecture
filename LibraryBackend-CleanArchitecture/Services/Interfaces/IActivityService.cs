using LibraryBackend_CleanArchitecture.Model.Dashboard;

namespace LibraryBackend_CleanArchitecture.Services.Interfaces
{
    public interface IActivityService
    {
        Task LogActivityAsync(ActivityType type, string title, string subtitle);
        Task<IEnumerable<Activity>> GetRecentActivitiesAsync(int count);
    }
}
