using Estore.DAL.Models;
using EstoreTests.Helpers;
using System.Transactions;
using System.Linq;

namespace EstoreTests.BlTests
{
    public class OrderTest : BaseTest
    {
        const string TEST_PRODUCT_UNIQUE_ID1 = "c-glazami-hakera";
        const string TEST_PRODUCT_UNIQUE_ID2 = "bibliya-c-5-izd";

        [Test]
        public async Task EmptyCartTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(0));
                Assert.IsEmpty(cartmodel.Items);
            }
        }

        [Test]
        public async Task PlaceOrderTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                dbSession.ResetSessionCache();
                string email = Guid.NewGuid().ToString() + "@test.com";

                int userId = await authBL.CreateUser(
                    new Estore.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                // add items
                var productModel1 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);
                var productModel2 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID2);

                await cart.AddCurrentUserCartProduct(productModel1.Product.ProductId);
                await cart.AddCurrentUserCartProduct(productModel2.Product.ProductId);
                var cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Items.Count, Is.EqualTo(2));

                // place order
                int orderId = await cart.PlaceOrder();

                // check cart
                cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Items.Count, Is.EqualTo(0));

                // check order
                var orders = await orderDAL.GetOrders(userId);
                var ordersList = orders.ToList();
                Assert.That(ordersList.Count, Is.EqualTo(1));
                Assert.That(ordersList[0].OrderId, Is.EqualTo(orderId));

                // check order items
                var orderItems = await orderDAL.GetOrderItems(orderId);
                var orderItemsList = orderItems.ToList();
                Assert.That(orderItemsList.Count, Is.EqualTo(2));

                // check one of the products
                var orderproduct1 = orderItems.FirstOrDefault(m => m.ProductId ==productModel1.Product.ProductId );
                Assert.IsNotNull(orderproduct1);
                Assert.That(orderproduct1.ProductCount, Is.EqualTo(1));
            }
        }
    }
}