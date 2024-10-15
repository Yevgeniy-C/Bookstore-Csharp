using System;
using Estore.DAL.Models;

namespace Estore.BL.Models
{
	public class CompleteProductDataModel
	{
		public ProductModel Product { get; set; } = null!;

		public List<ProductDetailModel> ProductDetails { get; set; } = null!;

		public List<AuthorModel>? Autors { get; set; }

        public List<CategoryModel>? Categories { get; set; }

        public List<BreadcrumpModel>? Breadcrumps { get; set; } = null;

        public ProductSerieModel? Serie { get; set; }

        public string CategoryPath(int index)
		{
			if (Categories == null)
				return "";
			return String.Join("/", Categories.Skip(index).Select(m => m.CategoryUniqueId).Reverse());
        }
    }
}

