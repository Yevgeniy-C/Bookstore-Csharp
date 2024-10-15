namespace Estore.DAL.Models
{
	public class OrderItemModel
	{
        public OrderItemModel() {

        }

        public OrderItemModel(CartItemModel model) {
            this.ProductId = model.ProductId;
            this.ProductCount = model.ProductCount;
            this.Created = DateTime.Now;
            this.Modified = DateTime.Now;
        }

        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int ProductCount { get; set; }

        public int Price { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

    }
}

