using LibraryBackend_CleanArchitecture.Dtos;

namespace LibraryBackend_CleanArchitecture.Services.Interfaces
{
    public interface IReportService
    {
        Task<ReportsAnalyticsDto> GetDashboardReportAsync();
    }
}
