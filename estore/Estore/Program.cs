using Estore.DAL;
using Estore.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
    InitDevDI(builder);
else 
    InitProdDI(builder);

builder.Services.AddMvc();

builder.Services.AddDataProtection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Estore.DAL.DbHelper.ConnString = app.Configuration["ConnectionStrings:Default"] ?? "";

app.UseMiddleware<PerformanceMetrics>();

app.Run();

static void InitDevDI(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<Estore.DAL.IDbSessionDAL, Estore.DAL.DbSessionDAL>();
    builder.Services.AddScoped<Estore.DAL.IAuthDAL, Estore.DAL.AuthDAL>();
    builder.Services.AddScoped<Estore.DAL.IUserTokenDAL, Estore.DAL.UserTokenDAL>();
    builder.Services.AddScoped<Estore.DAL.IProductDAL, Estore.DAL.ProductDAL>();
    builder.Services.AddScoped<Estore.DAL.IAuthorDAL, Estore.DAL.AuthorDAL>();
    builder.Services.AddScoped<Estore.DAL.IProductSearchDAL, Estore.DAL.ProductSearchDAL>();
    builder.Services.AddScoped<Estore.DAL.ICartDAL, Estore.DAL.CartDAL>();
    builder.Services.AddScoped<Estore.DAL.IDbHelper, Estore.DAL.DbHelper>();
    builder.Services.AddScoped<Estore.DAL.IMetricDAL, Estore.DAL.MetricDAL>();
    builder.Services.AddScoped<Estore.DAL.IAddressDAL, Estore.DAL.AddressDAL>();
    builder.Services.AddScoped<Estore.DAL.IBillingDAL, Estore.DAL.BillingDAL>();
    builder.Services.AddScoped<Estore.DAL.IOrderDAL, Estore.DAL.OrderDAL>();

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    builder.Services.AddScoped<Estore.BL.Auth.IAuth, Estore.BL.Auth.Auth>();
    builder.Services.AddScoped<Estore.BL.Auth.IEncrypt, Estore.Deps.Encrypt>();
    builder.Services.AddScoped<Estore.BL.Auth.ICurrentUser, Estore.BL.Auth.CurrentUser>();
    builder.Services.AddScoped<Estore.BL.Auth.IDbSession, Estore.BL.Auth.DbSession>();
    builder.Services.AddScoped<Estore.BL.General.IWebCookie, Estore.Deps.WebCookie>();
    builder.Services.AddScoped<Estore.BL.Catalog.IProduct, Estore.BL.Catalog.Product>();
    builder.Services.AddScoped<Estore.BL.Catalog.IAuthor, Estore.BL.Catalog.Author>();
    builder.Services.AddScoped<Estore.BL.Catalog.ICart, Estore.BL.Catalog.Cart>();
    builder.Services.AddScoped<Estore.BL.Profile.IAddress, Estore.BL.Profile.Address>();
    builder.Services.AddScoped<Estore.BL.Profile.IBilling, Estore.BL.Profile.Billing>();
}

static void InitProdDI(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<Estore.DAL.IDbSessionDAL, Estore.DAL.DbSessionDAL>();
    builder.Services.AddSingleton<Estore.DAL.IAuthDAL, Estore.DAL.AuthDAL>();
    builder.Services.AddSingleton<Estore.DAL.IUserTokenDAL, Estore.DAL.UserTokenDAL>();
    builder.Services.AddSingleton<Estore.DAL.IProductDAL, Estore.DAL.ProductDAL>();
    builder.Services.AddSingleton<Estore.DAL.IAuthorDAL, Estore.DAL.AuthorDAL>();
    builder.Services.AddSingleton<Estore.DAL.IProductSearchDAL, Estore.DAL.ProductSearchDAL>();
    builder.Services.AddSingleton<Estore.DAL.ICartDAL, Estore.DAL.CartDAL>();
    builder.Services.AddSingleton<Estore.DAL.IDbHelper, Estore.DAL.DbHelper>();
    builder.Services.AddSingleton<Estore.DAL.IAddressDAL, Estore.DAL.AddressDAL>();
    builder.Services.AddSingleton<Estore.DAL.IBillingDAL, Estore.DAL.BillingDAL>();
    builder.Services.AddSingleton<Estore.DAL.IOrderDAL, Estore.DAL.OrderDAL>();
    builder.Services.AddScoped<Estore.DAL.IMetricDAL, Estore.DAL.ProdMetricDAL>();

    builder.Services.AddScoped<Estore.BL.Auth.IAuth, Estore.BL.Auth.Auth>();
    builder.Services.AddSingleton<Estore.BL.Auth.IEncrypt, Estore.Deps.Encrypt>();
    builder.Services.AddScoped<Estore.BL.Auth.ICurrentUser, Estore.BL.Auth.CurrentUser>();
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped<Estore.BL.Auth.IDbSession, Estore.BL.Auth.DbSession>();
    builder.Services.AddScoped<Estore.BL.General.IWebCookie, Estore.Deps.WebCookie>();
    builder.Services.AddSingleton<Estore.BL.Catalog.IProduct, Estore.BL.Catalog.Product>();
    builder.Services.AddSingleton<Estore.BL.Catalog.IAuthor, Estore.BL.Catalog.Author>();
    builder.Services.AddScoped<Estore.BL.Catalog.ICart, Estore.BL.Catalog.Cart>();
    builder.Services.AddSingleton<Estore.BL.Profile.IAddress, Estore.BL.Profile.Address>();
    builder.Services.AddSingleton<Estore.BL.Profile.IBilling, Estore.BL.Profile.Billing>();
}