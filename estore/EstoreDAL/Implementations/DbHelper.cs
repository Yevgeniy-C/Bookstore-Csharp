using Npgsql;
using Dapper;
using System.Text.Json;
using System.Diagnostics;

namespace Estore.DAL
{
	public class DbHelper: IDbHelper
	{
        private readonly IMetricDAL metricDAL;
        public DbHelper(IMetricDAL metricDAL)
        {
            this.metricDAL = metricDAL;
        }

        public static string ConnString = "";

        public async Task ExecuteAsync(string sql, object model)
        {
            var timer = new Stopwatch();
            timer.Start();

            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, model);
            }
            timer.Stop();
            
            metricDAL.Add(new MetricData() { SQL = sql, Parameters = JsonSerializer.Serialize(model), Elapsed = timer.Elapsed });
        }

        public async Task<T> QueryScalarAsync<T>(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                var timer = new Stopwatch();
                timer.Start();

                await connection.OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<T>(sql, model);

                timer.Stop();

                metricDAL.Add(new MetricData() { SQL = sql, Parameters = JsonSerializer.Serialize(model), Elapsed = timer.Elapsed });
                return result;
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                var timer = new Stopwatch();
                timer.Start();

                await connection.OpenAsync();
                var result = await connection.QueryAsync<T>(sql, model);

                timer.Stop();

                metricDAL.Add(new MetricData() { SQL = sql, Parameters = JsonSerializer.Serialize(model), Elapsed = timer.Elapsed });
                return result;
            }
        }
    }
}

