using System;
using Estore.BL.Models;
using Estore.DAL.Models;

namespace Estore.ViewModels
{
	public class CatalogViewModel
	{
        public PaginationViewModel Pagination { get; set; } = null!;

        public List<ProductCardModel> Products { get; set; } = null!;

        public List<CategoryModel>? ChildCategories { get; set; }

        public List<BreadcrumpModel>? Breadcrumps { get; set; } = null;
    }
}

