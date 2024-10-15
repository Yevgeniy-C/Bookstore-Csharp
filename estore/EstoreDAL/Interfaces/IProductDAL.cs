using Estore.DAL.Models;

namespace Estore.DAL
{
	public interface IProductDAL
	{
        Task<ProductModel> GetProduct(string uniqueid);

        Task<IEnumerable<ProductDetailModel>> GetProductDetails(int productid);

        Task<IEnumerable<AuthorModel>> GetAuthorByProduct(int productid);

        Task<CategoryModel> GetCategory(int categoryid);

        Task<int?> GetCategoryId(IEnumerable<string> uniqueids);

        Task<IEnumerable<CategoryModel>> GetChildCategories(int? categoryid);

        Task<int> AddCategory(CategoryModel model);

        Task<IEnumerable<ProductSerieModel>> LoadProductSeries();
    }
}

