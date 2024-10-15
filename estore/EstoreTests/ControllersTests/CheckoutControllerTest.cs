using Estore.Controllers;
using Estore.DAL.Models;
using Estore.ViewModels;
using EstoreTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace EstoreTests.ControllersTests
{
    public class CheckoutContollerTest : BaseTest
    {
        const string TEST_PRODUCT_UNIQUE_ID1 = "c-glazami-hakera";

        [Test]
        public async Task ChechoutAddressControllerTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                await authBL.CreateUser(new Estore.DAL.Models.UserModel()  { Email =  Guid.NewGuid().ToString() + "@test.com", Password = "qwer1234"});
                CheckoutController controller = new CheckoutController(this.cart, this.address, this.currentUser, this.billing, this.dbSession);

                var addressModel = new AddressViewModel() {
                    ZipCode = "100001",
                    Region = "Просто Квашино",
                    City = "Простоквашино",
                    Street = "Мотроскино", 
                    House = "101", 
                    Email = "motroskin@mail.ru",
                    Phone = "9051013344",
                    Appartment = ""
                };
                IActionResult result = await controller.AddressSave(addressModel);

                var userCart = await cart.GetCurrentUserCart();

                AddressModel dbAddressModel = await address.GetAddress(userCart.Cart.AddressId ?? 0);
                Assert.That(dbAddressModel.City, Is.EqualTo(addressModel.City));
                Assert.That(dbAddressModel.RecieverName, Is.EqualTo(addressModel.RecieverName));
                Assert.That(dbAddressModel.Appartment, Is.EqualTo(addressModel.Appartment));
                Assert.That(dbAddressModel.Street, Is.EqualTo(addressModel.Street));
                Assert.That(dbAddressModel.House, Is.EqualTo(addressModel.House));
                Assert.That(dbAddressModel.Email, Is.EqualTo(addressModel.Email));
                Assert.That(dbAddressModel.Phone, Is.EqualTo(addressModel.Phone));
                Assert.That(dbAddressModel.ZipCode, Is.EqualTo(addressModel.ZipCode));
                Assert.That(dbAddressModel.Status, Is.EqualTo((int)addressModel.Status));
            }            
        }

        [Test]
        public async Task ChechoutBillingControllerTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                await authBL.CreateUser(new Estore.DAL.Models.UserModel()  { Email =  Guid.NewGuid().ToString() + "@test.com", Password = "qwer1234"});
                CheckoutController controller = new CheckoutController(this.cart, this.address, this.currentUser, this.billing, this.dbSession);

                var model = new BillingViewModel() {
                    CardNumber = "4111111111111111",
                    CardType = 0,
                    ExpMonth = "10",
                    ExpYear = "70",
                    OwnerName = "Tester"
                };
                IActionResult result = await controller.BillingSave(model);

                var userCart = await cart.GetCurrentUserCart();

                BillingModel dbModel = await billing.GetBilling(userCart.Cart.BillingId ?? 0);
                Assert.That(dbModel.CardNumber, Is.EqualTo(model.CardNumber));
                Assert.That(dbModel.CardType, Is.EqualTo((int)model.CardType));
                Assert.That(dbModel.OwnerName, Is.EqualTo(model.OwnerName));
                Assert.That(dbModel.ExpYear, Is.EqualTo(model.ExpYear));
                Assert.That(dbModel.ExpMonth, Is.EqualTo(model.ExpMonth));
            } 
        }
    }
}

