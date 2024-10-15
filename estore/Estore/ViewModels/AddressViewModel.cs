using System.ComponentModel.DataAnnotations;

namespace Estore.ViewModels
{
	public class AddressViewModel
	{
        public enum AddressStatusEnum { Order,  Saved, Deleted }

        [Required]
        [MaxLength(50, ErrorMessage = "Некорректный регион")]
	    public string Region { get; set;} = null!;

        [Required]
        [MaxLength(50, ErrorMessage = "Некорректный город")]
	    public string City { get; set; } = null!;

        [Required]
        [MaxLength(10, ErrorMessage = "Некорректный индекс")]
	    public string ZipCode  { get; set; } = null!;

        [Required]
	    public string Street  { get; set; } = null!;
	    
        [Required]
        public string House { get; set; } = null!;

	    public string? Appartment { get; set; }

        [Required]
        public string RecieverName { get; set; } = null!;

        [Required(ErrorMessage="Телефон обязательный")]
        [DataType(DataType.PhoneNumber)]
        public string Phone  { get; set; } = null!;

        [Required (ErrorMessage="Email обязательный")]
        [EmailAddress(ErrorMessage="Email неверный")]
        public string Email  { get; set; } = null!;

        public AddressStatusEnum Status { get; set; } = AddressStatusEnum.Order;
    }
}