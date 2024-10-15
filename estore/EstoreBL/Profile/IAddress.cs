using Estore.DAL.Models;

namespace Estore.BL.Profile {
    public interface IAddress {
        public Task<int> Save(AddressModel model);
        public Task<AddressModel> GetAddress(int id);
    }
}