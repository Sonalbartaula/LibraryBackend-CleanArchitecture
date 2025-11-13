using LibraryBackend_CleanArchitecture.Data;
using LibraryBackend_CleanArchitecture.Dtos;
using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static LibraryBackend_CleanArchitecture.Dtos.ReportsAnalyticsDto;

namespace LibraryBackend_CleanArchitecture.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly LibraryDbContext _context;

        public ReportRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ReportsAnalyticsDto> GetDashboardReportAsync()
        {
            var totalTransactions = await _context.Transactions.CountAsync();
            var totalBooks = await _context.Books.CountAsync();
            var activeMembers = await _context.Students.CountAsync();

            var avgDailyCheckouts = await _context.Transactions
                .GroupBy(t => new {
                    Year = t.CheckoutDate.Year,
                    Month = t.CheckoutDate.Month,
                    Day = t.CheckoutDate.Day
                })
                .Select(g => g.Count())
                .AverageAsync();

            var overdueBooks = await _context.Transactions
                .CountAsync(t => t.DueDate < DateTime.UtcNow && t.ReturnDate == null);

            var popularBooks = await _context.Books
                .OrderByDescending(x => x.IssuedCopies)
                .Take(3)
                .Select(b => new KeyValueDto
                {
                    Key = b.Title,        // or $"{b.Title} by {b.Author}" if you want both
                    Value = b.IssuedCopies
                })
                .ToListAsync();
            var categoryDistribution = await _context.Books
                .GroupBy(b => b.Categories)
                .Select(g => new KeyValueDto
                {
                    Key = g.Key,
                    Value = g.Count()
                })
                .ToListAsync();
            var monthlyTrendsRaw = await _context.Transactions
                .GroupBy(t => new { t.CheckoutDate.Year, t.CheckoutDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .ToListAsync(); // materialize first — EF Core can translate this part

            var monthlyTrends = monthlyTrendsRaw
                .Select(g => new KeyValueDto
                {
                    Key = $"{g.Year}-{g.Month:D2}", // now safely format in memory
                    Value = g.Count
                })
                .OrderBy(x => x.Key)
                .ToList();

            
            var memberGrowthRaw = await _context.Students
                .GroupBy(m => new { m.JoinedDate.Year, m.JoinedDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .ToListAsync(); // same here

            var memberGrowth = memberGrowthRaw
                .Select(g => new KeyValueDto
                {
                    Key = $"{g.Year}-{g.Month:D2}",
                    Value = g.Count
                })
                .OrderBy(x => x.Key)
                .ToList();



            return new ReportsAnalyticsDto
            {
                TotalTransactions = totalTransactions,
                ActiveMembers = activeMembers,
                BooksInCollection = totalBooks,
                AverageDailyCheckouts = Math.Round(avgDailyCheckouts, 1),
                OverdueBooksCount = overdueBooks,
                PopularBooks = popularBooks,
                MonthlyTrends = monthlyTrends,
                CategoryDistribution = categoryDistribution,
                MemberGrowth = memberGrowth
            };
        }
    }
}
