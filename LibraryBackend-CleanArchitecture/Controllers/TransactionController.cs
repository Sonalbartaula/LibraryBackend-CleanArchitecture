using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend_CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IBookService _bookService;

        public TransactionController(ITransactionService transactionService, IBookService bookService)
        {
            _transactionService = transactionService;
            _bookService = bookService;
        }

        
        [HttpPost("Checkout")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> CheckoutBook([FromBody] CheckoutRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(request.MemberName) || string.IsNullOrEmpty(request.BookTitle))
                return BadRequest("Member name and book title are required.");

            var bookAvaible = await _bookService.GetABookAsync(request.BookTitle);
            if (bookAvaible == null)
                return NotFound("Book not found.");

            var transaction = await _transactionService.CheckoutBookAsync(
                request.MemberName,
                request.BookTitle,
                request.Isbn
            );

            if (transaction == null)
                return BadRequest("Failed to checkout book.");

            return Ok(transaction);
        }


        [HttpPut("Return/{isbn}")] 
        [Authorize(Roles = "Admin,Librarian")] 
        public async Task<IActionResult> ReturnBook(string isbn) {
        var transaction = await _transactionService.ReturnBookAsync(isbn); 
            if (transaction == null) 
                return 
                    NotFound("Transaction not found or already returned.");
            return Ok(transaction); }


        [HttpPut("Renew/{isbn}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> RenewLoan(string isbn)
        {
            var transaction = await _transactionService.RenewLoanAsync(isbn);
            if (transaction == null)
                return NotFound("Transaction not found or cannot be renewed.");

            return Ok(transaction);
        }

        
        [HttpGet("ActiveLoans")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetActiveLoans([FromQuery] string? searchText, [FromQuery] string? status)
        {
            var result = await _transactionService.GetActiveLoansAsync(searchText, status);
            return Ok(result);
        }

        
        [HttpGet("History")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetTransactionHistory([FromQuery] string? searchText, [FromQuery] string? type)
        {
            var result = await _transactionService.GetTransactionHistoryAsync(searchText, type);
            return Ok(result);
        }
    }

    
    public class CheckoutRequest
    {
        public string MemberName { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
    }

    public class ReturnRequest
    {
        public string Isbn { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty; 
    }
}
