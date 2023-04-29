using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using web_project.Models;

namespace web_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
            if (Request?.Cookies["user_id"] != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == int.Parse(Request.Cookies["user_id"]));
                ViewData["isAdmin"] = user != null && user.PermissionLevel == 1;
            }
            else
            {
                ViewData["isAdmin"] = false;
            }
        }

        public IActionResult Index()
        {
            return View();
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
}