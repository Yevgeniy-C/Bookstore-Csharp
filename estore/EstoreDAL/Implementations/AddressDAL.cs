using Estore.DAL.Models;

namespace Estore.DAL
{
    public class AddressDAL : IAddressDAL
    {
        private readonly IDbHelper dbHelper;
        public AddressDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<int> Create(AddressModel model)
        {
            string sql = @"insert into ""Address"" (UserId, Region, City, ZipCode, Street, ""Status"", House, Appartment, RecieverName, Phone, Email, Created, Modified)
                    values (@UserId, @Region, @City, @ZipCode, @Street, @Status, @House, @Appartment, @RecieverName, @Phone, @Email, @Created, @Modified)
                    returning AddressId";

            return await dbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<AddressModel> Get(int addressid)
        {
            string sql = @"select AddressId, UserId, Region, City, ZipCode, Street, ""Status"", House, Appartment, RecieverName, Phone, Email, Created, Modified from ""Address"" where AddressId = @addressid";
            var addresses = await dbHelper.QueryAsync<AddressModel>(sql, new { addressid = addressid });
            return addresses.FirstOrDefault() ?? new AddressModel();
        }

        public async Task Update(AddressModel model)
        {
            string sql = @"update ""Address""
                    set UserId = @UserId,
                        Region = @Region,
                        City = @City,
                        ZipCode = @ZipCode,
                        Street = @Street,
                        ""Status"" = @Status,
                        House = @House,
                        Appartment = @Appartment,
                        RecieverName = @RecieverName,
                        Phone = @Phone,
                        Email = @Email,
                        Modified = @Modified
                    where AddressId = @AddressId";

            await dbHelper.ExecuteAsync(sql, model);
        }

        public async Task Delete(int addressid)
        {
            string sql = @"delete from ""Address"" where AddressId = @addressid";

            await dbHelper.ExecuteAsync(sql, new { addressid = addressid });
        }
    }
}

