using System;
using System.ComponentModel.DataAnnotations;
using Estore.DAL.Models;

namespace Estore.BL.Auth
{
	public interface IAuth
	{
		Task<int> CreateUser(Estore.DAL.Models.UserModel user);

		Task<int> Authenticate(string email, string password, bool rememberMe);

		Task ValidateEmail(string email);

		Task Register(UserModel user);
    }
}

