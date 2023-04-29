using Microsoft.AspNetCore.Mvc;
using web_project.Models;

namespace web_project.Controllers
{
    public class NewsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _env;

        public NewsController(IWebHostEnvironment env, DatabaseContext context)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var news = _context.News.ToList();
            var isAdmin = false;
            if (Request.Cookies["user_id"] != null)
            {
                var loginedUser = _context.Users.FirstOrDefault(u => u.Id == int.Parse(Request.Cookies["user_id"]));
                isAdmin = loginedUser.PermissionLevel == 1;
            }

            return View(new Tuple<List<News>, bool>(news, isAdmin));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            if (collection.Files.First() != null && collection.Files.First().Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(collection.Files.First().FileName);
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await collection.Files.First().CopyToAsync(fileStream);
                }
                var news = new News()
                {
                    Title = collection["title"].ToString(),
                    Content = collection["text"].ToString(),
                    PostDate = DateTime.Parse(collection["date"].ToString()),
                    ImagePath = fileName
                };
                _context.News.Add(news);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
