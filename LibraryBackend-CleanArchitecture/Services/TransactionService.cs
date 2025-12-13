using LibraryBackend_CleanArchitecture.Data;
using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Repositories;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using LibraryBackend_CleanArchitecture.Services.Interfaces;

namespace LibraryBackend_CleanArchitecture.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IBookRepository _bookRepository;
        

        public TransactionService(ITransactionRepository transactionRepository, IActivityRepository activityRepository, IBookRepository bookRepository)
        {
            _transactionRepository = transactionRepository;
            _activityRepository = activityRepository;
            _bookRepository = bookRepository;
           
        }

        public async Task<Transaction> CheckoutBookAsync(string memberName, string bookTitle, string isbn)
        {
            
            var transaction = new Transaction
            {
                MemberName = memberName,
                BookName = bookTitle,
                BookTitle = bookTitle,
                Isbn = isbn,
                CheckoutDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14),
                Status = TransactionStatus.Active
            };
            await _transactionRepository.AddAsync(transaction);
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

        public async Task<Transaction?> ReturnBookByTransactionIdAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);

            if (transaction == null || transaction.ReturnDate.HasValue)
                return null;

            transaction.ReturnDate = DateTime.UtcNow;
            transaction.IssueStatus = Issuestatus.Returned;

            if (DateTime.UtcNow > transaction.DueDate)
            {
                var daysOverdue = (DateTime.UtcNow - transaction.DueDate).Days;
                transaction.Fine = daysOverdue * 10; // adjust per-day fine
                transaction.Status = TransactionStatus.Overdue;
            }
            else
            {
                transaction.Status = TransactionStatus.Active;
            }

            await _transactionRepository.SaveChangesAsync();

            // Add activity
            var activity = new Activity
            {
                Type = ActivityType.BookReturned,
                Title = transaction.BookTitle,
                Subtitle = transaction.MemberName,
                Date = DateTime.UtcNow
            };
            await _activityRepository.AddAsync(activity);

            // Increase book availability
            var book = await _bookRepository.GetABookAsync(transaction.BookTitle);
            if (book != null)
            {
                book.TotalCopies++;
                await _bookRepository.UpdateBookAsync(book);
            }

            return transaction;
        }

        public async Task<Transaction?> RenewLoanByTransactionIdAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);

            if (transaction == null || transaction.ReturnDate.HasValue)
                return null;

            // Check if already overdue
            if (DateTime.UtcNow > transaction.DueDate)
                return null; // Don't allow renewal of overdue books

            transaction.DueDate = transaction.DueDate.AddDays(14);
            transaction.Status = TransactionStatus.Active;

            // Add activity
            var activity = new Activity
            {
                Type = ActivityType.BookRenew,
                Title = transaction.BookTitle,
                Subtitle = transaction.MemberName,
                Date = DateTime.UtcNow
            };
            await _activityRepository.AddAsync(activity);

            await _transactionRepository.SaveChangesAsync();

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetActiveLoansAsync(string? searchText, string? status)
        {
            return await _transactionRepository.GetActiveLoansAsync(searchText, status);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(string? searchText, string? type)
        {
            return await _transactionRepository.GetTransactionHistoryAsync(searchText, type);
        }
    }
}
