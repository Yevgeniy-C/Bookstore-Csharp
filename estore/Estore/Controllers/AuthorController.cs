using Estore.BL.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace Estore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthor author;

        public AuthorController(IAuthor author)
        {
            this.author = author;
        }

        [HttpGet]
        [Route("/author/{uniqueid}")]
        public async Task<IActionResult> Index(string uniqueid)
        {
            var model = await this.author.GetAuthor(uniqueid);
            return View(model);
        }
    }
}

