using Estore.DAL.Models;

namespace Estore.DAL
{
    public class CartDAL : ICartDAL
    {
        private readonly IDbHelper dbHelper;
        public CartDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<int> CreateCart(CartModel model)
        {
            return await dbHelper.QueryScalarAsync<int>(@"
                    insert into Cart (SessionId, UserId, Created, Modified, AddressId, BillingId, IsArchived)
                    values (@SessionId, @UserId, @Created, @Modified, @AddressId, @BillingId, 0)
                    returning CartId", model);
        }

        public async Task<CartModel> GetCart(Guid sessionId)
        {
            return (await dbHelper.QueryScalarAsync<CartModel>(@"
                    select CartId, SessionId, UserId, Created, Modified, AddressId, BillingId
                    from Cart
                    where SessionId = @sessionId 
                      and IsArchived = 0", 
                     new { sessionId = sessionId })) ?? new CartModel() { SessionId = sessionId };
        }

        public async Task<CartModel> GetCart(int userId)
        {
            return (await dbHelper.QueryScalarAsync<CartModel>(@"
                    select CartId, SessionId, UserId, Created, Modified, AddressId, BillingId
                    from Cart
                    where UserId = @userId
                      and IsArchived = 0",
                     new { userId = userId })) ?? new CartModel() { UserId = userId };
        }

        public async Task<int> AddCartItem(CartItemModel model)
        {
            return await dbHelper.QueryScalarAsync<int>(@"
                    insert into CartItem (CartId, ProductId, Created, Modified, ProductCount)
                    values (@CartId, @ProductId, @Created, @Modified, @ProductCount)
                    returning CartItemId", model);
        }

        public async Task<IEnumerable<CartItemDetailsModel>> GetCartItems(int cartId)
        {
            return await dbHelper.QueryAsync<CartItemDetailsModel>(@"
                    select ci.CartItemId, ci.CartId, ci.ProductId, ci.Created, ci.Modified, ci.ProductCount,
                        p.Price, p.ProductImage, p.ProductName, p.UniqueId as ProductUniqueId
                    from CartItem ci
                      join Product p on ci.ProductId = p.ProductId
                    where CartId = @cartId", 
                    new { cartId = cartId });
        }

        public async Task UpdateCartItem(CartItemModel model)
        {
            await dbHelper.ExecuteAsync(@"
                    update CartItem ci
                    set Modified = @Modified, ProductCount = @ProductCount
                    where CartItemId = @CartItemId", 
                    model);
        }

        public async Task DeleteCartItem(int cartItemId)
        {
            await dbHelper.ExecuteAsync(@"
                    delete from CartItem 
                    where CartItemId = @cartItemId", new { cartItemId = cartItemId });
        }

        public async Task SetCartUserId(Guid oldSessionId, int userId)
        {
            await dbHelper.ExecuteAsync(@"
                    update Cart 
                    set UserId = @userId
                    where SessionId = @oldSessionId
                ", new { oldSessionId = oldSessionId, userId = userId });
        }

        public async Task UpdateCart(CartModel model) {
            await dbHelper.ExecuteAsync(@"
                    update Cart 
                    set AddressId = @AddressId,
                        BillingId = @BillingId,
                        IsArchived = @IsArchived
                    where CartId = @CartId
                ", model );
        }

        public async Task MoveCartItems(Guid oldSessionId, int newcartId)
        {
            await dbHelper.ExecuteAsync(@"
                    update CartItem 
                    set CartId = @newcartId 
                    where CartId = (select CartId from Cart where SessionId = @oldSessionId)
                ", new { oldSessionId = oldSessionId, newcartId = newcartId });
        }
    }
}

