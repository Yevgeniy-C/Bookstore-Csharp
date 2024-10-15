using System;
using Estore.BL.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Estore.ViewComponents
{
	public class CartWidgetViewComponent : ViewComponent
    {
        private readonly IDbSession session;

        public CartWidgetViewComponent(IDbSession session)
        {
            this.session = session;
        }

        public IViewComponentResult Invoke()
        {
            return View("Index", session.GetValueDef(Estore.BL.General.Constants.CART_PARAM_NAME, 0).ToString());
        }
    }
}

