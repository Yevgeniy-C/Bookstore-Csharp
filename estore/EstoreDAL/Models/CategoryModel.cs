using System;
namespace Estore.DAL.Models
{
	public class CategoryModel
	{
        public int? CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string CategoryUniqueId { get; set; } = null!;
    }
}

