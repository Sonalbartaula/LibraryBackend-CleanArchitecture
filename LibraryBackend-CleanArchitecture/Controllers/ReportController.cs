using LibraryBackend_CleanArchitecture.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend_CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("ReportsAndAnalytics")]
        public async Task<IActionResult> GetDashboardReport()
        {
            var report = await _reportService.GetDashboardReportAsync();
            return Ok(report);
        }
    }
}
