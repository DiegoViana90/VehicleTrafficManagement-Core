using System.Data;
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

        public IEnumerable<T> ExecuteWithMultipleResults<T>(string procName, IDictionary<string, dynamic> parameters)
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                connection.Open();
                return connection.Query<T>(procName, new DynamicParameters(parameters), commandType: CommandType.StoredProcedure);
            }
        }

        public T ExecuteWithSingleResult<T>(string procName, IDictionary<string, dynamic> parameters)
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                connection.Open();
                return connection.QueryFirstOrDefault<T>(procName, new DynamicParameters(parameters), commandType: CommandType.StoredProcedure);
            }
        }

        public void ExecuteWithNoResult(string procName, IDictionary<string, dynamic> parameters)
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                connection.Open();
                connection.Execute(procName, new DynamicParameters(parameters), commandType: CommandType.StoredProcedure);
            }
        }
    }
}
