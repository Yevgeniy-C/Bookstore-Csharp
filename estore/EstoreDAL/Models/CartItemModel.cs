using System;

namespace Estore.DAL.Models
{
	public class CartItemModel
	{
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public int ProductCount { get; set; }
    }
}

