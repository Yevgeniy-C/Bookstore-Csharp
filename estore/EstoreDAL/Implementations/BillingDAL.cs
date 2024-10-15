using Estore.DAL.Models;

namespace Estore.DAL
{
    public class BillingDAL : IBillingDAL
    {
        private readonly IDbHelper dbHelper;
        public BillingDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<int> Create(BillingModel model)
        {
            string sql = @"insert into Billing (UserId, CardType, CardNumber, OwnerName, ExpYear, ""Status"", ExpMonth, BillingAddressId, Created, Modified)
                    values (@UserId, @CardType, @CardNumber, @OwnerName, @ExpYear, @Status, @ExpMonth, @BillingAddressId, @Created, @Modified)
                    returning BillingId";

            return await dbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<BillingModel> Get(int billingid)
        {
            string sql = @"select BillingId, UserId, CardType, CardNumber, OwnerName, ExpYear, ""Status"", ExpMonth, BillingAddressId, Created, Modified from Billing where BillingId = @billingid";
            var bilings = await dbHelper.QueryAsync<BillingModel>(sql, new { billingid = billingid });
            return bilings.FirstOrDefault() ?? new BillingModel();
        }

        public async Task Update(BillingModel model)
        {
            string sql = @"update Billing
                    set UserId = @UserId,
                        CardType = @CardType,
                        CardNumber = @CardNumber,
                        OwnerName = @OwnerName,
                        ExpYear = @ExpYear,
                        ""Status"" = @Status,
                        ExpMonth = @ExpMonth,
                        BillingAddressId = @BillingAddressId,
                        Modified = @Modified
                    where BillingId = @BillingId";

            await dbHelper.ExecuteAsync(sql, model);
        }

        public async Task Delete(int billingid)
        {
            string sql = @"delete from billingid where BillingId = @billingid";

            await dbHelper.ExecuteAsync(sql, new { billingid = billingid });
        }
    }
}

