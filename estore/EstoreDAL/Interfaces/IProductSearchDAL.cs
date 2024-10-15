using Estore.DAL.Models;

namespace Estore.DAL
{
	public interface IProductSearchDAL
	{
        Task<IEnumerable<ProductCardModel>> Search(ProductSearchFilter filter);

        Task<int> GetCount(ProductSearchFilter filter);
    }
}

