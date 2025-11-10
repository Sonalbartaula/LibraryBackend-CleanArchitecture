using LibraryBackend_CleanArchitecture.Model.Dashboard;

namespace LibraryBackend_CleanArchitecture.Repositories.Interfaces
{
    public interface IActivityRepository
    {
        Task AddAsync(Activity activity);
        Task<IEnumerable<Activity>> GetRecentActivitiesAsync(int count);
    }

}
