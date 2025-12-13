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

            var bookAvailable = await _bookService.GetABookAsync(request.BookTitle);
            if (bookAvailable == null)
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

        
        [HttpPut("Return/{transactionId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> ReturnBook(int transactionId)
        {
            try
            {
                var transaction = await _transactionService.ReturnBookByTransactionIdAsync(transactionId);

                if (transaction == null)
                    return NotFound("Transaction not found or already returned.");

                return Ok(new
                {
                    message = "Book returned successfully",
                    transaction
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //[HttpPost("Return")]
        //[Authorize(Roles = "Admin,Librarian")]
        //public async Task<IActionResult> ReturnBookByMemberAndIsbn([FromBody] ReturnRequest request)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(request.MemberName) || string.IsNullOrEmpty(request.Isbn))
        //            return BadRequest("Member name and ISBN are required.");

        //        var transaction = await _transactionService.ReturnBookByMemberAndIsbnAsync(
        //            request.MemberName,
        //            request.Isbn
        //        );

        //        if (transaction == null)
        //            return NotFound("Active transaction not found for this member and book.");

        //        return Ok(new
        //        {
        //            message = "Book returned successfully",
        //            transaction
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        [HttpPut("Renew/{transactionId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> RenewLoan(int transactionId)
        {
            try
            {
                var transaction = await _transactionService.RenewLoanByTransactionIdAsync(transactionId);

                if (transaction == null)
                    return NotFound("Transaction not found or cannot be renewed.");

                return Ok(new
                {
                    message = "Loan renewed successfully",
                    transaction
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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