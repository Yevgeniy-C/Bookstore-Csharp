using Estore.BL.Catalog;
using Estore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Estore.Middleware;

namespace Estore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SiteAuthorize(RequireAdmin: true)]
    public class CatalogController : Controller
    {
        private readonly IProduct product;

        public CatalogController(IProduct product)
        {
            this.product = product;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/admin/catalog/categories/{uniqueid?}/{uniqueid1?}/{uniqueid2?}/{uniqueid3?}")]
        public async Task<IActionResult> Categories(string? uniqueid, string? uniqueid1, string? uniqueid2, string? uniqueid3)
        {
            string?[] uniqueids = { uniqueid, uniqueid1, uniqueid2, uniqueid3 };
            var notNullIds = (IEnumerable<string>)uniqueids.Where(m => m != null).ToList();

            int? categoryId = await product.GetCategoryId(notNullIds);
            var childCategories = await product.GetChildCategories(categoryId);

            CategoryViewModel model = new CategoryViewModel()
            {
                BaseUrl = "/admin/catalog/categories",
                CurrentCategoryId = categoryId,
                Categories = childCategories.ToList()
            };
            if (notNullIds.Count() > 0)
                model.BaseUrl = model.BaseUrl + "/" + String.Join("/", notNullIds);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/catalog/categories/{uniqueid?}/{uniqueid1?}/{uniqueid2?}/{uniqueid3?}")]
        public async Task<IActionResult> CategorySave(EditCategoryViewModel model, string? uniqueid, string? uniqueid1, string? uniqueid2, string? uniqueid3)
        {
            string?[] uniqueids = { uniqueid, uniqueid1, uniqueid2, uniqueid3 };
            var notNullIds = (IEnumerable<string>)uniqueids.Where(m => m != null).ToList();

            if (ModelState.IsValid)
                await product.AddCategory(model.Categoryid, model.Name);

            string url = "/admin/catalog/categories";
            if (notNullIds.Count() > 0)
                url = url + "/" + String.Join("/", notNullIds);
            return Redirect(url);
        }
    }
}

