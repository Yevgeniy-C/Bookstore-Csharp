namespace Estore.DAL
{
	public class UserTokenDAL : IUserTokenDAL
    {
        private readonly IDbHelper dbHelper;
        public UserTokenDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<Guid> Create(int userid)
        {
            Guid tockenid = Guid.NewGuid();
            string sql = @"insert into UserToken (UserTokenID, UserId, Created)
                    values (@tockenid, @userid, NOW())";

            await dbHelper.ExecuteAsync(sql, new { userid = userid, tockenid = tockenid });
            return tockenid;
        }

        public async Task<int?> Get(Guid tokenid)
        {
            string sql = @"select UserId from UserToken where UserTokenID = @tokenid";
            return await dbHelper.QueryScalarAsync<int?>(sql, new { tokenid = tokenid });
        }

        public async Task DeleteToken(Guid tokenid) {
            string sql = @"delete from UserToken where UserTokenId = @tokenid";

            await dbHelper.ExecuteAsync(sql, new { tokenid = tokenid });
        }
    }
}

