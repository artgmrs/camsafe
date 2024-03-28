using CamSafe.Entity.Entities;

namespace CamSafe.Repository.Interfaces
{
    public interface IReportRepository
    {
        public Task<IEnumerable<Report>> FilterReportsByRangeDateAsync(string startDate, string endDate);
        public Task<IEnumerable<Report>> FilterReportsByCustomerIdAsync(string customerId);
        public Task<IEnumerable<Report>> FilterReportsByDayAsync(string date);
        public Task<int> InsertReportAsync(string ocurredAt, string cameraId);
    }
}
