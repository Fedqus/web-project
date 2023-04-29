using Microsoft.AspNetCore.Mvc;
using web_project.Models;

namespace web_project.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _env;

        public UserController(IWebHostEnvironment env, DatabaseContext context)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(Request.Cookies["user_id"]))
            {
                return RedirectToAction("Profile");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(IFormCollection collection)
        {
            var isValideEmail = !string.IsNullOrEmpty(collection["email"]);
            if (isValideEmail)
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == collection["email"].ToString());
                if (user?.Password == collection["password"].ToString())
                {
                    Response.Cookies.Append("user_id", user.Id.ToString());
                    return Redirect("/");
                }
            }
            return View();
        }
        public IActionResult SignUp()
        {
            if (!string.IsNullOrEmpty(Request.Cookies["user_id"]))
            {
                return RedirectToAction("Profile");
            }
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(IFormCollection collection)
        {
            var isValideEmail = !string.IsNullOrEmpty(collection["email"].ToString()) 
               && !_context.Users.Any(u => u.Email == collection["email"].ToString());

            if (isValideEmail && collection["password"].ToString() == collection["re-password"].ToString())
            {
                var user = new User()
                {
                    Name = collection["name"].ToString(),
                    LastName = collection["lastname"].ToString(),
                    MiddleName = collection["middlename"].ToString(),
                    Email = collection["email"].ToString(),
                    Password = collection["password"].ToString(),
                    PermissionLevel = 0
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return Redirect("/");
            }

            return View();
        }
        [HttpGet("/User/Profile")]
        public IActionResult Profile()
        {
            if (string.IsNullOrEmpty(Request.Cookies["user_id"]))
            {
                return RedirectToAction("Login");
            }
            var user = _context.Users.FirstOrDefault(u => u.Id == int.Parse(Request.Cookies["user_id"]));
            var isAdmin = user.PermissionLevel == 1;
            return View(new Tuple<User, bool>(user, isAdmin));
        }
        [HttpGet("/User/Profile/{id}")]
        public IActionResult Profile(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            var isAdmin = false;
            if (Request.Cookies["user_id"] != null)
            {
                var loginedUser = _context.Users.FirstOrDefault(u => u.Id == int.Parse(Request.Cookies["user_id"]));
                isAdmin = loginedUser.PermissionLevel == 1;
            }

            return View(new Tuple<User, bool>(user, isAdmin));
        }
        public IActionResult ChangePassword()
        {
            if (string.IsNullOrEmpty(Request.Cookies["user_id"]))
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(IFormCollection collection)
        {
            var loginedUser = _context.Users.FirstOrDefault(u => u.Id == int.Parse(Request.Cookies["user_id"]));
            if (loginedUser.Password == collection["old-password"].ToString())
            {
                loginedUser.Password = collection["new-password"].ToString();
                _context.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("user_id");
            return Redirect("/user/login");
        }
        public IActionResult AddPassport(int? userId)
        {
            return View(userId);
        }
        [HttpPost]
        public async Task<IActionResult> AddPassport(int? userId, IFormCollection collection)
        {
            if (collection.Files.First() != null && collection.Files.First().Length > 0 && userId != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(collection.Files.First().FileName);
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await collection.Files.First().CopyToAsync(fileStream);
                }
                var passport = new Passport()
                {
                    UserId = (int)userId,
                    Birthdate = DateTime.Parse(collection["birthdate"].ToString()),
                    Nationality = collection["nationality"].ToString(),
                    Gender = bool.Parse(collection["gender"].ToString()),
                    ImagePath = fileName
                };
                _context.Passports.Add(passport);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult DeletePassport(int? id)
        {
            var passport = _context.Passports.Find(id);
            if (passport != null)
            {
                _context.Passports.Remove(passport);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddStudentCard(int? userId)
        {
            return View(userId);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudentCard(int? userId, IFormCollection collection)
        {
            if (collection.Files.First() != null && collection.Files.First().Length > 0 && userId != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(collection.Files.First().FileName);
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await collection.Files.First().CopyToAsync(fileStream);
                }
                var studentCard = new StudentCard()
                {
                    UserId = (int)userId,
                    IssueDate = DateTime.Parse(collection["issueDate"].ToString()),
                    ExpireDate = DateTime.Parse(collection["expireDate"].ToString()),
                    EduForm = collection["eduForm"].ToString(),
                    EduInstitution = collection["eduInstitution"].ToString(),
                    ImagePath = fileName
                };
                _context.StudentCards.Add(studentCard);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult DeleteStudentCard(int? id)
        {
            var studentCard = _context.StudentCards.Find(id);
            if (studentCard != null)
            {
                _context.StudentCards.Remove(studentCard);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddDrivingLicence(int? userId)
        {
            return View(userId);
        }
        [HttpPost]
        public async Task<IActionResult> AddDrivingLicence(int? userId, IFormCollection collection)
        {
            if (collection.Files.First() != null && collection.Files.First().Length > 0 && userId != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(collection.Files.First().FileName);
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await collection.Files.First().CopyToAsync(fileStream);
                }
                var drivingLicence = new DrivingLicence()
                {
                    UserId = (int)userId,
                    IssueDate = DateTime.Parse(collection["issueDate"].ToString()),
                    ExpireDate = DateTime.Parse(collection["expireDate"].ToString()),
                    Categories = collection["categories"].ToString(),
                    ImagePath = fileName
                };
                _context.DrivingLicences.Add(drivingLicence);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult DeleteDrivingLicence(int? id)
        {
            var drivingLicence = _context.DrivingLicences.Find(id);
            if (drivingLicence != null)
            {
                _context.DrivingLicences.Remove(drivingLicence);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
