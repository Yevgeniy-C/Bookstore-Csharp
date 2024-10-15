using Estore.BL.Models;
using Estore.BL.Catalog;
using Microsoft.AspNetCore.Mvc;
using Estore.BL.Auth;

namespace Estore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct product;
        private readonly ICurrentUser currentUser;

        public ProductController(IProduct product, ICurrentUser currentUser)
        {
            this.product = product;
            this.currentUser = currentUser;
        }

        [HttpGet]
        [Route("/product/{uniqueid}")]
        public async Task<IActionResult> Index(string uniqueid)
        {
            ViewBag.CurrentUserId = await currentUser.GetCurrentUserId();
            CompleteProductDataModel model = await this.product.GetProduct(uniqueid);
            if (model.Categories != null)
            {
                model.Breadcrumps = model?.Categories.Select((m, i) => new BreadcrumpModel()
                {
                    Name = m.CategoryName,
                    Link = "/product-category/" + model.CategoryPath(i)
                }).Reverse().ToList();
            }

            return View(model);
        }
    }
}

