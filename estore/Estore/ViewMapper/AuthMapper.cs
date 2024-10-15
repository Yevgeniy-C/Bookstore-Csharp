using Estore.ViewModels;
using Estore.DAL.Models;

namespace Estore.ViewMapper
{
	public static class AuthMapper
	{
		public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model)
		{
            return new UserModel()
            {
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }
}

