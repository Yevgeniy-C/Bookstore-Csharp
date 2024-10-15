namespace Estore.DAL.Models
{
	public class BillingModel
	{
        public int? BillingId { get; set; }
        public int UserId { get; set; }
	    public int CardType { get; set;}
	    public string? CardNumber { get; set; }
	    public string? OwnerName  { get; set; }
	    public string? ExpYear  { get; set; } 
        public string? ExpMonth { get; set; }
	    public int BillingAddressId { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}