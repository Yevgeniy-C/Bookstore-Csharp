using Estore.DAL.Models;
using Estore.ViewModels;

namespace Estore.ViewMapper
{
	public static class ProfileMapper
	{
        public static ProfileModel MapProfileViewModelToProfileModel(ProfileViewModel model)
        {
            return new ProfileModel()
            {
                ProfileId = model.ProfileId,
                ProfileName = model.ProfileName,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        public static ProfileViewModel MapProfileModelToProfileViewModel(ProfileModel model)
        {
            return new ProfileViewModel()
            {
                ProfileId = model.ProfileId,
                ProfileName = model.ProfileName,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        public static AddressModel MapAddressViewModelToAddressModel(AddressViewModel model) {
            return new AddressModel() {
                Region = model.Region,
                City = model.City,
                ZipCode = model.ZipCode,
                Street = model.Street,
                House = model.House,
                Appartment = model.Appartment ?? "",
                RecieverName = model.RecieverName,
                Phone = model.Phone,
                Email = model.Email,
                Status = (int)model.Status
            };
        }

        public static AddressViewModel MapAddressModelToAddressViewModel(AddressModel model) {
            return new AddressViewModel() {
                Region = model.Region,
                City = model.City,
                ZipCode = model.ZipCode,
                Street = model.Street,
                House = model.House,
                Appartment = model.Appartment,
                RecieverName = model.RecieverName,
                Phone = model.Phone,
                Email = model.Email,
                Status = (AddressViewModel.AddressStatusEnum)model.Status,
            };
        }


        public static BillingModel MapBillingViewModelToBillingModel (BillingViewModel model) {
            return new BillingModel() {
                CardNumber = model.CardNumber,
                CardType = (int)model.CardType,
                OwnerName = model.OwnerName,
                ExpYear = model.ExpYear,
                ExpMonth = model.ExpMonth,
                BillingAddressId = model.BillingAddressId,
                Status = (int)model.Status
            };
        }

        public static BillingViewModel MapBillingModelToBillingViewModel (BillingModel model) {
            return new BillingViewModel() {
                CardNumber = model.CardNumber,
                CardType = (BillingViewModel.CardTypeEnum)model.CardType,
                OwnerName = model.OwnerName,
                ExpYear = model.ExpYear,
                ExpMonth = model.ExpMonth,
                BillingAddressId = model.BillingAddressId,
                Status = (Estore.ViewModels.BillingViewModel.BillingStatusEnum)model.Status
            };
        }
    }
}

