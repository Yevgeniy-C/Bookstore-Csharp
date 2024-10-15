using Estore.BL.Catalog;
using Estore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart cart;

        public CartController(ICart cart)
        {
            this.cart = cart;
        }

        [HttpGet]
        [Route("/cart")]
        public async Task<IActionResult> Index()
        {
            var model = await cart.GetCurrentUserCart();
            return View(model);
        }

        [HttpPost]
        [Route("/cart/add")]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Add(int productId)
        {
            await cart.AddCurrentUserCartProduct(productId);
            return Redirect("/cart");
        }

        [HttpPost]
        [Route("/cart/add/ajax")]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> AddAjax(int productId)
        {
            await cart.AddCurrentUserCartProduct(productId);
            var model = await cart.GetCurrentUserCart();
            return Json(new {
                Total = model.Count
            });
        }


        [HttpPost]  
        [Route("/cart/update")]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> update(CartUpdateViewModel model)
        {
            await cart.UpdateCurrentUserCartProduct(model.Productid, model.ProductCount);
            return Redirect("/cart");
        }
    }
}

