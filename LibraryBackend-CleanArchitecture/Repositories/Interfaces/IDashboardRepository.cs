using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Model.Dashboard;

namespace LibraryBackend_CleanArchitecture.Repositories.Interfaces
{
    public interface IDashboardRepository
    {

        Task<int> GetTotalBooksAsync();
        Task<int> GetActiveMembersAsync();
        Task<int> GetIssuedBooksCountAsync();
        Task<int> GetOverdueBooksCountAsync();
        Task<IEnumerable<Activity>> GetRecentActivitiesAsync(int count);

        //Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int count);
        Task<IEnumerable<Book>>GetPopularBooksAsync(int count);

    }
}
