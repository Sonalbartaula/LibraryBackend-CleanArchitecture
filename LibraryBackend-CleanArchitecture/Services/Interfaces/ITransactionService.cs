using LibraryBackend_CleanArchitecture.Model;

namespace LibraryBackend_CleanArchitecture.Services.Interfaces
{
    public interface ITransactionService
        
    {
        Task<Transaction> CheckoutBookAsync(string memberName, string bookTitle, string isbn);
        Task<Transaction?> ReturnBookAsync(string isbn);
        Task<Transaction?> RenewLoanAsync(string isbn);
        Task<IEnumerable<Transaction>> GetActiveLoansAsync(string? searchText, string? status);
        Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(string? searchText, string? type);
    }
}
