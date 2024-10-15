using Microsoft.AspNetCore.Mvc;
using Estore.BL.Auth;

namespace Estore.ViewComponents
{
	public class MainMenuViewComponent: ViewComponent 
	{
		private readonly ICurrentUser currentUser;

        public MainMenuViewComponent(ICurrentUser currentUser)
		{
			this.currentUser = currentUser;
        }

		public async Task<IViewComponentResult> InvokeAsync()
		{
			bool isLoggedIn = await currentUser.IsLoggedIn();
			return View("Index", isLoggedIn);
		}
	}
}

