using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using LibraryBackend_CleanArchitecture.Services.Interfaces;

namespace LibraryBackend_CleanArchitecture.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        // Main aggregated call — fastest & recommended
        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            return new DashboardSummaryDto
            {
                TotalBooks = await _dashboardRepository.GetTotalBooksAsync(),
                ActiveMembers = await _dashboardRepository.GetActiveMembersAsync(),
                BooksIssued = await _dashboardRepository.GetIssuedBooksCountAsync(),
                OverdueBooks = await _dashboardRepository.GetOverdueBooksCountAsync(),
                BooksAddedThisMonth = await _dashboardRepository.GetBooksAddedThisMonthAsync(),
                MembersJoinedThisMonth = await _dashboardRepository.GetMembersJoinedThisMonthAsync(),
                DueSoonCount = await _dashboardRepository.GetDueSoonCountAsync(),
                RemindersSent = await _dashboardRepository.GetRemindersSentCountAsync(),
                RecentActivities = await _dashboardRepository.GetRecentActivitiesAsync(5),
                PopularBooks = await _dashboardRepository.GetPopularBooksAsync(5)
            };
        }

        // Individual methods (kept for flexibility)
        public async Task<int> GetTotalBooksAsync()
            => await _dashboardRepository.GetTotalBooksAsync();

        public async Task<int> GetActiveMembersAsync()
            => await _dashboardRepository.GetActiveMembersAsync();

        public async Task<int> GetIssuedBooksCountAsync()
            => await _dashboardRepository.GetIssuedBooksCountAsync();

        public async Task<int> GetOverdueBooksCountAsync()
            => await _dashboardRepository.GetOverdueBooksCountAsync();

        public async Task<int> GetBooksAddedThisMonthAsync()
            => await _dashboardRepository.GetBooksAddedThisMonthAsync();

        public async Task<int> GetMembersJoinedThisMonthAsync()
            => await _dashboardRepository.GetMembersJoinedThisMonthAsync();

        public async Task<int> GetDueSoonCountAsync()
            => await _dashboardRepository.GetDueSoonCountAsync();

        public async Task<int> GetRemindersSentCountAsync()
            => await _dashboardRepository.GetRemindersSentCountAsync();

        public async Task<IEnumerable<Book>> GetPopularBooksAsync(int count = 5)
            => await _dashboardRepository.GetPopularBooksAsync(count);

        public async Task<IEnumerable<Activity>> GetRecentActivitiesAsync(int count = 5)
            => await _dashboardRepository.GetRecentActivitiesAsync(count);
    }
}