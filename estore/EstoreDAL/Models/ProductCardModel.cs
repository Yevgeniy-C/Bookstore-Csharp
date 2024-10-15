using System;
namespace Estore.DAL.Models
{
	public class ProductCardModel
	{
		public string ProductImage { get; set; } = null!;

		public string ProductName { get; set; } = null!;

		public int Price { get; set; }

		public string UniqueId { get; set; } = null!;
    }
}

