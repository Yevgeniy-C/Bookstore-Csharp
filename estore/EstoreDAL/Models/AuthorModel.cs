using System;
using System.Xml;

namespace Estore.DAL.Models
{
	public class AuthorModel
	{
		public int AuthorId { get; set; }
		public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
		public string LastName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? AuthorImage { get; set; }
        public string UniqueId { get; set; } = null!;
    }
}

