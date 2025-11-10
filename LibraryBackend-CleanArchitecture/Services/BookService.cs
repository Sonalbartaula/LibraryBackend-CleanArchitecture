using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using LibraryBackend_CleanArchitecture.Services.Interfaces;

namespace LibraryBackend_CleanArchitecture.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IActivityRepository _activityRepository;
        public BookService(IBookRepository bookRepository, IActivityRepository activityRepository)
        {
            _bookRepository = bookRepository;
            _activityRepository = activityRepository;
        }   

        public async Task<IEnumerable<Book>> GetAllBooksAsync(int pageNumber = 1, int pageSize = 20)
        {
            return await _bookRepository.GetAllBooksAsync(pageNumber, pageSize);
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            var returnbook = await _bookRepository.AddBookAsync(book);
            var activity = new Activity
            {
                Type = ActivityType.BookAdded,             // Enum type for the activity
                Title = returnbook.Title,             // Title of book added
                Subtitle = returnbook.Author,           // Author Name displayed
                Date = DateTime.UtcNow                       // Timestamp
            };

            await _activityRepository.AddAsync(activity);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateBookAsync(book);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchtext, string category, string status)
        {
            return await _bookRepository.SearchBooksAsync(searchtext, category, status);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _bookRepository.SaveChangesAsync();

        }
    }
}
