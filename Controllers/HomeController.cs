using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10390916_PROG7311_POE.Models;
using ST10390916_PROG7311_POE.Services;
using System.Diagnostics;

namespace ST10390916_PROG7311_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult _Layout()
        {
            HttpContext.Session.SetInt32("UserID", -1);
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Marketplace(string category, DateTime? productionDate)
        {
            UserService userService = new UserService();
            ProductService productService = new ProductService();

            var products = productService.GetAllProducts();
            var farmers = userService.GetUsers();

            // Extract distinct, sorted list of categories
            var categories = products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            // Filter by category if provided and valid
            if (!string.IsNullOrEmpty(category) && Enum.TryParse<ProductCategory>(category, true, out var parsedCategory))
            {
                products = products.Where(p => p.Category == parsedCategory).ToList();
            }

            if (productionDate.HasValue)
            {
                var filterDate = DateOnly.FromDateTime(productionDate.Value);
                products = products.Where(p => p.ProductionDate == filterDate).ToList();
            }

            // Pass data to view
            ViewData["Farmers"] = farmers;
            ViewData["UserService"] = userService;
            ViewData["Categories"] = categories;
            ViewBag.Category = category;
            ViewBag.ProductionDate = productionDate?.ToString("yyyy-MM-dd");

            return View(products);
        }


        public IActionResult FarmerDetails(int UserID, string category, DateTime? productionDate)
        {
            int sessionUserId = HttpContext.Session.GetInt32("UserID") ?? 0;
            bool isOwner = sessionUserId == UserID || UserID == 0;

            var userIdToUse = isOwner ? sessionUserId : UserID;

            var userService = new UserService();
            var productService = new ProductService();

            var user = userService.GetUser(userIdToUse);
            var products = productService.GetProductsByUserID(userIdToUse);

            // Apply category filter
            if (!string.IsNullOrEmpty(category))
            {
                if (Enum.TryParse(typeof(ProductCategory), category, out var parsedCategory))
                {
                    products = products
                        .Where(p => p.Category == (ProductCategory)parsedCategory)
                        .ToList();
                }
            }

            // Apply production date filter
            if (productionDate.HasValue)
            {
                var filterDate = DateOnly.FromDateTime(productionDate.Value);
                products = products
                    .Where(p => p.ProductionDate == filterDate)
                    .ToList();
            }

            ViewData["User"] = user;
            ViewData["Products"] = products;
            ViewData["CanEdit"] = isOwner;

            return View();
        }


        public IActionResult Login()
        {
            HttpContext.Session.SetInt32("UserID", -1);
            HttpContext.Session.SetString("UserRole", UserRole.Customer.ToString());
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var userService = new UserService();
            var user = new User();
            int userID = userService.CheckUser(email, password);

            if (userID != -1)
            {
                user = userService.GetUser(userID);
                HttpContext.Session.SetInt32("UserID", userID);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());
                return RedirectToAction("Marketplace", "Home");
            }
            else
            {
                ViewData["LoginFailed"] = "Email or password is incorrect.";
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
