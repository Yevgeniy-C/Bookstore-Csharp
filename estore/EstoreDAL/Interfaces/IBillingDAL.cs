using Estore.DAL.Models;

namespace Estore.DAL
{
	public interface IBillingDAL
	{
        public Task<int> Create(BillingModel model);
        public Task<BillingModel> Get(int billingid);
        public Task Update(BillingModel model);
        public Task Delete(int billingid);

    }
}