using Estore.BL.Catalog;
using Estore.BL.Profile;
using Estore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Estore.Middleware;
using Estore.BL.Auth;
using Estore.DAL.Models;
using Estore.ViewMapper;
using Estore.Service;
using Estore.BL;

namespace Estore.Controllers
{
    [SiteAuthorize()]
    public class CheckoutController : Controller
    {
        private readonly ICart cart;
        private readonly IAddress address;
        private readonly IBilling billing;
        private readonly ICurrentUser currentUser;
        private readonly IDbSession dbSession;


        public CheckoutController(ICart cart, IAddress address, ICurrentUser currentUser,
                IBilling billing, IDbSession dbSession)
        {
            this.cart = cart;
            this.address = address;
            this.currentUser = currentUser;
            this.billing = billing;
            this.dbSession = dbSession;
        }

        [HttpGet]
        [Route("/checkout/address")]
        public async Task<IActionResult> Address()
        {   
            var userCart = await this.cart.GetCurrentUserCart();
            ViewBag.Cart = userCart;


            AddressViewModel addressViewModel = new AddressViewModel();
            if (userCart.Cart.AddressId != null) {
                AddressModel addressModel = await address.GetAddress((int)userCart.Cart.AddressId);
                addressViewModel = ProfileMapper.MapAddressModelToAddressViewModel(addressModel);
            }

            return View(addressViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/checkout/address")]
        public async Task<IActionResult> AddressSave(AddressViewModel model)
        {            
            var userCart = await this.cart.GetCurrentUserCart();
            ViewBag.Cart = userCart;
            if (ModelState.IsValid) {
                AddressModel addressModel = ProfileMapper.MapAddressViewModelToAddressModel(model);
                addressModel.UserId = await currentUser.GetCurrentUserId() ?? 0;
                addressModel.AddressId = userCart.Cart.AddressId;

                int addressId = await address.Save(addressModel);

                userCart.Cart.AddressId = addressId;
                await this.cart.UpdateCurrentUserCart(userCart.Cart);
                return this.Redirect("/checkout/billing");
            }
            
            return View("Address", model);
        }


        [HttpGet]
        [Route("/checkout/billing")]
        public async Task<IActionResult> Billing()
        {   
            var userCart = await this.cart.GetCurrentUserCart();
            ViewBag.Cart = userCart;


            BillingViewModel billingViewModel = new BillingViewModel();
            if (userCart.Cart.BillingId != null) {
                BillingModel billingModel = await billing.GetBilling((int)userCart.Cart.BillingId);
                billingViewModel = ProfileMapper.MapBillingModelToBillingViewModel(billingModel);
            }

            return View(billingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/checkout/billing")]
        public async Task<IActionResult> BillingSave(BillingViewModel model)
        {            
            var userCart = await this.cart.GetCurrentUserCart();
            ViewBag.Cart = userCart;
            if (ModelState.IsValid) {
                BillingModel billingModel = ProfileMapper.MapBillingViewModelToBillingModel(model);
                billingModel.UserId = await currentUser.GetCurrentUserId() ?? 0;
                billingModel.BillingId = userCart.Cart.BillingId;

                int BillingId = await billing.Save(billingModel);

                userCart.Cart.BillingId = BillingId;
                await this.cart.UpdateCurrentUserCart(userCart.Cart);

                dbSession.AddValue("CVV", (model.CVV ?? 0).ToString());
                await dbSession.UpdateSessionData();

                return this.Redirect("/checkout/review");
            }
            
            return View("Billing", model);
        }

        [HttpGet]
        [Route("/checkout/review")]
        public async Task<IActionResult> Review()
        {   
            var userCart = await this.cart.GetCurrentUserCart();
            ViewBag.Cart = userCart;

            BillingService billingService = new BillingService();
            var addressData = await billingService.LoadAddress(address, userCart.Cart.AddressId);
            if (!addressData.Item1) {
                return Redirect("/checkout/address");
            }

            var billingData = await billingService.LoadBilling(billing, userCart.Cart.BillingId);
            if (!billingData.Item1) {
                return Redirect("/checkout/billing?1");
            }

            ViewBag.Address = addressData.Item2;
            ViewBag.Billing = billingData.Item2;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/checkout/review")]
        public async Task<IActionResult> ReviewSave()
        {   
            var userCart = await this.cart.GetCurrentUserCart();
            ViewBag.Cart = userCart;

            BillingService billingService = new BillingService();
            var addressData = await billingService.LoadAddress(address, userCart.Cart.AddressId);
            if (!addressData.Item1) {
                return Redirect("/checkout/address");
            }

            var billingData = await billingService.LoadBilling(billing, userCart.Cart.BillingId);
            if (!billingData.Item1) {
                return Redirect("/checkout/billing");
            }

            ViewBag.Address = addressData.Item2;
            ViewBag.Billing = billingData.Item2;

            try {
                await cart.PlaceOrder();
                return Redirect("/");
            }
            catch (OrderException ex) {
                ViewBag.Error = ex.Message;
            }

            return View("Review");
        }
    }
}