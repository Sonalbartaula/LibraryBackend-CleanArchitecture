using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;

namespace LibraryBackend_CleanArchitecture.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();
    }

   
    public class DashboardSummaryDto
    {
        public int TotalBooks { get; set; }
        public int ActiveMembers { get; set; }
        public int BooksIssued { get; set; }
        public int OverdueBooks { get; set; }

        //public IEnumerable<Transaction> RecentActivities { get; set; } = new List<Transaction>();
        public IEnumerable<Book> PopularBooks { get; set; } = new List<Book>();
    }
}
