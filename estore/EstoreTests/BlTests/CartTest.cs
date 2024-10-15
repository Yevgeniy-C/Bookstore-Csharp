using Estore.BL.Auth;
using EstoreTests.Helpers;
using System.Transactions;

namespace EstoreTests.BlTests
{
    public class CartTest : Helpers.BaseTest
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
        public async Task AddCartTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var productModel1 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);
                var productModel2 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID2);

                var cartmodel = await cart.GetCurrentUserCart();

                // добавить первый
                await cart.AddCurrentUserCartProduct(productModel1.Product.ProductId);

                // проверить
                cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(productModel1.Product.Price));
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));
                Assert.That(cartmodel.Items[0].ProductCount, Is.EqualTo(1));

                // добавить второй
                await cart.AddCurrentUserCartProduct(productModel1.Product.ProductId);

                // проверить
                cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(productModel1.Product.Price * 2));
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));
                Assert.That(cartmodel.Items[0].ProductCount, Is.EqualTo(2));

                // добавить ещё товар
                await cart.AddCurrentUserCartProduct(productModel2.Product.ProductId);

                // проверить
                cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(productModel1.Product.Price * 2 + productModel2.Product.Price));
                Assert.That(cartmodel.Items.Count, Is.EqualTo(2));
                Assert.That(cartmodel.Items.First(m => m.ProductId == productModel1.Product.ProductId).ProductCount, Is.EqualTo(2));
                Assert.That(cartmodel.Items.First(m => m.ProductId == productModel2.Product.ProductId).ProductCount, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task UpdateCartTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var productModel1 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);

                var cartmodel = await cart.GetCurrentUserCart();

                // добавить 
                await cart.AddCurrentUserCartProduct(productModel1.Product.ProductId);

                await cart.UpdateCurrentUserCartProduct(productModel1.Product.ProductId, 3);

                // проверить
                cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(productModel1.Product.Price * 3));
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));
                Assert.That(cartmodel.Items[0].ProductCount, Is.EqualTo(3));
            }
        }

        [Test]
        public async Task RemoveCartTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var productModel1 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);

                var cartmodel = await cart.GetCurrentUserCart();

                // добавить 
                await cart.AddCurrentUserCartProduct(productModel1.Product.ProductId);

                await cart.UpdateCurrentUserCartProduct(productModel1.Product.ProductId, 0);

                // проверить
                cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(0));
                Assert.That(cartmodel.Items.Count, Is.EqualTo(0));
            }
        }


        [Test]
        public async Task MergeWithExistingUserEmptyCartTest()
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

                webCookie.Delete(AuthConstants.SessionCookieName);
                dbSession.ResetSessionCache();

                var productModel1 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);

                // добавить 
                await cart.AddCurrentUserCartProduct(productModel1.Product.ProductId);
                var cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));

                var sessionData = await dbSession.GetSession();
                var catDalData = await cartDAL.GetCart(sessionData.DbSessionId);
                Assert.IsNull(catDalData.UserId);

                int id = await authBL.Authenticate(email, "qwer1234", true);
                cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));

                catDalData = await cartDAL.GetCart(id);
                Assert.That(catDalData.UserId, Is.EqualTo(id));
                Assert.NotNull(catDalData.UserId);
            }
        }

        [Test]
        public async Task MergeWithExistingUserNotEmptyCartTest()
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

                var productModel1 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);

                await cart.AddCurrentUserCartProduct(productModel1.Product.ProductId);
                var cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));

                webCookie.Delete(AuthConstants.SessionCookieName);
                dbSession.ResetSessionCache();

                var productModel2 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID2);

                // добавить 
                await cart.AddCurrentUserCartProduct(productModel2.Product.ProductId);
                cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));

                var sessionData = await dbSession.GetSession();
                var catDalData = await cartDAL.GetCart(sessionData.DbSessionId);
                Assert.IsNull(catDalData.UserId);

                int id = await authBL.Authenticate(email, "qwer1234", true);
                cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Items.Count, Is.EqualTo(2));

                catDalData = await cartDAL.GetCart(id);
                Assert.That(catDalData.UserId, Is.EqualTo(id));
                Assert.NotNull(catDalData.UserId);
            }
        }
        [Test]
        public async Task MergeWithNewUserCartTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                dbSession.ResetSessionCache();
                string email = Guid.NewGuid().ToString() + "@test.com";

                var productModel1 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);

                // добавить 
                await cart.AddCurrentUserCartProduct(productModel1.Product.ProductId);
                var cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));

                var sessionData = await dbSession.GetSession();
                var catDalData = await cartDAL.GetCart(sessionData.DbSessionId);
                Assert.IsNull(catDalData.UserId, "UserID should be null");

                int id = await authBL.CreateUser(
                    new Estore.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                cartmodel = await cart.GetCurrentUserCart();
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));

                catDalData = await cartDAL.GetCart(id);
                Assert.That(catDalData.UserId, Is.EqualTo(id));
                Assert.NotNull(catDalData.UserId);
            }
        }
    }
}

