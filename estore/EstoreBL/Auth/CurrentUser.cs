using System;
using Estore.BL.General;
using Estore.DAL;
using Estore.DAL.Models;

namespace Estore.BL.Auth
{
	public class CurrentUser: ICurrentUser
	{
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;

        public CurrentUser(
            IDbSession dbSession,
            IWebCookie webCookie,
            IUserTokenDAL userTokenDAL
            )
		{
			this.dbSession = dbSession;
			this.webCookie = webCookie;
			this.userTokenDAL = userTokenDAL;
        }

		private Guid? GetCurrentUserToken() {
			string? tokenCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
			if (tokenCookie == null)
				return null;
			return Helpers.StringToGuidDef(tokenCookie ?? "");
		}

		public async Task<int?> GetUserIdByToken()
		{
			Guid? tokenGuid = GetCurrentUserToken();
			if (tokenGuid == null)
				return null;
            int? userid = await userTokenDAL.Get((Guid)tokenGuid);
			return userid;
        }

		public async Task<bool> IsLoggedIn()
		{
			bool isLoggedIn = await dbSession.IsLoggedIn();
			if (!isLoggedIn)
			{
				int? userid = await GetUserIdByToken();
				if (userid != null)
				{
					await dbSession.SetUserId((int)userid);
					isLoggedIn = true;
                }
            }
			return isLoggedIn;
        }

		public async Task<int?> GetCurrentUserId()
		{
			return await dbSession.GetUserId();
        }

        public bool IsAdmin()
		{
			if (dbSession.GetValueDef(AuthConstants.ADMIN_ROLE_KEY, "").ToString() == AuthConstants.ADMIN_ROLE_ABBR)
				return true;
			return false;
        }

        public async Task Logout()
		{
			await dbSession.DeleteSessionId();

			Guid? tokenGuid = GetCurrentUserToken();
			if (tokenGuid != null){
				await userTokenDAL.DeleteToken((Guid)tokenGuid);
				webCookie.Delete(AuthConstants.RememberMeCookieName);
			}

            webCookie.Delete(AuthConstants.SessionCookieName);
            dbSession.ResetSessionCache();
        }
    }
}

