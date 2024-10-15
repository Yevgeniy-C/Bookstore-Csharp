using Estore.DAL.Models;

namespace Estore.BL.Profile {
    public interface IBilling {
        public Task<int> Save(BillingModel model);
        public Task<BillingModel> GetBilling(int id);
    }
}