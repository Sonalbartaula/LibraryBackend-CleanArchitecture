using LibraryBackend_CleanArchitecture.Model;

namespace LibraryBackend_CleanArchitecture.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(int pageNumber = 1, int pageSize = 20);
        Task<Book?> GetByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchtext, string category, string status);
        Task<int> SaveChangesAsync();
    }
}
