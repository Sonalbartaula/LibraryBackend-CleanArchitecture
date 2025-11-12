using LibraryBackend_CleanArchitecture.Model;
//using LibraryBackend_CleanArchitecture.Services.Interfaces;
using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using LibraryBackend_CleanArchitecture.Services.Interfaces;
using DashboardSummaryDto = LibraryBackend_CleanArchitecture.Model.Dashboard.DashboardSummaryDto;


namespace LibraryBackend_CleanArchitecture.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<int> GetTotalBooksAsync()
        {
            return await _dashboardRepository.GetTotalBooksAsync();
        }

        
        public async Task<int> GetActiveMembersAsync()
        {
            return await _dashboardRepository.GetActiveMembersAsync();
        }

       
        public async Task<int> GetIssuedBooksCountAsync()
        {
            return await _dashboardRepository.GetIssuedBooksCountAsync();
        }

       
        public async Task<int> GetOverdueBooksCountAsync()
        {
            return await _dashboardRepository.GetOverdueBooksCountAsync();
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
        public async Task<IEnumerable<Book>> GetPopularBooksAsync(int count)
        {
            return await _dashboardRepository.GetPopularBooksAsync(count);
        }

        public async Task<IEnumerable<Activity>> GetRecentActivitiesAsync(int count)
        {
            return await _dashboardRepository.GetRecentActivitiesAsync(count);
        }
    } 
}

