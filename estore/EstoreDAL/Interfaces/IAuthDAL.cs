using Estore.DAL.Models;

namespace Estore.DAL
{
	public interface IAuthDAL
	{
		Task<UserModel> GetUser(string email);

        Task<UserModel> GetUser(int id);

        Task<int> CreateUser(UserModel model);

        Task<IEnumerable<AppRoleModel>> GetRoles(int appUserId);
    }
}

