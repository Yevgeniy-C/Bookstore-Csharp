using Estore.BL.Auth;
using Estore.BL.Catalog;
using Estore.BL.Models;
using Estore.DAL.Models;
using Estore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Estore.ViewHelpers;

namespace Estore.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProduct product;

        public ProductCategoryController(ICurrentUser currentUser, IProduct product)
        {
            this.product = product;
        }

        [HttpGet]
        [Route("/product-category/{uniqueid}/{uniqueid1?}/{uniqueid2?}/{uniqueid3?}")]
        public async Task<IActionResult> Index(string uniqueid, string? uniqueid1, string? uniqueid2, string? uniqueid3, int page = 1)
        {
            string?[] uniqueids = { uniqueid, uniqueid1, uniqueid2, uniqueid3};
            int? categoryId = await product.GetCategoryId((IEnumerable<string>)uniqueids.Where(m => m != null));

            if (categoryId != null) {
                (int totalProducts, IEnumerable<ProductCardModel> products) = await product.GetByCategory((int)categoryId, ViewConstants.PAGE_SIZE, page);

                var childCategories = await product.GetChildCategories((int)categoryId);
                var categoryTree = await product.GetCategoryTree((int)categoryId);
                var categoryTreeList = categoryTree.Reverse().ToList();

                CatalogViewModel model = new CatalogViewModel() {
                    Products = products.ToList(),
                    ChildCategories = childCategories.ToList(),
                    Breadcrumps = categoryTreeList.Select((m, index) => new BreadcrumpModel()
                    {
                        Link = "/product-category/" + String.Join("/",categoryTreeList.Take(index + 1).Select(m => m.CategoryUniqueId)),
                        Name = m.CategoryName
                    }).ToList(),
                    Pagination = new PaginationViewModel()
                    {
                        Page = page,
                        PageSize = ViewConstants.PAGE_SIZE,
                        TotalCount = totalProducts,
                        TotalPages = (int)Math.Ceiling((double)totalProducts / ViewConstants.PAGE_SIZE),
                        BaseUrl = "/product-category/" + String.Join("/", (IEnumerable<string>)uniqueids.Where(m => m != null))
                    }
                };

                ViewData["Title"] = "Книги из раздела: " + categoryTreeList.First().CategoryName;
                return View(model);
            }

            return NotFound();
        }


        [HttpGet]
        [Route("/product-serie/{serieName}")]
        public async Task<IActionResult> Serie(string serieName, int page = 1)
        {
            (int totalProducts, IEnumerable<ProductCardModel> products) = await product.GetBySerie(serieName, ViewConstants.PAGE_SIZE, page);

            CatalogViewModel model = new CatalogViewModel()
            {
                Products = products.ToList(),
                Pagination = new PaginationViewModel()
                {
                    Page = page,
                    PageSize = ViewConstants.PAGE_SIZE,
                    TotalCount = totalProducts,
                    TotalPages = (int)Math.Ceiling((double)totalProducts / ViewConstants.PAGE_SIZE),
                    BaseUrl = "/product-serie/" + serieName
                }
            };
            ViewData["Title"] = "Книги Серии: " + serieName;
            return View("Index", model);
        }
    }
}

