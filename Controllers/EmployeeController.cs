 using Microsoft.AspNetCore.Mvc;
using ST10390916_PROG7311_POE.Models;
using ST10390916_PROG7311_POE.Services;

namespace ST10390916_PROG7311_POE.Controllers
{
    public class EmployeeController : Controller
    {
        
        public IActionResult AddFarmer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFarmer(User user)
        {
            if (ModelState.IsValid)
            {
                // Save the farmer to the database
                UserService userService1 = new UserService();
                userService1.AddNewUser(user);

                return RedirectToAction("Marketplace", "Home");
            }
            return View();
        }




    }
}
