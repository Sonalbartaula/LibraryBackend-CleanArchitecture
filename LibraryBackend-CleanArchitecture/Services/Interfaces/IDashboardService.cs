using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;

namespace LibraryBackend_CleanArchitecture.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();
        Task<int> GetTotalBooksAsync();
        Task<int> GetActiveMembersAsync();
        Task<int> GetIssuedBooksCountAsync();
        Task<int> GetOverdueBooksCountAsync();
        Task<IEnumerable<Book>> GetPopularBooksAsync(int count);
        Task<IEnumerable<Activity>> GetRecentActivitiesAsync(int count);

    }


    //public class DashboardSummaryDto
    //{
    //    public int TotalBooks { get; set; }
    //    public int ActiveMembers { get; set; }
    //    public int BooksIssued { get; set; }
    //    public int OverdueBooks { get; set; }

    //    public IEnumerable<Activity> RecentActivities { get; set; } = new List<Activity>();
    //    //public IEnumerable<Transaction> RecentTransactions { get; set; } = new List<Transaction>();

    //    public IEnumerable<Book> PopularBooks { get; set; } = new List<Book>();

    
}
