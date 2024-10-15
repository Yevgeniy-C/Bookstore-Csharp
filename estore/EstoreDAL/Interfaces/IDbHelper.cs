namespace Estore.DAL
{
	public interface IDbHelper
	{
        Task ExecuteAsync(string sql, object model);

        Task<T> QueryScalarAsync<T>(string sql, object model);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object model);
    }
}

