using System;
using Estore.DAL.Models;

namespace Estore.BL.Auth
{
	public interface ICurrentUser
	{
		Task<bool> IsLoggedIn();

		Task<int?> GetCurrentUserId();

		bool IsAdmin();

		Task Logout();
    }
}

