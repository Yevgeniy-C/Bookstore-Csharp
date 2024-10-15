using Estore.BL.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Estore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ICurrentUser currentUser;

        public ProfileController(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }

        [Route("/profile")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/profile/logout")]
        public async Task<IActionResult> Logout()
        {
            await currentUser.Logout();
            return Redirect("/");
        }
    }
}

