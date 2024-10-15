using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Estore.BL.Auth;
using Estore.BL.Catalog;
using Estore.Models;
using Estore.ViewModels;

namespace Estore.Controllers;

public class HomeController : Controller
{
    private readonly ICurrentUser currentUser;
    private readonly IProduct product;

    public HomeController(ICurrentUser currentUser, IProduct product)
    {
        this.currentUser = currentUser;
        this.product = product;
    }

    public async Task<IActionResult> Index()
    {
        var newproducts = await product.GetNew(6);
        return View(
            new HomePageViewModel { NewProducts = newproducts.ToList() }
            );
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

