using LibraryBackend_CleanArchitecture.Data;
using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend_CleanArchitecture.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly LibraryDbContext _context;

        public ActivityRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Activity activity)
        {
            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Activity>> GetRecentActivitiesAsync(int count)
        {
            return await _context.Activities
                                 .OrderByDescending(a => a.Date)
                                 .Take(count)
                                 .ToListAsync();
        }
    }
}
