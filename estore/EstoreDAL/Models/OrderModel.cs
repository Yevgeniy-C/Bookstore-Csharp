namespace Estore.DAL.Models
{
	public class OrderModel
	{
        public OrderModel() {

        }

        public OrderModel(CartModel model) {
            this.CartId =  model.CartId ?? 0;
            this.SessionId = model.SessionId;
            this.UserId = model.UserId;
            this.Created = DateTime.Now;
            this.Modified = DateTime.Now;
            this.AddressId = model.AddressId;
            this.BillingId = model.BillingId;
        }

        public int? OrderId { get; set; }
        
        public int CartId { get; set; }

        public Guid? SessionId { get; set; }

        public int? UserId { get; set; }

	    public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public int? AddressId { get; set; }

        public int? BillingId { get; set; }
    }
}

