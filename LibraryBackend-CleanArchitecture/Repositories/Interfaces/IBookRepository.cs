using LibraryBackend_CleanArchitecture.Model;

namespace LibraryBackend_CleanArchitecture.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(int pageNumber, int pageSize);
        Task<Book?>GetByIdAsync(int id);
        Task<Book>AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchText, string status, string categories);
        Task<int> SaveChangesAsync();

    }
}
