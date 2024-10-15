using Estore.BL.Auth;
using Estore.DAL;
using Microsoft.AspNetCore.Http;
using Estore.BL.General;
using Estore.BL.Catalog;
using Estore.BL.Profile;
using Microsoft.AspNetCore.DataProtection;

namespace EstoreTests.Helpers
{
	public class BaseTest
	{
        protected IAuthDAL authDal;
        protected IEncrypt encrypt = new Estore.Deps.Encrypt(DataProtectionProvider.Create("Test App"));
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
		protected IAuth authBL;
        protected IDbSessionDAL dbSessionDAL;
        protected IDbSession dbSession;
        protected IWebCookie webCookie;
        protected IUserTokenDAL userTokenDAL;
        protected ICartDAL cartDAL;
        protected ICart cart;
        protected IProductSearchDAL productSearchDAL;
        protected IProductDAL productDAL;
        protected IProduct product;
        protected IMetricDAL metricDAL = new MetricDAL();
        protected IDbHelper dbHelper;
        protected IAddress address;
        protected IBilling billing;
        protected IOrderDAL orderDAL;

        protected CurrentUser currentUser;

        public BaseTest()
		{
            DbHelper.ConnString = "User ID=postgres;Password=password;Host=localhost;Port=5433;Database=estore";
            dbHelper = new DbHelper(metricDAL);
            authDal = new AuthDAL(dbHelper);
            dbSessionDAL = new DbSessionDAL(dbHelper);
            userTokenDAL = new UserTokenDAL(dbHelper);
            cartDAL = new CartDAL(dbHelper);
            productSearchDAL = new ProductSearchDAL(dbHelper);
            productDAL = new ProductDAL(dbHelper);
            orderDAL = new OrderDAL(dbHelper);

            webCookie = new TestCookie();
            dbSession = new DbSession(dbSessionDAL, webCookie);
            currentUser = new CurrentUser(dbSession, webCookie, userTokenDAL);
            cart = new Cart(cartDAL, currentUser, dbSession, orderDAL);
            authBL = new Auth(authDal, encrypt, webCookie, dbSession, userTokenDAL, cart);
            product = new Product(productDAL, productSearchDAL);

            billing = new Billing(new BillingDAL(dbHelper), this.encrypt);
            address = new Address(new AddressDAL(dbHelper));
        }
    }
}

