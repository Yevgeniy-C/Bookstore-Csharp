using EstoreTests.Helpers;
using System.Transactions;
using Estore.BL;

namespace EstoreTests.BlTests
{
	public class AuthTest : Helpers.BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AuthRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";

                // create user
                int userId = await authBL.CreateUser(
                    new Estore.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate("sefrew", "111", false).GetAwaiter().GetResult(); });

                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate(email, "111", false).GetAwaiter().GetResult(); });

                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate("werewr", "qwer1234", false).GetAwaiter().GetResult(); });

                await authBL.Authenticate(email, "qwer1234", false);

                string? authCookie = this.webCookie.Get(Estore.BL.Auth.AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = this.webCookie.Get(Estore.BL.Auth.AuthConstants.RememberMeCookieName);
                Assert.Null(rememberMeCookie);
            }
        }

        [Test]
        public async Task RememberMeTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";

                // create user
                int userId = await authBL.CreateUser(
                    new Estore.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                await authBL.Authenticate(email, "qwer1234", true);

                string? authCookie = this.webCookie.Get(Estore.BL.Auth.AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = this.webCookie.Get(Estore.BL.Auth.AuthConstants.RememberMeCookieName);
                Assert.NotNull(rememberMeCookie);
            }
        }
    }
}

