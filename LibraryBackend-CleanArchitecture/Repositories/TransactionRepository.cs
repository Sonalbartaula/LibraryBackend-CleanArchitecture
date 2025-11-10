using LibraryBackend_CleanArchitecture.Data;
using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend_CleanArchitecture.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly LibraryDbContext _context;

        public TransactionRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<IEnumerable<transaction>> GetActiveLoansAsync(string? searchText, string? status)
        {
            var query = _context.Transactions.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(t =>
                    t.BookName.Contains(searchText) ||
                    t.MemberName.Contains(searchText) ||
                    t.BookTitle.Contains(searchText));
            }

            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse<TransactionStatus>(status, true, out var parsedStatus))
                    query = query.Where(t => t.Status == parsedStatus && !t.ReturnDate.HasValue);
            }
            else
            {
                query = query.Where(t => !t.ReturnDate.HasValue);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<transaction>> GetTransactionHistoryAsync(string? searchText, string? type)
        {
            var query = _context.Transactions.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(t =>
                    t.BookName.Contains(searchText) ||
                    t.MemberName.Contains(searchText) ||
                    t.BookTitle.Contains(searchText));
            }

            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("Return", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(t => t.ReturnDate.HasValue);
                else if (type.Equals("Checkout", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(t => !t.ReturnDate.HasValue);
            }

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
