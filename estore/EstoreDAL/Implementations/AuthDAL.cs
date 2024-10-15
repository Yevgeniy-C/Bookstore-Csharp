using Estore.DAL.Models;

namespace Estore.DAL
{
	public class AuthDAL: IAuthDAL
	{
        private readonly IDbHelper dbHelper;
        public AuthDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<UserModel> GetUser(string email)
        {
            var result = await dbHelper.QueryAsync<UserModel>(@"
                    select UserId, Email, Password, Salt, Status
                    from AppUser
                    where Email = @email", new { email = email });
            return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<UserModel> GetUser(int id)
        {
            var result = await dbHelper.QueryAsync<UserModel>(@"
                        select UserId, Email, Password, Salt, Status
                        from AppUser
                        where UserId = @id", new { id = id });
            return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<int> CreateUser(UserModel model)
        {
            string sql = @"insert into AppUser(Email, Password, Salt, Status)
                    values(@Email, @Password, @Salt, @Status) returning UserId";
            return await dbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<AppRoleModel>> GetRoles(int appUserId)
        {
            return await dbHelper.QueryAsync<AppRoleModel>(@"
                    select ar.AppRoleId, ar.Abbreviation, ar.RoleName
                    from AppRole ar
	                    join AppUserAppRole au on au.AppRoleId = ar.AppRoleId
                    where au.AppUserId = @appUserId
                ", new { appUserId = appUserId });
        }
    }
}

