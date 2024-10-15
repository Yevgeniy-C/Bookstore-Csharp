using Microsoft.AspNetCore.Mvc;
using Estore.BL.Auth;
using Estore.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Estore.Controllers
{
//    [SiteNotAuthorize()]
    public class LoginController : Controller
    {
        private readonly IAuth authBl;

        public LoginController(IAuth authBl)
        {
            this.authBl = authBl;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (Estore.BL.AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Имя или Email неверные");
                }
            }

            return View("Index", model);
        }
    }
}

