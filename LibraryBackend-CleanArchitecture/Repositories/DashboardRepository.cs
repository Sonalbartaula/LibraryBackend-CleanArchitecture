using LibraryBackend_CleanArchitecture.Data;
using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace LibraryBackend_CleanArchitecture.Repositories
{
    public class DashboardRepository
    {
        private readonly LibraryDbContext _context;
        public DashboardRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetTotalBooksAsync()
        {
            return await _context.Books.CountAsync();
        }

        public async Task<int> GetActiveMembersAsync()
        {
            return await _context.Students.CountAsync(s => s.Status == Status.Active);
        }

        public async Task<int> GetIssuedBooksCountAsync()
        {
            return await _context.Transactions.CountAsync(t => t.IssueStatus == Issuestatus.Issued);
        }

        public async Task<int> GetOverdueBooksCountAsync()
        {
            return await _context.Transactions.CountAsync(t => t.DueDate < DateTime.UtcNow && t.IssueStatus == Issuestatus.Issued);
        }

        //public async Task<IEnumerable<Transaction>> GetRecentActivitiesAsync(int count)
        //{
        //    return await _context.Transactions
        //        .OrderByDescending(t => t.CheckoutDate)
        //        .Take(count)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Activity>> GetRecentActivitiesAsync(int count)
        {
            return await _context.Activities
                .OrderByDescending(a => a.Date)
                .Take(count)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetPopularBooksAsync(int count)
        {
            return await _context.Books
                .OrderByDescending(x => x.IssuedCopies)
                .Take(count)
                .ToListAsync();
        }
    }
}
