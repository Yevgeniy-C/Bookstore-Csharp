using Estore.DAL.Models;
using EstoreTests.Helpers;
using System.Transactions;

namespace EstoreTests.BlTests
{
    public class CheckoutBillingTest : BaseTest
    {
        const string TEST_PRODUCT_UNIQUE_ID1 = "c-glazami-hakera";

        [Test]
        public async Task AnonamousBillingTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var userCart = await cart.GetCurrentUserCart();
                Assert.IsNull(userCart.Cart.BillingId);

                BillingModel model = CreateBilling();
                model.UserId = await currentUser.GetCurrentUserId() ?? 0;
                Assert.Throws<Exception>(delegate { billing.Save(model).GetAwaiter().GetResult(); });
            }
        }

        [Test]
        public async Task LoggedInEmptyBillingCartUpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                int userId = await authBL.CreateUser(new Estore.DAL.Models.UserModel()  { Email =  Guid.NewGuid().ToString() + "@test.com", Password = "qwer1234"});

                //add billing
                var userCart = await cart.GetCurrentUserCart();
                Assert.IsNull(userCart.Cart.BillingId);

                // create billing
                BillingModel model = CreateBilling();
                model.UserId = await currentUser.GetCurrentUserId() ?? 0;
                int billingid = await billing.Save(model);
                Assert.Greater(billingid, 0);

                // update cart
                userCart.Cart.BillingId = billingid;
                await this.cart.UpdateCurrentUserCart(userCart.Cart);
                
                // pull latest
                userCart = await cart.GetCurrentUserCart();
                Assert.That(userCart.Cart.BillingId, Is.EqualTo(billingid));
            }
        }

        [Test]
        public async Task LoggedInNotEmptyBillingCartUpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                int userId = await authBL.CreateUser(new Estore.DAL.Models.UserModel()  { Email =  Guid.NewGuid().ToString() + "@test.com", Password = "qwer1234"});
                
                // add product
                var productModel1 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);
                await cart.AddCurrentUserCartProduct(productModel1.Product.ProductId);

                //add billing
                var userCart = await cart.GetCurrentUserCart();
                Assert.IsNull(userCart.Cart.BillingId);

                // create billing
                BillingModel model = CreateBilling();
                model.UserId = userId;
                int billingid = await billing.Save(model);

                // update cart
                userCart.Cart.BillingId = billingid;
                await this.cart.UpdateCurrentUserCart(userCart.Cart);
                
                // pull latest
                userCart = await cart.GetCurrentUserCart();
                Assert.That(userCart.Cart.BillingId, Is.EqualTo(billingid));
            }
        }

        [Test]
        public async Task LoggedInBillingTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                int userId = await authBL.CreateUser(new Estore.DAL.Models.UserModel()  { Email =  Guid.NewGuid().ToString() + "@test.com", Password = "qwer1234"});
                
                //add billing
                var userCart = await cart.GetCurrentUserCart();
                Assert.IsNull(userCart.Cart.AddressId);

                // create billing
                BillingModel model = CreateBilling();
                model.UserId = await currentUser.GetCurrentUserId() ?? 0;
                int billingid = await billing.Save(model);

                // check db
                BillingModel dbModel = await billing.GetBilling(billingid);
                Assert.That(dbModel.CardNumber, Is.EqualTo("4111111111111111"));
                Assert.That(dbModel.CardType, Is.EqualTo(model.CardType));
                Assert.That(dbModel.OwnerName, Is.EqualTo("Tester"));
                Assert.That(dbModel.ExpYear, Is.EqualTo("70"));
                Assert.That(dbModel.ExpMonth, Is.EqualTo("10"));
            }
        }

        [Test]
        public async Task LoggedInBillingUpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                int userId = await authBL.CreateUser(new Estore.DAL.Models.UserModel()  { Email =  Guid.NewGuid().ToString() + "@test.com", Password = "qwer1234"});
                
                //add billing
                var userCart = await cart.GetCurrentUserCart();
                Assert.IsNull(userCart.Cart.AddressId);

                // create billing
                BillingModel model = CreateBilling();
                model.UserId = await currentUser.GetCurrentUserId() ?? 0;
                int billingid = await billing.Save(model);

                model.BillingId = billingid;
                model.CardNumber = "4111111111111222";
                model.ExpMonth = "11";
                model.ExpYear = "80";
                model.OwnerName = "Test Updated";
                await billing.Save(model);

                // check db
                BillingModel dbModel = await billing.GetBilling(billingid);
                Assert.That(dbModel.CardNumber, Is.EqualTo("4111111111111222"));
                Assert.That(dbModel.CardType, Is.EqualTo(model.CardType));
                Assert.That(dbModel.OwnerName, Is.EqualTo("Test Updated"));
                Assert.That(dbModel.ExpYear, Is.EqualTo("80"));
                Assert.That(dbModel.ExpMonth, Is.EqualTo("11"));
            }
        }

        public static BillingModel CreateBilling() {
            return new BillingModel() {
                CardNumber = "4111111111111111",
                CardType = 0,
                ExpMonth = "10",
                ExpYear = "70",
                OwnerName = "Tester"
            };
        }
    }
}