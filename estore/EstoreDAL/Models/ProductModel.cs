namespace Estore.DAL.Models
{
	public class ProductModel
	{
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Price { get; set; }
        public string Description { get; set; } = null!;
        public string? ProductImage { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string UniqueId { get; set; } = null!;
        public int ProductSerieId { get; set; }
    }
}

