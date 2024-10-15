namespace Estore.ViewModels
{
	public class PaginationViewModel
	{
        public int Page { get; set; } = 1;

        public int TotalPages { get; set; } = 1;

        public int TotalCount { get; set; } = 0;

        public int PageSize { get; set; } = 24;

        public string BaseUrl { get; set; } = null!;

        public string AdditionalParameters { get; set; } = "";
    }
}

