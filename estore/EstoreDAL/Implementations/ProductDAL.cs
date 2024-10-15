using System.Text;
using Estore.DAL.Models;

namespace Estore.DAL
{
	public class ProductDAL: IProductDAL
    {
        private readonly IDbHelper dbHelper;
        public ProductDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task<ProductModel> GetProduct(string uniqueid)
        {
            return await dbHelper.QueryScalarAsync<ProductModel>(
                @"select ProductId, CategoryId, ProductName, Price, Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId
                from Product
                where uniqueid = @id", new { id = uniqueid });
        }

        public async Task<IEnumerable<ProductDetailModel>> GetProductDetails(int productid)
        {
            return await dbHelper.QueryAsync<ProductDetailModel>(
                @"select ParamName, StringValue
                from ProductDetail
                where ProductId = @id", new { id = productid });
        }

        public async Task<IEnumerable<AuthorModel>> GetAuthorByProduct(int productid)
        {
            return await dbHelper.QueryAsync<AuthorModel>(
                @"select a.AuthorId, a.FirstName, a.MiddleName, a.Description, a.LastName, a.AuthorImage, a.UniqueId
                from Author a
                    join ProductAuthor pa on a.AuthorId = pa.AuthorId
                where pa.ProductId = @productId", new { productid = productid });
        }

        public async Task<CategoryModel> GetCategory(int categoryid)
        {
            return await dbHelper.QueryScalarAsync<CategoryModel>(
                @"select c.CategoryId, c.ParentCategoryId, c.CategoryName, c.CategoryUniqueId
                    from Category c
                    where c.CategoryId = @categoryid", new { categoryid = categoryid });
        }

        public async Task<IEnumerable<CategoryModel>> GetChildCategories(int? categoryid)
        {
            string sql = "select c.CategoryId, c.ParentCategoryId, c.CategoryName, c.CategoryUniqueId from Category c ";
            if (categoryid == null)
                return await dbHelper.QueryAsync<CategoryModel>(sql + " where c.ParentCategoryId is null", new { });
            else 
                return await dbHelper.QueryAsync<CategoryModel>(sql + " where c.ParentCategoryId = @categoryid", new { categoryid = categoryid });
        }

        public async Task<int?> GetCategoryId(IEnumerable<string> uniqueids)
        {
            if (!uniqueids.Any())
                return null;

            StringBuilder sb = new StringBuilder();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            int index = 0;
            foreach (string uniqueid in uniqueids)
            {
                if (index == 0)
                    sb.Append($" from Category c{index} ");
                else 
                    sb.Append($" inner join Category c{index} on c{index-1}.CategoryID = c{index}.ParentCategoryId and c{index}.CategoryUniqueId = @u{index} ");

                parameters.Add($"u{index}", uniqueid);
                index++;
            }

            return await dbHelper.QueryScalarAsync<int>($"select c{index - 1}.CategoryId " + sb.ToString() + " where c0.CategoryUniqueId = @u0", parameters);
        }

        public async Task<int> AddCategory(CategoryModel model)
        {
            string sql = @"insert into Category(ParentCategoryId, CategoryName, CategoryUniqueId)
                    values(@ParentCategoryId, @CategoryName, @CategoryUniqueId) returning CategoryId";
            return await dbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<ProductSerieModel>> LoadProductSeries()
        {
            return await dbHelper.QueryAsync<ProductSerieModel>(
                @"select ProductSerieId, SerieName
                    from ProductSerie", new { });
        }
    }
}

