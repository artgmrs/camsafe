using CamSafe.Entity.Entities;
using CamSafe.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CamSafe.Repository.Repositories
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public ReportRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<IEnumerable<Report>> FilterReportsByDayAsync(string date)
        {
            var query = @"SELECT rep.ID As Id, rep.OCCURRED_AT AS OccurredAt, rep.CAMERA_ID AS CameraId
                          FROM Reports rep
                              WHERE CONVERT(date, OCCURRED_AT) = @Date";
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Date", date, DbType.String);

                var result = await sqlConnection.QueryAsync<Report>(query, parameters);

                sqlConnection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<Report>> FilterReportsByCustomerIdAsync(string customerId)
        {
            var query = @"SELECT rep.ID As Id, rep.OCCURRED_AT AS OccurredAt, rep.CAMERA_ID AS CameraId
                          FROM Reports rep
	                          JOIN Cameras cam ON cam.ID = rep.CAMERA_ID
	                          JOIN Customers cust ON cust.ID = cam.CUSTOMER_ID
	                          WHERE cust.ID = @CustomerId;";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId, DbType.String);

                var result = await sqlConnection.QueryAsync<Report>(query, parameters);

                sqlConnection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<Report>> FilterReportsByDateAsync(string dateHour)
        {
            var query = @"SELECT rep.ID As Id, rep.OCCURRED_AT AS OccurredAt, rep.CAMERA_ID AS CameraId
                          FROM Reports rep
                              WHERE CONVERT(datetime, OCCURRED_AT) = @DateHour;";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@DateHour", dateHour, DbType.String);

                var result = await sqlConnection.QueryAsync<Report>(query, parameters);

                sqlConnection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<Report>> FilterReportsByRangeDateAsync(string startDate, string endDate)
        {
            var query = @"SELECT rep.ID As Id, rep.OCCURRED_AT AS OccurredAt, rep.CAMERA_ID AS CameraId
                          FROM Reports rep
                              WHERE CONVERT(datetime, OCCURRED_AT) >= @StartDate
                              AND CONVERT(datetime, OCCURRED_AT) <= @EndDate;";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@StartDate", startDate, DbType.String);
                parameters.Add("@EndDate", endDate, DbType.String);

                var result = await sqlConnection.QueryAsync<Report>(query, parameters);

                sqlConnection.Close();

                return result;
            }
        }

        public async Task<int> InsertReportAsync(string occurredAt, string cameraId)
        {
            var query = @"INSERT INTO Reports (OCCURRED_AT, CAMERA_ID)
                              OUTPUT Inserted.Id
                              VALUES (@OccurredAt, @CameraId)";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@OccurredAt", occurredAt, DbType.String);
                parameters.Add("@CameraId", cameraId, DbType.String);

                var result = await sqlConnection.QuerySingleAsync<int>(query, parameters);

                sqlConnection.Close();

                return result;
            }
        }
    }
}
