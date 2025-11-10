using LibraryBackend_CleanArchitecture.Model;

namespace LibraryBackend_CleanArchitecture.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        // Used in ReturnBookAsync and RenewLoanAsync
        Task<transaction?> GetByIdAsync(int id);

        // Used in service/controller for active loans
        Task<IEnumerable<transaction>> GetActiveLoansAsync(string? searchText, string? status);

        // Used in service/controller for transaction history
        Task<IEnumerable<transaction>> GetTransactionHistoryAsync(string? searchText, string? type);

        // Save changes after checkout, return, or renew
        Task SaveChangesAsync();
    }
}
