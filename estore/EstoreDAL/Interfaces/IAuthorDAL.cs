using Estore.DAL.Models;

namespace Estore.DAL
{
	public interface IAuthorDAL
	{
        Task<AuthorModel> GetAuthor(string uniqueid);
    }
}

