using LibraryBackend_CleanArchitecture.Model;
using System.Drawing;

namespace LibraryBackend_CleanArchitecture.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(int pageNumber = 1, int pageSize = 20);
        Task<Book?> GetByIdAsync(int id);

        Task<Book> GetABookAsync(string name);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchtext, string category, string status);
        Task<int> SaveChangesAsync();
    }
}
