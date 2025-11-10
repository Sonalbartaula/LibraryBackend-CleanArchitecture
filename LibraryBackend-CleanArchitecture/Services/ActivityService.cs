using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using LibraryBackend_CleanArchitecture.Services.Interfaces;

namespace LibraryBackend_CleanArchitecture.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _repository;

        public ActivityService(IActivityRepository repository)
        {
            _repository = repository;
        }

        public async Task LogActivityAsync(ActivityType type, string title, string subtitle)
        {
            var activity = new Activity
            {
                Type = type,
                Title = title,
                Subtitle = subtitle,
                Date = DateTime.UtcNow
            };

            await _repository.AddAsync(activity);
        }

        public async Task<IEnumerable<Activity>> GetRecentActivitiesAsync(int count)
        {
            return await _repository.GetRecentActivitiesAsync(count);
        }
    } 
}
