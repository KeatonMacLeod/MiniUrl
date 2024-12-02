using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using MiniUrl.Database;
using MiniUrl.Logic;
using MiniUrl.Models;
using MiniUrl.Validator;

using System.Diagnostics;

namespace MiniUrl.Controllers
{
    public class HomeController : Controller
    {
        private readonly MiniUrlDbContext databaseContext;

        IUrlHasher UrlHasher { get; set; }

        UrlValidator UrlValidator { get; set; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MiniUrlDbContext databaseContext)
        {
            this._logger = logger;

            this.UrlHasher = new GuidHasher();
            this.UrlValidator = new UrlValidator();
            this.databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShrinkUrl(string url)
        {
            string hashedUrl = string.Empty;

            if (!UrlValidator.ValidateUrl(url))
            {
                hashedUrl = "Invalid URL.";
            }
            else
            {
                hashedUrl = UrlHasher.Hash(url);
                var urlMapping = new UrlMapping
                {
                    HashedUrl = hashedUrl,
                    OriginalUrl = url
                };

                databaseContext.UrlMappings.Add(urlMapping);
                databaseContext.SaveChanges();
            }

            ViewBag.Result = string.Format("{0}{1}", "http://localhost:5297/", hashedUrl);
            return View(Constants.INDEX_PAGE_NAME);
        }

        [HttpGet]
        public IActionResult RedirectToOriginal(string hashedUrl)
        {
            var urlMapping = databaseContext.UrlMappings.FirstOrDefault(u => u.HashedUrl == hashedUrl);

            if (urlMapping == null)
            {
                return NotFound();
            }

            return Redirect(urlMapping.OriginalUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
