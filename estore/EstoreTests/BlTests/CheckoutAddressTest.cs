using Estore.DAL.Models;
using EstoreTests.Helpers;
using System.Transactions;

namespace EstoreTests.BlTests
{
    public class CheckoutAddressTest : BaseTest
    {
        const string TEST_PRODUCT_UNIQUE_ID1 = "c-glazami-hakera";

        [Test]
        public async Task AnonamousAddressTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var userCart = await cart.GetCurrentUserCart();
                Assert.IsNull(userCart.Cart.AddressId);

                AddressModel addressModel = CreateAddress ();
                addressModel.UserId = await currentUser.GetCurrentUserId() ?? 0;
                Assert.Throws<Exception>(delegate { address.Save(addressModel).GetAwaiter().GetResult(); });
            }
        }

        [Test]
        public async Task LoggedInEmptyAddressCartUpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                int userId = await authBL.CreateUser(new Estore.DAL.Models.UserModel()  { Email =  Guid.NewGuid().ToString() + "@test.com", Password = "qwer1234"});

                //add address
                var userCart = await cart.GetCurrentUserCart();
                Assert.IsNull(userCart.Cart.AddressId);

                // create address
                AddressModel addressModel = CreateAddress ();
                addressModel.UserId = await currentUser.GetCurrentUserId() ?? 0;
                int addressId = await address.Save(addressModel);
                Assert.Greater(addressId, 0);

                // update cart
                userCart.Cart.AddressId = addressId;
                await this.cart.UpdateCurrentUserCart(userCart.Cart);
                
                // pull latest
                userCart = await cart.GetCurrentUserCart();
                Assert.That(userCart.Cart.AddressId, Is.EqualTo(addressId));
            }
        }

        [Test]
        public async Task LoggedInNotEmptyAddressCartUpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                int userId = await authBL.CreateUser(new Estore.DAL.Models.UserModel()  { Email =  Guid.NewGuid().ToString() + "@test.com", Password = "qwer1234"});
                
                // add product
                var productModel1 = await product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);
                await cart.AddCurrentUserCartProduct(productModel1.Product.ProductId);

                //add address
                var userCart = await cart.GetCurrentUserCart();
                Assert.IsNull(userCart.Cart.AddressId);

                // create address
                AddressModel addressModel = CreateAddress ();
                addressModel.UserId = await currentUser.GetCurrentUserId() ?? 0;
                int addressId = await address.Save(addressModel);
                Assert.Greater(addressId, 0);

                // update cart
                userCart.Cart.AddressId = addressId;
                await this.cart.UpdateCurrentUserCart(userCart.Cart);
                
                // pull latest
                userCart = await cart.GetCurrentUserCart();
                Assert.That(userCart.Cart.AddressId, Is.EqualTo(addressId));
            }
        }

        [Test]
        public async Task LoggedInAddressTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                int userId = await authBL.CreateUser(new Estore.DAL.Models.UserModel()  { Email =  Guid.NewGuid().ToString() + "@test.com", Password = "qwer1234"});
                
                //add address
                var userCart = await cart.GetCurrentUserCart();
                Assert.IsNull(userCart.Cart.AddressId);

                // create address
                AddressModel addressModel = CreateAddress ();
                addressModel.UserId = await currentUser.GetCurrentUserId() ?? 0;
                int addressId = await address.Save(addressModel);
                Assert.Greater(addressId, 0);

                // check db
                AddressModel dbAddressModel = await address.GetAddress(addressId);
                Assert.That(dbAddressModel.City, Is.EqualTo(addressModel.City));
                Assert.That(dbAddressModel.RecieverName, Is.EqualTo(addressModel.RecieverName));
                Assert.That(dbAddressModel.Appartment, Is.EqualTo(addressModel.Appartment));
                Assert.That(dbAddressModel.Street, Is.EqualTo(addressModel.Street));
                Assert.That(dbAddressModel.House, Is.EqualTo(addressModel.House));
                Assert.That(dbAddressModel.Email, Is.EqualTo(addressModel.Email));
                Assert.That(dbAddressModel.Phone, Is.EqualTo(addressModel.Phone));
                Assert.That(dbAddressModel.ZipCode, Is.EqualTo(addressModel.ZipCode));
                Assert.That(dbAddressModel.Status, Is.EqualTo(addressModel.Status));
            }
        }

        [Test]
        public async Task LoggedInAddressUpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                int userId = await authBL.CreateUser(new Estore.DAL.Models.UserModel()  { Email =  Guid.NewGuid().ToString() + "@test.com", Password = "qwer1234"});
                
                //add address
                var userCart = await cart.GetCurrentUserCart();
                Assert.IsNull(userCart.Cart.AddressId);

                // create address
                AddressModel addressModel = CreateAddress ();
                addressModel.UserId = await currentUser.GetCurrentUserId() ?? 0;
                int addressId = await address.Save(addressModel);
                Assert.Greater(addressId, 0);

                addressModel.AddressId = addressId;
                addressModel.ZipCode = "100002";
                addressModel.Region = "Просто Квашино Updated";
                addressModel.City = "Простоквашино Updated";
                addressModel.Street = "Мотроскино Updated";
                addressModel.House = "101 Updated";
                addressModel.Email = "motroskin1@mail.ru";
                addressModel.Phone = "9051020011";

                await address.Save(addressModel);

                // check db
                AddressModel dbAddressModel = await address.GetAddress(addressId);
                Assert.That(dbAddressModel.City, Is.EqualTo(addressModel.City));
                Assert.That(dbAddressModel.RecieverName, Is.EqualTo(addressModel.RecieverName));
                Assert.That(dbAddressModel.Appartment, Is.EqualTo(addressModel.Appartment));
                Assert.That(dbAddressModel.Street, Is.EqualTo(addressModel.Street));
                Assert.That(dbAddressModel.House, Is.EqualTo(addressModel.House));
                Assert.That(dbAddressModel.Email, Is.EqualTo(addressModel.Email));
                Assert.That(dbAddressModel.Phone, Is.EqualTo(addressModel.Phone));
                Assert.That(dbAddressModel.ZipCode, Is.EqualTo(addressModel.ZipCode));
                Assert.That(dbAddressModel.Status, Is.EqualTo(addressModel.Status));
            }
        }

        public static AddressModel CreateAddress () {
            return new AddressModel() {
                    ZipCode = "100001",
                    Region = "Просто Квашино",
                    City = "Простоквашино",
                    Street = "Мотроскино", 
                    House = "101", 
                    Email = "motroskin@mail.ru",
                    Phone = "9051013344"
                };
        }
    }
}

