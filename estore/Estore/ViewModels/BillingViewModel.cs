using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Estore.BL.General;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Estore.ViewModels
{
	public class BillingViewModel: IValidatableObject
	{
        public enum CardTypeEnum { MasterCard, Visa, Amex }
        public enum BillingStatusEnum { Order,  Saved, Deleted }

        [Required]
	    public CardTypeEnum CardType { get; set;}

        [Required]
        [MaxLength(16, ErrorMessage = "Некорректная карта")]
	    public string? CardNumber { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Некорректный владелец")]
	    public string? OwnerName  { get; set; }

        [Required]
	    public string? ExpYear  { get; set; }

        [Required]
        public string? ExpMonth { get; set; }

	    public int BillingAddressId { get; set; }

        [Required]
        public int? CVV { get; set; }

        public BillingStatusEnum Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ExpYear != null && 
                ExpMonth != null && 
                new DateTime((Helpers.StringToIntDef(ExpYear, 0) ?? 0) + 2000, Helpers.StringToIntDef(ExpMonth, 0) ?? 0, 1).AddMonths(1) < DateTime.Now) {
                yield return new ValidationResult("Срок действия карты истёк", new string[] { "ExpYear" });
            }

            if (!String.IsNullOrEmpty(CardNumber) && !ValidateCreditCard(CardNumber, this.CardType)) {
                yield return new ValidationResult("Неверный номер карты", new string[] { "CardNumber" });
            }
        }

        public bool ValidateCreditCard(string cardnumber, CardTypeEnum cardtype)
		{
			bool result = true;

			cardnumber = cardnumber.Replace(" ","").Replace("-","");

			result = Regex.IsMatch(cardnumber, @"^\d*$");
			if (!result) {
                return false;
            }

            switch (cardtype)
            {
                case CardTypeEnum.MasterCard:
                    // check length
                    result = (cardnumber.Length == 16);
                    // ensure prefix starts with 51-55
                    if (result)
                        result = Regex.IsMatch(cardnumber, "^5[1-5]");
                    if (result)
                        result = checkluhn(cardnumber);
                    break;
                case CardTypeEnum.Visa:
                    // check length
                    result = (cardnumber.Length == 16 || cardnumber.Length == 13);
                    // ensure prefix starts with 4
                    if (result)
                        result = Regex.IsMatch(cardnumber, "^4");
                    if (result)
                        result = checkluhn(cardnumber);
                    break;
                case CardTypeEnum.Amex:
                    // check length
                    result = (cardnumber.Length == 15);
                    if (result)
                        result = cardnumber.StartsWith("34") || cardnumber.StartsWith("37");
                    if (result)
                        result = checkluhn(cardnumber);
                    break;
                default: throw new Exception("Unknown card type '" + cardtype + "'");
            }
			return result;
		}

        private bool checkluhn(string number)
		{
			int total = 0;
			bool odd = true;
			for (int i = number.Length-1; i>=0; i--)
			{
				short val = short.Parse(number[i].ToString());
				if (!odd)
					if (val > 4)
						total += 1 + val*2-10;
					else
						total += val * 2;
				else
					total += val;
				odd = !odd;
			}
			return (total % 10 == 0);
		}
    }
}