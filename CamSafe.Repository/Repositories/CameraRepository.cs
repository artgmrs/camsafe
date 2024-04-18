using CamSafe.Entity.CustomExceptions;
using CamSafe.Entity.Entities;
using CamSafe.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CamSafe.Repository.Repositories
{
    public class CameraRepository : BaseRepository, ICameraRepository
    {
        public CameraRepository(IConfiguration configuration) : base(configuration)
        {
        }

        private async Task CheckCameraIdAsync(string cameraId)
        {
            var query = @"SELECT cam.ID
                          FROM Cameras cam
                          WHERE ID = @CameraId";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@CameraId", cameraId, DbType.String);

                var result = await sqlConnection.QueryAsync<Camera>(query, parameters);

                sqlConnection.Close();

                if (!result.Any())
                {
                    throw new CustomException($"There's no camera for the id '{cameraId}'.");
                }
            }
        }

        public async Task<int> InsertCameraAsync(Camera camera)
        {
            var query = @"INSERT INTO Cameras (NAME, IP, IS_ENABLED, CUSTOMER_ID)
                              OUTPUT INSERTED.Id
                              VALUES (@Name, @Ip, @IsEnabled, @CustomerId)";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Name", camera.Name, DbType.String);
                parameters.Add("@Ip", camera.Ip, DbType.String);
                parameters.Add("@IsEnabled", camera.IsEnabled, DbType.String);
                parameters.Add("@CustomerId", camera.CustomerId, DbType.Int32);

                var result = await sqlConnection.QuerySingleAsync<int>(query, parameters);

                sqlConnection.Close();
                return result;
            }
        }

        public async Task<IEnumerable<Camera>> GetAllCamerasByCustomerIdAsync(string customerId)
        {
            var query = @"SELECT cam.ID, cam.NAME, cam.IP, cam.IS_ENABLED AS IsEnabled, cam.CUSTOMER_ID AS CustomerId
                          FROM Cameras cam
                          WHERE cam.CUSTOMER_ID = @CustomerId";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId, DbType.Int32);

                var result = await sqlConnection.QueryAsync<Camera>(query, parameters);

                if (!result.Any())
                {
                    throw new CustomException($"There's no camera for the customerId '{customerId}'.");
                }

                sqlConnection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<Camera>> GetAllCamerasByStateAsync(string state)
        {
            var query = @"SELECT cam.ID, cam.NAME, cam.IP, cam.IS_ENABLED AS IsEnabled, cam.CUSTOMER_ID AS CustomerId
                          FROM Cameras cam
                          WHERE IS_ENABLED = @State";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@State", state, DbType.String);

                var result = await sqlConnection.QueryAsync<Camera>(query, parameters);

                if (!result.Any())
                {
                    throw new CustomException($"There's no camera for the state '{state}'.");
                }

                sqlConnection.Close();

                return result;
            }
        }

        public async Task<bool> CustomerHasGivenIpAsync(string cameraIp, string customerId)
        {
            var query = @"SELECT count(*)
                                  FROM Customers
	                                  JOIN Cameras cam ON Customers.ID = cam.CUSTOMER_ID
                                  WHERE
	                                  cam.IP = @Ip
	                                  AND cam.CUSTOMER_ID = @CustomerId";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Ip", cameraIp, DbType.String);
                parameters.Add("@CustomerId", customerId, DbType.Int32);

                var result = await sqlConnection.QueryFirstAsync<int>(query, parameters);

                sqlConnection.Close();

                return result != 0;
            }
        }

        public async Task ActivateCameraAsync(string cameraId)
        {
            var query = @"UPDATE Cameras
                              SET IS_ENABLED = 'true'
                              WHERE ID = @CameraId";

            await CheckCameraIdAsync(cameraId);

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@CameraId", cameraId, DbType.Int32);

                var result = await sqlConnection.ExecuteAsync(query, parameters);

                sqlConnection.Close();
            }
        }


        public async Task DeactivateCameraAsync(string cameraId)
        {
            var query = @"UPDATE Cameras
                              SET IS_ENABLED = 'false'
                              WHERE ID = @CameraId";

            await CheckCameraIdAsync(cameraId);

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@CameraId", cameraId, DbType.Int32);

                await sqlConnection.ExecuteAsync(query, parameters);

                sqlConnection.Close();
            }
        }
    }
}
