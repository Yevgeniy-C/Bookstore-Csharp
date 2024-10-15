using System;
namespace Estore.DAL.Models
{
	public class ProductSearchFilter
	{
		public enum SortByColumn { ReleaseDate }

        public enum SortDirection { Asc, Desc }

        public int PageSize { get; set; } = 24;

        public int Offset { get; set; } = 0;

        public int? AuthorId { get; set; }

        public SortByColumn SortBy { get; set; } = ProductSearchFilter.SortByColumn.ReleaseDate;

		public SortDirection Direction = ProductSearchFilter.SortDirection.Desc;

        public int? CategoryId { get; set; } = null;

        public string? SerieName { get; set; } = null;

        public string? SearchFor { get; set; } = null;
    }
}

