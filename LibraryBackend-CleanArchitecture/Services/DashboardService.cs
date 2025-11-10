using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
//using LibraryBackend_CleanArchitecture.Services.Interfaces;
using LibraryBackend_CleanArchitecture.Model.Dashboard;


namespace LibraryBackend_CleanArchitecture.Services
{
    public class DashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            var totalBooks = await _dashboardRepository.GetTotalBooksAsync();
            var activeMembers = await _dashboardRepository.GetActiveMembersAsync();
            var issuedBooks = await _dashboardRepository.GetIssuedBooksCountAsync();
            var overdueBooks = await _dashboardRepository.GetOverdueBooksCountAsync();

            var recentActivities = await _dashboardRepository.GetRecentActivitiesAsync(5);

            var popularBooks = await _dashboardRepository.GetPopularBooksAsync(5);

            return new DashboardSummaryDto
            {
                TotalBooks = totalBooks,
                ActiveMembers = activeMembers,
                BooksIssued = issuedBooks,
                OverdueBooks = overdueBooks,
                RecentActivities = recentActivities,
                PopularBooks = popularBooks
            };
        }
    } 
}

