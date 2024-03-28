using CamSafe.Business.Interfaces;
using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;
using CamSafe.Repository.Interfaces;
using CamSafe.Utils.Validation;
using System.Globalization;

namespace CamSafe.Business.Businesses
{
    public class ReportBusiness : IReportBusiness
    {
        private readonly IReportRepository _reportRepository;

        public ReportBusiness(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<Report> CreateReportAsync(ReportRequest requestParameters)
        {
            Validation.ValidateDateTimeFromString(requestParameters.OccuredAt);
            Validation.ValidateInt(requestParameters.CameraId);

            var culture = CultureInfo.CreateSpecificCulture("pt-BR");

            var dateLocal = DateTime.Parse(requestParameters.OccuredAt, culture);
            var dateUtcString = dateLocal.ToUniversalTime().ToString("s", System.Globalization.CultureInfo.InvariantCulture);

            var result = await _reportRepository.InsertReportAsync(dateUtcString, requestParameters.CameraId);
            var report = new Report()
            {
                Id = result,
                CameraId = requestParameters.CameraId,
                OccurredAt = dateLocal.ToString("s", System.Globalization.CultureInfo.InvariantCulture)
            };

            return report;
        }

        public async Task<IEnumerable<Report>> GetReportsWithFiltersAsync(ReportFilterRequest reportRequest)
        {
            var shouldUseRangeDateFilter = !string.IsNullOrEmpty(reportRequest.InitialDate) && !string.IsNullOrEmpty(reportRequest.EndDate);
            var shouldUseCustomerFilter = !string.IsNullOrEmpty(reportRequest.CustomerId);
            var shouldUseDateFilter = !string.IsNullOrEmpty(reportRequest.dateHour);

            Validation.ValidateMoreThanOneTrueValue(shouldUseRangeDateFilter, shouldUseCustomerFilter, shouldUseDateFilter);

            var culture = CultureInfo.CreateSpecificCulture("pt-BR");

            IEnumerable<Report> result;
            if (shouldUseRangeDateFilter)
            {
                Validation.ValidateRangeDate(reportRequest.InitialDate, reportRequest.EndDate);

                var startDate = DateTime.Parse(reportRequest.InitialDate, culture).ToUniversalTime().ToString("s", System.Globalization.CultureInfo.InvariantCulture);
                var endDate = DateTime.Parse(reportRequest.EndDate, culture).ToUniversalTime().ToString("s", System.Globalization.CultureInfo.InvariantCulture);

                result = await _reportRepository.FilterReportsByRangeDateAsync(startDate, endDate);
            }
            else if (shouldUseCustomerFilter)
            {
                Validation.ValidateInt(reportRequest.CustomerId);

                result = await _reportRepository.FilterReportsByCustomerIdAsync(reportRequest.CustomerId);
            }
            else if (shouldUseDateFilter)
            {
                Validation.ValidateDateTimeFromString(reportRequest.dateHour);
                var dateHour = DateTime.Parse(reportRequest.dateHour, culture).ToString("s", System.Globalization.CultureInfo.InvariantCulture);

                result = await _reportRepository.FilterReportsByDayAsync(dateHour);
            }
            else
            {
                var currentDate = DateTime.Now.Date.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
                result = await _reportRepository.FilterReportsByDayAsync(currentDate);
            }

            return result;
        }
    }
}
