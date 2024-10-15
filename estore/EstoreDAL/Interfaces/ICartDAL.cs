using Estore.DAL.Models;

namespace Estore.DAL
{
	public interface ICartDAL
	{
		Task<CartModel> GetCart(Guid sessionId);

        Task<CartModel> GetCart(int userId);

        Task<int> CreateCart(CartModel model);

        Task<IEnumerable<CartItemDetailsModel>> GetCartItems(int cartId);

        Task<int> AddCartItem(CartItemModel model);

        Task UpdateCartItem(CartItemModel model);

        Task DeleteCartItem(int cartItemId);

        Task SetCartUserId(Guid oldSessionId, int userId);

        Task MoveCartItems(Guid oldSessionId, int newcartId);

        Task UpdateCart(CartModel model);
    }
}

