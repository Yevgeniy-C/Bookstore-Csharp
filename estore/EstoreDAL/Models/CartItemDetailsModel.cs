using System;
namespace Estore.DAL.Models
{
	public class CartItemDetailsModel: CartItemModel
	{
        public int Price { get; set; }

        public string ProductName { get; set; } = null!;

        public string ProductImage { get; set; } = null!;

        public string ProductUniqueId { get; set; } = null!;
    }
}

