using EstoreTests.Helpers;
using System.Transactions;
using System.Text.Json;
using NUnit.Framework.Internal;

namespace EstoreTests.BlTests
{
    public class SessionTest : Helpers.BaseTest
    {
        [Test]
        [NonParallelizable]
        public async Task CreateSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();

                var dbSessoion = await this.dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSessoion, "Session shoule not be null");

                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task CreateAuthorizedSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                await this.dbSession.SetUserId(10);

                var dbSessoion = await this.dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSessoion, "Session shoule not be null");
                Assert.That(dbSessoion.UserId!, Is.EqualTo(10));

                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session2.DbSessionId));

                int? userid = await this.currentUser.GetCurrentUserId();
                Assert.That(userid, Is.EqualTo(10));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task AddValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                this.dbSession.AddValue("Test", "TestValue");
                await this.dbSession.UpdateSessionData();

                Assert.That(this.dbSession.GetValueDef("Test", ""), Is.EqualTo("TestValue"));

                await this.dbSession.SetUserId(10);

                var dbSessoion = await this.dbSessionDAL.Get(session.DbSessionId);
                var SessionData = JsonSerializer.Deserialize<Dictionary<string, object>>(dbSessoion?.SessionData ?? "");

                Assert.IsTrue(SessionData?.ContainsKey("Test"));
                Assert.That(SessionData?["Test"].ToString(), Is.EqualTo("TestValue"));

                this.dbSession.ResetSessionCache();
                session = await this.dbSession.GetSession();
                Assert.That(this.dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("TestValue"));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task UpdateValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                this.dbSession.AddValue("Test", "TestValue");
                Assert.That(this.dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("TestValue"));

                this.dbSession.AddValue("Test", "UpdatedValue");
                Assert.That(this.dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("UpdatedValue"));

                await this.dbSession.UpdateSessionData();

                this.dbSession.ResetSessionCache();
                session = await this.dbSession.GetSession();
                Assert.That(this.dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("UpdatedValue"));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task RemoveValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                this.dbSession.AddValue("Test", "TestValue");
                await this.dbSession.UpdateSessionData();

                this.dbSession.RemoveValue("Test");
                await this.dbSession.UpdateSessionData();

                this.dbSession.ResetSessionCache();
                session = await this.dbSession.GetSession();
                Assert.That(this.dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo(""));
            }
        }
    }
}

