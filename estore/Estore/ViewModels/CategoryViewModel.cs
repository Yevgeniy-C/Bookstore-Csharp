using System;
using Estore.DAL.Models;

namespace Estore.ViewModels
{
    public class CategoryViewModel
    {
        public int? CurrentCategoryId { get; set; }

        public string BaseUrl { get; set; } = null!;

        public List<CategoryModel> Categories { get; set; } = null!;
    }
}

