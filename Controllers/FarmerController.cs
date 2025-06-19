using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10390916_PROG7311_POE.Data;
using ST10390916_PROG7311_POE.Models;
using ST10390916_PROG7311_POE.Services;

namespace ST10390916_PROG7311_POE.Controllers
{
    public class FarmerController : Controller
    {
        public IActionResult Addproduct()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            return View();
        }

        [HttpPost]
        public IActionResult Addproduct(Product product)
        {            
            if (ModelState.IsValid)
            {
                ProductService productService = new ProductService();

                productService.AddProduct(product);

                return RedirectToAction("FarmerDetails", "Home");
            }

            return View(product);
        }

    }
}
