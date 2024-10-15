using System;
using Estore.DAL.Models;

namespace Estore.BL.Models
{
	public class AuthorDataModel
	{
        public AuthorModel Author { get; set; } = null!;

        public List<ProductCardModel> ProductCards { get; set; } = null!;
    }
}

