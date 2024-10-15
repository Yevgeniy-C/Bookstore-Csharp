using Estore.DAL.Models;
using Estore.DAL;
using Estore.BL.General;
using System.Text.Json;
using Estore.BL.Auth;

namespace Estore.BL.Profile
{
    public class Billing : IBilling
    {
        private readonly IBillingDAL billingDAL;
        private readonly IEncrypt encrypt;

        public Billing(IBillingDAL billingDAL, IEncrypt encrypt)
        {
            this.billingDAL = billingDAL;
            this.encrypt = encrypt;
        }

        public async Task<BillingModel> GetBilling(int id) {
            var model = await billingDAL.Get(id);

            model.CardNumber = encrypt.DecryptString(model.CardNumber);
            model.OwnerName = encrypt.DecryptString(model.OwnerName);
            model.ExpMonth = encrypt.DecryptString(model.ExpMonth);
            model.ExpYear = encrypt.DecryptString(model.ExpYear);

            return model;
        }

        public async Task<int> Save(BillingModel model) {
            if (model.UserId == 0) {
                throw new Exception("Какого хера BillingModel.UserId получил 0");
            }
            model.CardNumber = encrypt.EncryptString(model.CardNumber ?? "");
            model.OwnerName = encrypt.EncryptString(model.OwnerName ?? "");
            model.ExpMonth = encrypt.EncryptString(model.ExpMonth ?? "");

            model.ExpYear = encrypt.EncryptString(model.ExpYear ?? "");


            model.Modified = DateTime.Now;
            if (model.BillingId == null) {
                model.Created = DateTime.Now;
                return await billingDAL.Create(model);
            }
            else {
                await billingDAL.Update(model);
                return model.BillingId ?? 0;
            }
        }
    }
}
