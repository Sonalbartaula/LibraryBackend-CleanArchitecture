using LibraryBackend_CleanArchitecture.Model;
using System.Drawing;

namespace LibraryBackend_CleanArchitecture.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        // Used in ReturnBookAsync and RenewLoanAsync
        Task<Transaction?> GetByIsbn(string isbn);
        Task AddAsync(Transaction transaction);

        // Used in service/controller for active loans
        Task<IEnumerable<Transaction>> GetActiveLoansAsync(string? searchText, string? status);
        

        // Used in service/controller for transaction history
        Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(string? searchText, string? type);

        // Save changes after checkout, return, or renew
        Task SaveChangesAsync();
    }
}
