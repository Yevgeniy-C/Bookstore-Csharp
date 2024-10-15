using Estore.DAL.Models;

namespace Estore.DAL
{
	public class AuthorDAL: IAuthorDAL
    {
        private readonly IDbHelper dbHelper;
        public AuthorDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<AuthorModel> GetAuthor(string uniqueid)
        {
            return await dbHelper.QueryScalarAsync<AuthorModel>(
                @"select AuthorId, FirstName, MiddleName, LastName, Description, AuthorImage, UniqueId
                from Author
                where uniqueid = @id", new { id = uniqueid });
        }

    }
}

