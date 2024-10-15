using System;
using Estore.DAL.Models;

namespace Estore.BL.Models
{
	public class UserCartModel
	{
		public int Total { get { return Items.Sum(m => m.Price * m.ProductCount); } }

        public int Count { get { return Items.Sum(m => m.ProductCount); } }

        public CartModel Cart { get; set; } = null!;

        public List<CartItemDetailsModel> Items { get; set; } = new List<CartItemDetailsModel>();

    }
}

