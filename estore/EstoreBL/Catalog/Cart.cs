using System.Transactions;
using Estore.BL.Auth;
using Estore.BL.General;
using Estore.BL.Models;
using Estore.DAL;
using Estore.DAL.Models;

namespace Estore.BL.Catalog
{
	public class Cart : ICart
    {
        private readonly ICartDAL cartDAL;
        private readonly IOrderDAL orderDAL;
        private readonly ICurrentUser currentUser;
        private readonly IDbSession dbsession;

        public Cart(ICartDAL cartDAL, ICurrentUser currentUser, IDbSession dbsession, IOrderDAL orderDAL)
		{
            this.currentUser = currentUser;
            this.cartDAL = cartDAL;
            this.dbsession = dbsession;
            this.orderDAL = orderDAL;
        }

        public async Task<CartModel> GetCurrentUserCartModel()
        {
            bool isLoggedIn = await currentUser.IsLoggedIn();
            if (isLoggedIn)
            {
                int? userId = await currentUser.GetCurrentUserId();
                return await this.cartDAL.GetCart((int)userId);
            }
            else
            {
                var session = await dbsession.GetSession();
                return await this.cartDAL.GetCart(session.DbSessionId);
            }
        }

        public async Task<CartModel> CreateOrGetCurrentUserCartModel()
        {
            var cartModel = await GetCurrentUserCartModel();
            if (cartModel.CartId == null)
                cartModel.CartId = await CreateCurrentUserCart();
            return cartModel;
        }

        public async Task<UserCartModel> GetCurrentUserCart()
        {
            var cartModel = await GetCurrentUserCartModel();

            if (cartModel.CartId == null)
                return new UserCartModel() {
                    Cart = cartModel
                };

            var cartItems = (await this.cartDAL.GetCartItems((int)cartModel.CartId)).ToList();

            return new UserCartModel()
            {
                Cart = cartModel,
                Items = cartItems
            };
        }

        public async Task AddCurrentUserCartProduct(int productId)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, Constants.TRANSACTION_SECCONDS),
                TransactionScopeAsyncFlowOption.Enabled
                ))
            {
                await this.dbsession.Lock();

                CartModel cartModel = await CreateOrGetCurrentUserCartModel();

                CartItemModel? cartItemModel = (await this.cartDAL.GetCartItems(cartModel.CartId ?? 0)).FirstOrDefault(m => m.ProductId == productId);
                if (cartItemModel == null)
                {
                    cartItemModel = new CartItemModel()
                    {
                        CartId = cartModel.CartId ?? 0,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        ProductId = productId,
                        ProductCount = 1
                    };
                    await cartDAL.AddCartItem(cartItemModel);
                }
                else
                {
                    cartItemModel.Modified = DateTime.Now;
                    cartItemModel.ProductCount = cartItemModel.ProductCount + 1;
                    await cartDAL.UpdateCartItem(cartItemModel);
                }
                scope.Complete();
            }

            await UpdateSessionCartValue();
        }

        public async Task<int> CreateCurrentUserCart()
        {
            CartModel cartModel = new CartModel();
            cartModel.Modified = DateTime.Now;
            cartModel.Created = DateTime.Now;

            if (await currentUser.IsLoggedIn())
                cartModel.UserId = await currentUser.GetCurrentUserId();
            cartModel.SessionId =  (await dbsession.GetSession()).DbSessionId;

            return await cartDAL.CreateCart(cartModel);
        }

        public async Task UpdateCurrentUserCartProduct(int productId, int productCount)
        {
            CartModel cartModel = await CreateOrGetCurrentUserCartModel();

            CartItemModel? cartItemModel = (await this.cartDAL.GetCartItems(cartModel.CartId ?? 0)).FirstOrDefault(m => m.ProductId == productId);
            if (cartItemModel == null)
                return;
            if (productCount > 0)
            {
                cartItemModel.Modified = DateTime.Now;
                cartItemModel.ProductCount = productCount;
                await cartDAL.UpdateCartItem(cartItemModel);
            }
            else
                await cartDAL.DeleteCartItem(cartItemModel.CartItemId);

            await UpdateSessionCartValue();
        }

        public async Task MergeCart(Guid oldSessionId, int userId)
        {
            var cartModel = await GetCurrentUserCartModel();
            if (cartModel.CartId == null)
                await cartDAL.SetCartUserId(oldSessionId, userId);
            else
                await cartDAL.MoveCartItems(oldSessionId, (int)cartModel.CartId);

            await UpdateSessionCartValue();
        }

        public async Task UpdateSessionCartValue()
        {
            var model = await GetCurrentUserCart();
            dbsession.AddValue(Constants.CART_PARAM_NAME, model.Count);
            await dbsession.UpdateSessionData();
        }

        public async Task UpdateCurrentUserCart(CartModel model) {
            if (model.CartId == null) {
                model.CartId = await CreateCurrentUserCart();
            }
            await this.cartDAL.UpdateCart(model);
        }

        public async Task<int> PlaceOrder() {
            int orderId = 0;
            using (var scope = Helpers.CreateTransactionScope()) {
                var cartModel = await GetCurrentUserCartModel();
                var cartItems = (await this.cartDAL.GetCartItems(cartModel.CartId ?? 0)).ToList();

                if (!ChargeCreditCard()) 
                    throw new OrderException("Cannot charge CreditCard");
                
                orderId = await CreateOrder(cartModel, cartItems);
                await ArchiveCart(cartModel);

                scope.Complete();
            }
            
            await UpdateSessionCartValue();
            return orderId;
        }

        private async Task<int> CreateOrder(CartModel cartModel, List<CartItemDetailsModel> cartItems) {
            OrderModel orderModel = new OrderModel(cartModel);
            int orderId = await orderDAL.CreateOrder(orderModel);

            foreach (CartItemDetailsModel cartItem in cartItems) {
                OrderItemModel orderItemModel = new OrderItemModel(cartItem);
                orderItemModel.OrderId = orderId;
                orderItemModel.Price = cartItem.Price;
                await orderDAL.AddOrderItem(orderItemModel);
            }

            return orderId;
        }

        private async Task ArchiveCart(CartModel cartModel) {
            cartModel.IsArchived = 1;
            await this.cartDAL.UpdateCart(cartModel);
        }

        private bool ChargeCreditCard() {
            return true;
        }
    }
}

