using Estore.BL.Models;

namespace Estore.BL.Catalog
{
	public interface IAuthor
	{
		Task<AuthorDataModel> GetAuthor(string uniqueid);
	}
}

