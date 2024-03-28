using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;

namespace CamSafe.Business.Interfaces
{
    public interface IReportBusiness
    {
        public Task<IEnumerable<Report>> GetReportsWithFiltersAsync(ReportFilterRequest reportRequest);
        public Task<Report> CreateReportAsync(ReportRequest requestParameters);
    }
}
