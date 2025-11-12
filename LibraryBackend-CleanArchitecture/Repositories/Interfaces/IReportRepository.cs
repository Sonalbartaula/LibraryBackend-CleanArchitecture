using LibraryBackend_CleanArchitecture.Dtos;

namespace LibraryBackend_CleanArchitecture.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<ReportsAnalyticsDto> GetDashboardReportAsync();
    }
}
