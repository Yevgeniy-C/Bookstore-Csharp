using Estore.BL.Models;
using Estore.DAL.Models;

namespace Estore.BL.Catalog
{
	public interface IProduct
	{
        Task<IEnumerable<ProductCardModel>> GetNew(int top);

        Task<CompleteProductDataModel> GetProduct(string uniqueid);

        Task<Tuple<int, IEnumerable<ProductCardModel>>> GetByCategory(int categoryId, int pagesize, int page);

        Task<Tuple<int, IEnumerable<ProductCardModel>>> GetBySerie(string serieName, int pagesize, int page);

        Task<Tuple<int, IEnumerable<ProductCardModel>>> Search(string searchFor, int pagesize, int page);

        Task<int?> GetCategoryId(IEnumerable<string> uniqueids);

        Task<IEnumerable<CategoryModel>> GetChildCategories(int? categoryid);

        Task<int> AddCategory(int? categoryid, string name);

        Task<IEnumerable<CategoryModel>> GetCategoryTree(int categoryid);
    }
}

