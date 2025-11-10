using LibraryBackend_CleanArchitecture.Model;

namespace LibraryBackend_CleanArchitecture.Services.Interfaces
{
    public interface ITransactionService
        
    {
        Task<transaction> CheckoutBookAsync(string memberName, string bookTitle, string isbn);
        Task<transaction?> ReturnBookAsync(int transactionId);
        Task<transaction?> RenewLoanAsync(int transactionId);
        Task<IEnumerable<transaction>> GetActiveLoansAsync(string? searchText, string? status);
        Task<IEnumerable<transaction>> GetTransactionHistoryAsync(string? searchText, string? type);
    }
}
