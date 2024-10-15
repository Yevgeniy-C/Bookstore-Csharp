using Estore.DAL.Models;

namespace Estore.DAL
{
    public class DbSessionDAL : IDbSessionDAL
    {
        private readonly IDbHelper dbHelper;
        public DbSessionDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task Create(SessionModel model)
        {
            string sql = @"insert into DbSession (DbSessionID, SessionData, Created, LastAccessed, UserId)
                    values (@DbSessionID, @SessionData, @Created, @LastAccessed, @UserId)";

            await dbHelper.ExecuteAsync(sql, model);
        }

        public async Task<SessionModel?> Get(Guid sessionId)
        {
            string sql = @"select DbSessionID, SessionData, Created, LastAccessed, UserId from DbSession where DbSessionID = @sessionId";
            var sessions = await dbHelper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
            return sessions.FirstOrDefault();
        }

        public async Task Lock(Guid sessionId)
        {
            string sql = @"select DbSessionID from DbSession where DbSessionID = @sessionId for update";
            await dbHelper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
        }

        public async Task Update(Guid dbSessionID, string sessionData)
        {
            string sql = @"update DbSession
                    set SessionData = @sessionData
                    where DbSessionID = @dbSessionID";

            await dbHelper.ExecuteAsync(sql, new { dbSessionID = dbSessionID, sessionData = sessionData });
        }

        public async Task Extend(Guid dbSessionID)
        {
            string sql = @"update DbSession
                    set LastAccessed = @lastAccessed
                    where DbSessionID = @dbSessionID";

            await dbHelper.ExecuteAsync(sql, new { dbSessionID = dbSessionID, lastAccessed = DateTime.Now });
        }

        public async Task Delete(Guid sessionId)
        {
            string sql = @"delete from DbSession where DbSessionID = @dbSessionID";

            await dbHelper.ExecuteAsync(sql, new { dbSessionID = sessionId });
        }
    }
}

