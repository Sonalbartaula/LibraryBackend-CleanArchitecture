using LibraryBackend_CleanArchitecture.Dtos;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using LibraryBackend_CleanArchitecture.Services.Interfaces;

namespace LibraryBackend_CleanArchitecture.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<ReportsAnalyticsDto> GetDashboardReportAsync()
        {
            return await _reportRepository.GetDashboardReportAsync();
        }
    }
}
