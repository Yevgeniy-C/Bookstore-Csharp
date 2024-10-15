using Estore.DAL.Models;

namespace Estore.DAL
{
    public class OrderDAL : IOrderDAL
    {
        private readonly IDbHelper dbHelper;
        public OrderDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<int> CreateOrder(OrderModel model)
        {
            return await dbHelper.QueryScalarAsync<int>("""
                    insert into "Order" (SessionId, UserId, Created, Modified, AddressId, BillingId, CartId)
                    values (@SessionId, @UserId, @Created, @Modified, @AddressId, @BillingId, @CartId)
                    returning OrderId
                    """, model);
        }

        public async Task<int> AddOrderItem(OrderItemModel model)
        {
            return await dbHelper.QueryScalarAsync<int>(@"
                    insert into OrderItem (OrderId, ProductId, Created, Modified, ProductCount, Price)
                    values (@OrderId, @ProductId, @Created, @Modified, @ProductCount, @Price)
                    returning OrderItemId", model);
        }

        public async Task<IEnumerable<OrderModel>> GetOrders(int userId)
        {
            return await dbHelper.QueryAsync<OrderModel>("""
                    select OrderId, SessionId, UserId, Created, Modified, AddressId, BillingId, CartId
                    from "Order" 
                    where UserId = @userId
                    """, 
                    new { userId = userId });
        }

        public async Task<IEnumerable<OrderItemModel>> GetOrderItems(int orderId)
        {
            return await dbHelper.QueryAsync<OrderItemModel>("""
                    select OrderItemId, OrderId, ProductId, Created, Modified, ProductCount, Price
                    from OrderItem 
                    where orderId = @orderId
                    """, 
                    new { orderId = orderId });
        }
    }
}

