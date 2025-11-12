using LibraryBackend_CleanArchitecture.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend_CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet("RecentActivities")]
        public async Task<IActionResult> GetRecentActivities(int count = 5)
        {
            var activities = await _dashboardService.GetRecentActivitiesAsync(count);
            return Ok(activities);
        }

        [HttpGet("PopularBooks")]
        public async Task<IActionResult> GetPopularBooks([FromQuery] int count = 5)
        {
            var popularBooks = await _dashboardService.GetPopularBooksAsync(count);
            return Ok(popularBooks);
        }

        [HttpGet("TotalBooks")]
        public async Task<IActionResult> GetTotalBooks()
        {
            var totalBooks = await _dashboardService.GetTotalBooksAsync();
            return Ok(totalBooks);
        }

        [HttpGet("ActiveMembers")]
        public async Task<IActionResult> GetActiveMembers()
        {
            var activeMembers = await _dashboardService.GetActiveMembersAsync();
            return Ok(activeMembers);
        }

        [HttpGet("BooksIssued")]
        public async Task<IActionResult> GetIssuedBooks()
        {
            var booksIssued = await _dashboardService.GetIssuedBooksCountAsync();
            return Ok(booksIssued);

        }

        [HttpGet("OverdueBooks")]
        public async Task<IActionResult> GetOverdueBooks()
        {
            var overdueBooks = await _dashboardService.GetOverdueBooksCountAsync();
            return Ok(overdueBooks);
        }
    }
}

