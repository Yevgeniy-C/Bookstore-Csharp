
using Estore.BL.Profile;
using Estore.DAL.Models;
using Estore.ViewMapper;
using Estore.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace Estore.Service
{
	public class BillingService
	{
        public async Task<Tuple<bool, AddressViewModel?>> LoadAddress(IAddress address, int? addressId) {
            if (addressId == null) {
                return new Tuple<bool, AddressViewModel?>(false, null);
            }
            AddressModel addressModel = await address.GetAddress((int)addressId);
            AddressViewModel addressViewModel = ProfileMapper.MapAddressModelToAddressViewModel(addressModel);

            ValidationContext vc = new ValidationContext(addressViewModel);
            ICollection<ValidationResult> result = new List<ValidationResult>();

            bool isModelValid = Validator.TryValidateObject(addressViewModel, vc, result, true);
            return new Tuple<bool, AddressViewModel?>(isModelValid, addressViewModel);
        }

        public async Task<Tuple<bool, BillingViewModel?>> LoadBilling(IBilling billing, int? billingid) {
            if (billingid == null) {
                return new Tuple<bool, BillingViewModel?>(false, null);
            }
            BillingModel billingModel = await billing.GetBilling((int)billingid);
            BillingViewModel billingViewModel = ProfileMapper.MapBillingModelToBillingViewModel(billingModel);
            
            ValidationContext vc = new ValidationContext(billingViewModel);
            ICollection<ValidationResult> result = new List<ValidationResult>();
            bool isModelValid = Validator.TryValidateObject(billingViewModel, vc, result, true);
            return new Tuple<bool, BillingViewModel?>(isModelValid || result.Where(m => !m.MemberNames.Contains("CVV")).Count() == 0, billingViewModel);
        }
    }
}