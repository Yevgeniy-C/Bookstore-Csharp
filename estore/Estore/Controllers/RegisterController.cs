using Estore.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using Estore.ViewModels;
using Estore.ViewMapper;
using Estore.BL;
using Estore.Middleware;

namespace Estore.Controllers
{
    [SiteNotAuthorize()]
    public class RegisterController: Controller
	{
		private readonly IAuth authBl;

        public RegisterController(IAuth authBl)
		{
			this.authBl = authBl;
        }

		[HttpGet]
		[Route("/register")]
		public IActionResult Index()
		{
			return View("Index", new RegisterViewModel());
		}

        [HttpPost]
        [Route("/register")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Register(AuthMapper.MapRegisterViewModelToUserModel(model));
                    return Redirect("/");
                }
                catch (DuplicateEmailException)
                {
                    ModelState.TryAddModelError("Email", "Email уже существует");
                }
            }

            return View("Index", model);
        }
    }
}

