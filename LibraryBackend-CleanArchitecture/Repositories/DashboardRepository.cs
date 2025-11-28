using LibraryBackend_CleanArchitecture.Data;
using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace LibraryBackend_CleanArchitecture.Repositories
{
    public class DashboardRepository : IDashboardRepository
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
        // NEW: Books added this month
        public async Task<int> GetBooksAddedThisMonthAsync()
        {
            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            return await _context.Books
                .CountAsync(b => b.AddedDate >= startOfMonth);
        }

        // NEW: Members joined this month
        public async Task<int> GetMembersJoinedThisMonthAsync()
        {
            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            return await _context.Students
                .CountAsync(s => s.JoinedDate >= startOfMonth && s.Status == Status.Active);
        }

        // NEW: Books due soon (next 7 days)
        public async Task<int> GetDueSoonCountAsync()
        {
            var now = DateTime.UtcNow;
            var nextWeek = now.AddDays(7);

            return await _context.Transactions
                .CountAsync(t => t.IssueStatus == Issuestatus.Issued
                              && t.DueDate >= now
                              && t.DueDate <= nextWeek);
        }

        
        public async Task<int> GetRemindersSentCountAsync()
        {
            
            return await GetOverdueBooksCountAsync(); // temporary fallback
        }
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
