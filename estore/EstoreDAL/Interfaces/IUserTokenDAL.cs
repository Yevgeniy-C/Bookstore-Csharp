namespace Estore.DAL
{
	public interface IUserTokenDAL
	{
        Task<Guid> Create(int userId);

        Task<int?> Get(Guid tokenid);

        Task DeleteToken(Guid tokenid);
    }
}

