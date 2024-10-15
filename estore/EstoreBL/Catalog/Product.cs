using Estore.BL.Models;
using Estore.DAL;
using Estore.DAL.Models;

namespace Estore.BL.Catalog
{
	public class Product : IProduct
	{
		private readonly IProductDAL productDAL;
        private readonly IProductSearchDAL productSearchDAL;

        public static Dictionary<int, CategoryModel> categoriesCache = new Dictionary<int, CategoryModel>();
        public static Dictionary<int, ProductSerieModel> productSerieCache = new Dictionary<int, ProductSerieModel>(); 

        public Product(IProductDAL productDAL, IProductSearchDAL productSearchDAL)
		{
			this.productDAL = productDAL;
            this.productSearchDAL = productSearchDAL;
        }

        public async Task<IEnumerable<ProductCardModel>> GetNew(int top)
		{
			return await productSearchDAL.Search(new ProductSearchFilter() {
                PageSize = 6,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc
            });
		}

        public async Task<Tuple<int, IEnumerable<ProductCardModel>>> GetBySerie(string serieName, int pagesize, int page)
        {
            var filter = new ProductSearchFilter()
            {
                PageSize = pagesize,
                Offset = (page - 1) * pagesize,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc,
                SerieName = serieName
            };
            var data = await productSearchDAL.Search(filter);
            var cnt = await productSearchDAL.GetCount(filter);
            return new Tuple<int, IEnumerable<ProductCardModel>>(cnt, data);
        }

        public async Task<Tuple<int, IEnumerable<ProductCardModel>>> Search(string searchFor, int pagesize, int page)
        {
            var filter = new ProductSearchFilter()
            {
                PageSize = pagesize,
                Offset = (page - 1) * pagesize,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc,
                SearchFor = "%" + searchFor.Replace(" ", "%") + "%"
            };
            
            var data = await productSearchDAL.Search(filter);
            var cnt = await productSearchDAL.GetCount(filter);
            return new Tuple<int, IEnumerable<ProductCardModel>>(cnt, data);
        }

        public async Task<Tuple<int, IEnumerable<ProductCardModel>>> GetByCategory(int categoryId, int pagesize, int page)
        {
            var filter = new ProductSearchFilter()
            {
                PageSize = pagesize,
                Offset = (page - 1) * pagesize,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc,
                CategoryId = categoryId
            };
            var data = await productSearchDAL.Search(filter);
            var cnt = await productSearchDAL.GetCount(filter);
            return new Tuple<int, IEnumerable<ProductCardModel>>(cnt, data);
        }

        public async Task<int?> GetCategoryId(IEnumerable<string> uniqueids)
        {
            return await productDAL.GetCategoryId(uniqueids);
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoryTree(int categoryid)
        {
            CategoryModel? model = null;
            int? currentCategoryId = categoryid;
            List<CategoryModel> result = new List<CategoryModel>();

            while (currentCategoryId != null)
            {
                if (!categoriesCache.ContainsKey((int)currentCategoryId))
                {
                    model = await productDAL.GetCategory((int)currentCategoryId);
                    if (model == null)
                    {
                        throw new Exception("Категория не найдена");
                    }

                    try
                    {
                        categoriesCache.Add((int)currentCategoryId, model);
                    }
                    catch { }
                }
                else
                    model = categoriesCache[(int)currentCategoryId];

                if (model != null)
                {
                    result.Add(model);
                    currentCategoryId = model?.ParentCategoryId;
                }
            }
            return result;
        }

        public async Task<ProductSerieModel> GetProductSerie(int productSerieId)
        {
            if (!productSerieCache.ContainsKey(productSerieId))
            {
                var series = await productDAL.LoadProductSeries();
                foreach (var serie in series)
                {
                    if (!productSerieCache.ContainsKey(serie.ProductSerieId))
                        productSerieCache.Add(serie.ProductSerieId, serie);
                }
            }

            return productSerieCache[productSerieId];
        }

        public async Task<CompleteProductDataModel> GetProduct(string uniqueid)
        {
            var product = await productDAL.GetProduct(uniqueid);
            var productDetails = await productDAL.GetProductDetails(product.ProductId);
            var author = await productDAL.GetAuthorByProduct(product.ProductId);
            var categories = await GetCategoryTree(product.CategoryId);
            var serie = await GetProductSerie(product.ProductSerieId);

            return new CompleteProductDataModel()
            {
                Product = product,
                ProductDetails = productDetails.ToList(),
                Autors = author.ToList(),
                Categories = categories.ToList(),
                Serie = serie
            };
        }

        public async Task<IEnumerable<CategoryModel>> GetChildCategories(int? categoryid)
        {
            return await productDAL.GetChildCategories(categoryid);
        }

        public async Task<int> AddCategory(int? categoryid, string name)
        {
            return await productDAL.AddCategory(new CategoryModel()
            {
                ParentCategoryId = categoryid,
                CategoryName = name,
                CategoryUniqueId = General.Helpers.Translit(name)
            }); ;
        }
    }
}

