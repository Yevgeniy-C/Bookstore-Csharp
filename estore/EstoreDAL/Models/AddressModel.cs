namespace Estore.DAL.Models
{
	public class AddressModel
	{
        public int? AddressId { get; set; }

        public int UserId { get; set; }

	    public string Region { get; set;} = null!;

	    public string City { get; set; } = null!;

	    public string ZipCode  { get; set; } = null!;

	    public string Street  { get; set; } = null!;
	    
        public string House { get; set; } = null!;

	    public string Appartment { get; set; } = "";

        public string RecieverName { get; set; } = null!;

        public string Phone  { get; set; } = null!;

        public string Email  { get; set; } = null!;

        public int Status { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}