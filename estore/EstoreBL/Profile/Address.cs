using Estore.DAL.Models;
using Estore.DAL;
using Estore.BL.General;
using System.Text.Json;

namespace Estore.BL.Profile
{
    public class Address : IAddress
    {
        private readonly IAddressDAL addressDAL;

        public Address(IAddressDAL addressDAL)
        {
            this.addressDAL = addressDAL;
        }

        public async Task<AddressModel> GetAddress(int id) {
            return await addressDAL.Get(id);
        }

        public async Task<int> Save(AddressModel model) {
            if (model.UserId == 0) {
                throw new Exception("Какого хера addressModel.UserId получил 0");
            }

            model.Modified = DateTime.Now;
            if (model.AddressId == null) {
                model.Created = DateTime.Now;
                return await addressDAL.Create(model);
            }
            else {
                await addressDAL.Update(model);
                return model.AddressId ?? 0;
            }
        }
    }
}
