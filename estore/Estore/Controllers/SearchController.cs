using Estore.BL.Catalog;
using Estore.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Estore.ViewHelpers;
using Estore.ViewModels;
using Estore.BL.Models;

namespace Estore.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProduct product;

        public SearchController(IProduct product)
        {
            this.product = product;
        }

        [HttpGet]
        [Route("/search/index")]
        public async Task<IActionResult> Index(string searchFor, int page = 1)
        {
            (int totalProducts, IEnumerable<ProductCardModel> products) 
                = await product.Search(searchFor ?? "", ViewConstants.PAGE_SIZE, page);


                CatalogViewModel model = new CatalogViewModel() {
                    Products = products.ToList(),
                    ChildCategories = null,
                    Breadcrumps =  new List<BreadcrumpModel>(),
                    Pagination = new PaginationViewModel()
                    {
                        Page = page,
                        PageSize = ViewConstants.PAGE_SIZE,
                        TotalCount = totalProducts,
                        TotalPages = (int)Math.Ceiling((double)totalProducts / ViewConstants.PAGE_SIZE),
                        BaseUrl = "",
                        AdditionalParameters = "searchFor=" + searchFor
                    }
                };
                
            ViewData["Title"] = "Результаты поиска: " + searchFor;
            return View(model);
        }

        [HttpGet]
        [Route("/search/quicksearch")]
        public async Task<IActionResult> QuickSearch(string searchFor)
        {
            if (String.IsNullOrEmpty(searchFor)) {
                return PartialView("Quick", new List<ProductCardModel>());
            }
            (int totalProducts, IEnumerable<ProductCardModel> products) = await product.Search(searchFor, 5, 1);

            return PartialView("Quick", products);
        }
    }
}

