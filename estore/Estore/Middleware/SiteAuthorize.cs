using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Estore.BL.Auth;

namespace Estore.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SiteAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        bool requireAdmin = false;
        public SiteAuthorizeAttribute(bool RequireAdmin = false)
        {
            this.requireAdmin = RequireAdmin;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
            if (currentUser == null)
            {
                throw new Exception("No user middleware");
            }

            bool isLoggedIn = await currentUser.IsLoggedIn();
            if (isLoggedIn == false)
                context.Result = new RedirectResult("/Login");

            if (requireAdmin)
            {
                bool isadmin = currentUser.IsAdmin();
                if (isadmin == false)
                    context.Result = new RedirectResult("/Login");
            }
        }
    }
}