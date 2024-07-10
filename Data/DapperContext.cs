using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace VehicleTrafficManagement.Data
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<T>> ExecuteWithListResultAsync<T>(string procName, IDictionary<string, dynamic> parameters)
        {
            using (var connection = new Npgsql.NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(procName, new DynamicParameters(parameters), commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> ExecuteWithSingleResultAsync<T>(string procName, IDictionary<string, dynamic> parameters)
        {
            using (var connection = new Npgsql.NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<T>(procName, new DynamicParameters(parameters), commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ExecuteWithNoResultAsync(string procName, IDictionary<string, dynamic> parameters)
        {
            using (var connection = new Npgsql.NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(procName, new DynamicParameters(parameters), commandType: CommandType.StoredProcedure);
            }
        }
    }
}
