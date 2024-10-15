using Estore.BL.Models;
using Estore.DAL.Models;

namespace Estore.BL.Catalog
{
	public interface ICart
	{
        Task<UserCartModel> GetCurrentUserCart();

        Task AddCurrentUserCartProduct(int productId);

        Task UpdateCurrentUserCartProduct(int productId, int productCount);

        Task MergeCart(Guid oldSessionId, int userId);

        Task UpdateCurrentUserCart(CartModel model);

        Task<int> PlaceOrder();
    }
}

