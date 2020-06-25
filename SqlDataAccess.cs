using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary {
    public class SqlDataAccess : ISqlDataAccess {
        private readonly IConfiguration _config;
        public string ConnectionStringName { get; set; } = "Default";
        public SqlDataAccess(IConfiguration config) {
            _config = config;
        }
        public async Task<List<T>> LoadData<T, U>(string sql, U paramater) {
            string conectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(conectionString)) {
                var data = await connection.QueryAsync<T>(sql, paramater);

                return data.ToList();
            }
        }
        public async Task SaveData<T>(string sql, T paramater) {
            string conectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(conectionString)) {
                await connection.ExecuteAsync(sql, paramater);
            }
        }
    }
}
