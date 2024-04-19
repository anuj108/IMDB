using Dapper;
using IMDB.Domain.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IMDB.Repository
{
    public class BaseRepository<T>where T : class
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString=connectionString; 
        }

        public async Task<IEnumerable<T>> Get(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<T>(query);
        }

        public async Task<T> Get(string query,object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<T>(query,parameters);
        }

      

        public async Task<int> Create(string query,object parameters)
        {
            using var connection=new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<int>(query,parameters);
        }

        public async Task Update(string query, object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }
        public async Task Delete(string query, object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }
    }
}
