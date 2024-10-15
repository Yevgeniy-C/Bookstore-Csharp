using Estore.BL.Models;
using Estore.DAL;
using Estore.DAL.Models;

namespace Estore.BL.Catalog
{
	public class Author: IAuthor
    {
		private readonly IAuthorDAL authorDAL;
        private readonly IProductDAL productDAL;
        private readonly IProductSearchDAL productSearchDAL;

        public Author(IAuthorDAL authorDAL, IProductDAL productDAL, IProductSearchDAL productSearchDAL)
		{
			this.authorDAL = authorDAL;
            this.productDAL = productDAL;
            this.productSearchDAL = productSearchDAL;
        }

        public async Task<AuthorDataModel> GetAuthor(string uniqueid)
		{
            var author = await authorDAL.GetAuthor(uniqueid);

            var productcards = await productSearchDAL.Search(new ProductSearchFilter()
            {
                PageSize = 100,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc,
                AuthorId = author.AuthorId
            });

            return new AuthorDataModel()
            {
                Author = author,
                ProductCards = productcards.ToList()
            };
        }
    }
}

