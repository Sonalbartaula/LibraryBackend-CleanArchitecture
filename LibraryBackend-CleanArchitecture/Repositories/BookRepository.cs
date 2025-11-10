using LibraryBackend_CleanArchitecture.Data;
using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryBackend_CleanArchitecture.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(int pageNumber = 1, int pageSize = 20)
        {
            pageSize = Math.Min(pageSize, 100);
            return await _context.Books
            .OrderBy(b => b.Id) 
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize)                     
            .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            return book;
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchText, string status, string categories)
        {
            var query = _context.Books.AsQueryable();

           
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(b =>
                    b.Title.Contains(searchText) ||
                    b.Author.Contains(searchText) ||
                    b.ISBN.Contains(searchText)
                );
            }

            
            if (!string.IsNullOrEmpty(categories))
            {
                query = query.Where(b => b.Categories.Contains(categories));
            }


            if (!string.IsNullOrEmpty(status) && Enum.TryParse<Bookstatus>(status, true, out var parsedStatus))
            {
                query = query.Where(b => b.Status == parsedStatus);
            }


            return await query.ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
       
    }
}
