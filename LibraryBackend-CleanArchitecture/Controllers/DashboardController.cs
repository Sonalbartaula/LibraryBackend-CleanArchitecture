using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Services.Interfaces;
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

        // MAIN ENDPOINT — RECOMMENDED (One call = everything)
        [HttpGet("summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            try
            {
                var summary = await _dashboardService.GetDashboardSummaryAsync();
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to load dashboard data", error = ex.Message });
            }
        }

        // Optional: Keep individual endpoints if other parts of app use them
        [HttpGet("total-books")]
        public async Task<IActionResult> GetTotalBooks()
            => Ok(await _dashboardService.GetTotalBooksAsync());

        [HttpGet("active-members")]
        public async Task<IActionResult> GetActiveMembers()
            => Ok(await _dashboardService.GetActiveMembersAsync());

        [HttpGet("books-issued")]
        public async Task<IActionResult> GetBooksIssued()
            => Ok(await _dashboardService.GetIssuedBooksCountAsync());

        [HttpGet("overdue-books")]
        public async Task<IActionResult> GetOverdueBooks()
            => Ok(await _dashboardService.GetOverdueBooksCountAsync());

        [HttpGet("books-added-this-month")]
        public async Task<IActionResult> GetBooksAddedThisMonth()
            => Ok(await _dashboardService.GetBooksAddedThisMonthAsync());

        [HttpGet("members-joined-this-month")]
        public async Task<IActionResult> GetMembersJoinedThisMonth()
            => Ok(await _dashboardService.GetMembersJoinedThisMonthAsync());

        [HttpGet("due-soon")]
        public async Task<IActionResult> GetDueSoonCount()
            => Ok(await _dashboardService.GetDueSoonCountAsync());

        [HttpGet("reminders-sent")]
        public async Task<IActionResult> GetRemindersSent()
            => Ok(await _dashboardService.GetRemindersSentCountAsync());

        [HttpGet("recent-activities")]
        public async Task<IActionResult> GetRecentActivities([FromQuery] int count = 10)
            => Ok(await _dashboardService.GetRecentActivitiesAsync(count));

        [HttpGet("popular-books")]
        public async Task<IActionResult> GetPopularBooks([FromQuery] int count = 5)
            => Ok(await _dashboardService.GetPopularBooksAsync(count));
    }
}