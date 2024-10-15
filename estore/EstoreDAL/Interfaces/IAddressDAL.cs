using Estore.DAL.Models;

namespace Estore.DAL
{
	public interface IAddressDAL
	{
        public Task<int> Create(AddressModel model);
        public Task<AddressModel> Get(int addressid);
        public Task Update(AddressModel model);
        public Task Delete(int addressid);

    }
}