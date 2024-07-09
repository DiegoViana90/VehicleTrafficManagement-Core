using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace VehicleTrafficManagement.Data
{
    public class DapperContext
    {
        private readonly ApplicationDbContext _dbContext;

        public DapperContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> ExecuteWithMultipleResultsAsync<T>(string procName, IDictionary<string, dynamic> parameters)
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(procName, new DynamicParameters(parameters), commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> ExecuteWithSingleResultAsync<T>(string procName, IDictionary<string, dynamic> parameters)
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<T>(procName, new DynamicParameters(parameters), commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ExecuteWithNoResultAsync(string procName, IDictionary<string, dynamic> parameters)
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(procName, new DynamicParameters(parameters), commandType: CommandType.StoredProcedure);
            }
        }
    }
}
