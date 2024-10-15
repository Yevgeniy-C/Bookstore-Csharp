namespace EstoreTests.BlTests;

using System.Transactions;
using Helpers;
using Estore.BL;

public class RegisterTests: Helpers.BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task BaseRegistrationTest()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope())
        {
            string email = Guid.NewGuid().ToString() + "@test.com";

            // validate: should not be in the DB
            authBL.ValidateEmail(email).GetAwaiter().GetResult();

            // create user
            int userId = await authBL.CreateUser(
                new Estore.DAL.Models.UserModel()
                {
                    Email = email,
                    Password = "qwer1234"
                });

            Assert.Greater(userId, 0);

            var userdalresult = await authDal.GetUser(userId);
            Assert.That(email, Is.EqualTo(userdalresult.Email));
            Assert.NotNull(userdalresult.Salt);

            var userbyemaildalresult = await authDal.GetUser(email);
            Assert.That(email, Is.EqualTo(userbyemaildalresult.Email));

            // validate: should be in the DB
            Assert.Throws<DuplicateEmailException>(delegate { authBL.ValidateEmail(email).GetAwaiter().GetResult(); });

            string encpassword = encrypt.HashPassword("qwer1234", userbyemaildalresult.Salt);
            Assert.That(encpassword, Is.EqualTo(userbyemaildalresult.Password));
        }
    }
}
