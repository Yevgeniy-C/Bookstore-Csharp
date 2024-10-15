using System.Text;
using Estore.DAL.Models;

namespace Estore.DAL
{
	public class ProductSearchDAL: IProductSearchDAL
	{
        private readonly IDbHelper dbHelper;
        public ProductSearchDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        static string ProductCardModelSql = @"select p.ProductImage, p.ProductName, p.Price, p.UniqueId from Product p ";
        static string ProductCardCountSql = @"select count(*) cnt from Product p ";

        public async Task<int> GetCount(ProductSearchFilter filter)
        {
            var result = await GetData<int>(ProductCardCountSql, filter, false);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ProductCardModel>> Search(ProductSearchFilter filter)
        {
            return await GetData<ProductCardModel>(ProductCardModelSql, filter, true);
        }

        async Task<IEnumerable<T>> GetData<T>(string basequery, ProductSearchFilter filter, bool order)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("pagesize", filter.PageSize);
            parameters.Add("pageoffset", filter.Offset);

            Dictionary<string, object> joins = new Dictionary<string, object>();

            StringBuilder whereSql = new StringBuilder();
            whereSql.Append(" where 1=1 ");

            if (!String.IsNullOrEmpty(filter.SearchFor)) 
            {
                if (!joins.ContainsKey("ProductAuthor")) {
                    joins.Add("ProductAuthor", 
                        @"
                         join ProductAuthor pa on p.ProductId = pa.ProductId 
                         join Author a on pa.AuthorId = a.AuthorId 
                        ");
                }

                whereSql.Append(" and CONCAT(a.FirstName, ' ', a.LastName, ' ', p.ProductName, ' ', a.LastName) like @searchfor");
                parameters.Add("searchfor", filter.SearchFor);                
            }

            if (filter.AuthorId != null)
            {
                if (!joins.ContainsKey("ProductAuthor"))
                    joins.Add("ProductAuthor", " join ProductAuthor pa on p.ProductId = pa.ProductId ");

                whereSql.Append(" and pa.AuthorId = @authorid ");
                parameters.Add("authorid", filter.AuthorId);
            }

            if (!String.IsNullOrEmpty(filter.SerieName))
            {
                if (!joins.ContainsKey("ProductSerie"))
                    joins.Add("ProductSerie", " join ProductSerie ps on p.ProductSerieId = ps.ProductSerieId ");

                whereSql.Append(" and ps.SerieName = @productSerie ");
                parameters.Add("productSerie", filter.SerieName);
            }

            if (filter.CategoryId != null)
            {
                if (!joins.ContainsKey("Categories"))
                    joins.Add("Categories", @"
                        join Category c1 on p.CategoryId = c1.CategoryId 
                        join Category c2 on c2.CategoryId = c1.ParentCategoryId
                        join Category c3 on c3.CategoryId = c2.ParentCategoryId
                    ");

                whereSql.Append(" and @categoryid  in (c1.CategoryId, c2.CategoryId, c3.CategoryId, c3.ParentCategoryId)  ");
                parameters.Add("categoryid", filter.CategoryId);
            }

            string orderby = "";
            if (order)
                orderby = " order by " + filter.SortBy.ToString() + " " + filter.Direction.ToString() +
                    " limit @pagesize offset @pageoffset";

            return await dbHelper.QueryAsync<T>(
                basequery +
                String.Join(" ", joins.Values) +
                whereSql.ToString() +
                orderby, parameters);
        }
    }
}

