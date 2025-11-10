using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend_CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookservice;

        public BookController(IBookService bookservice)
        {
            _bookservice = bookservice;
        }

        [HttpGet("Books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookservice.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpPost("AddBooks")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book data is required.");
            }

            await _bookservice.AddBookAsync(book);

            return Ok("Book added successfully.");
        }
        //Need to look at this again in the future 
        [HttpPut("UpdateBooks")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook([FromBody] Book updatedBook)
        {
            if (updatedBook == null || updatedBook.Id == 0)
            {
                return BadRequest("Valid book data is required.");
            }

            var existingBook = await _bookservice.GetByIdAsync(updatedBook.Id);
            if (existingBook == null)
            {
                return NotFound("Book not found.");
            }

           
            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.ISBN = updatedBook.ISBN;
            existingBook.Categories = updatedBook.Categories;
            existingBook.Status = updatedBook.Status;
            

            await _bookservice.UpdateBookAsync(existingBook);

            return Ok("Book updated successfully.");
        }

        [HttpDelete("DeleteBooks/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookservice.DeleteBookAsync(id);
            return Ok("Book deleted successfully.");
            
        }

        [HttpGet("SearchBooks")]
        public async Task<IActionResult> SearchBooks(
             [FromQuery] string? searchText = null,
             [FromQuery] string? category = null,
             [FromQuery] string? status = null)
        {
            var books = await _bookservice.SearchBooksAsync(searchText, status, category);
            return Ok(books);
        }


    }
}
