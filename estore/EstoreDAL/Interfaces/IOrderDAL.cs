using Estore.DAL.Models;

namespace Estore.DAL
{
	public interface IOrderDAL
	{
        Task<int> CreateOrder(OrderModel model);

        Task<int> AddOrderItem(OrderItemModel model);

        Task<IEnumerable<OrderModel>> GetOrders(int userId);

        Task<IEnumerable<OrderItemModel>> GetOrderItems(int orderId);
    }
}

