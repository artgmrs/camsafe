using CamSafe.Entity.CustomExceptions;
using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;
using CamSafe.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CamSafe.Repository.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            // Just to facilitate testing. Not thinking about performance/best practices here.
            var query = @"SELECT ID, NAME 
                              FROM Customers";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var result = await sqlConnection.QueryAsync<Customer>(query);

                sqlConnection.Close();

                return result;
            }
        }

        public async Task<Customer?> GetCustomerByIdAsync(string id)
        {
            var query = @"SELECT ID, NAME 
                              FROM Customers 
                              WHERE Customers.ID = @Id;";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int32);

                var result = await sqlConnection.QuerySingleOrDefaultAsync<Customer>(query, parameters);

                sqlConnection.Close();

                if (result == null)
                {
                    throw new CustomException($"There's no customer with given id '{id}'.");
                }

                return result;
            }
        }

        public async Task<int> InsertCustomerAsync(CustomerRequest customer)
        {
            var query = @"INSERT INTO Customers (NAME)
                              OUTPUT INSERTED.Id
                              VALUES (@Name)";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Name", customer.Name, DbType.String);

                var result = await sqlConnection.QuerySingleAsync<int>(query, parameters);

                sqlConnection.Close();
                return result;
            }
        }
    }
}
