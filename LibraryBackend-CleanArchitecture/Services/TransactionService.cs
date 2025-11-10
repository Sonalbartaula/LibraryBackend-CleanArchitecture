using LibraryBackend_CleanArchitecture.Data;
using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using LibraryBackend_CleanArchitecture.Services.Interfaces;

namespace LibraryBackend_CleanArchitecture.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IActivityRepository _activityRepository;
        

        public TransactionService(ITransactionRepository transactionRepository, IActivityRepository activityRepository)
        {
            _transactionRepository = transactionRepository;
            _activityRepository = activityRepository;
           
        }

        public async Task<transaction> CheckoutBookAsync(string memberName, string bookTitle, string isbn)
        {
            var transaction = new transaction
            {
                MemberName = memberName,
                BookName = bookTitle,
                BookTitle = bookTitle,
                Isbn = isbn,
                CheckoutDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14),
                Status = TransactionStatus.Active
            };

            var activity = new Activity
            {
                Type = ActivityType.BookIssued,             // Enum type for the activity
                Title = transaction.BookTitle,             // Member who performed the action
                Subtitle = transaction.MemberName,           // Book that was issued
                Date = DateTime.UtcNow                       // Timestamp
            };

            await _activityRepository.AddAsync(activity);
            
            await _transactionRepository.SaveChangesAsync();
            return transaction;
        }

        public async Task<transaction?> ReturnBookAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null || transaction.ReturnDate.HasValue)
                return null;

            transaction.ReturnDate = DateTime.UtcNow;

            if (DateTime.UtcNow > transaction.DueDate)
            {
                var daysOverdue = (DateTime.UtcNow - transaction.DueDate).Days;
                transaction.Fine = daysOverdue * 1; // adjust per-day fine
                transaction.Status = TransactionStatus.Overdue;
            }
            else
            {
                transaction.Status = TransactionStatus.Active;
            }

            await _transactionRepository.SaveChangesAsync();
            var activity = new Activity
            {
                Type = ActivityType.BookReturned,             // Enum type for the activity
                Title = transaction.BookTitle,             // Member who performed the action
                Subtitle = transaction.MemberName,           // Book that was issued
                Date = DateTime.UtcNow
            };
            await _activityRepository.AddAsync(activity);
            return transaction;
        }

        public async Task<transaction?> RenewLoanAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null || transaction.ReturnDate.HasValue)
                return null;

            transaction.DueDate = transaction.DueDate.AddDays(14);
            transaction.Status = TransactionStatus.Active;
            var activity = new Activity
            {
                Type = ActivityType.BookRenew,             // Enum type for the activity
                Title = transaction.BookTitle,             // Member who performed the action
                Subtitle = transaction.MemberName,           // Book that was issued
                Date = DateTime.UtcNow
            };
            await _activityRepository.AddAsync(activity);
            await _transactionRepository.SaveChangesAsync();
            return transaction;
        }

        public async Task<IEnumerable<transaction>> GetActiveLoansAsync(string? searchText, string? status)
        {
            return await _transactionRepository.GetActiveLoansAsync(searchText, status);
        }

        public async Task<IEnumerable<transaction>> GetTransactionHistoryAsync(string? searchText, string? type)
        {
            return await _transactionRepository.GetTransactionHistoryAsync(searchText, type);
        }
    }
}
