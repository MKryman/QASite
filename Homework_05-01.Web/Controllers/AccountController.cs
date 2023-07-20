using Homework_05_01.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Homework_05_01.Web.Controllers
{
    public class AccountController : Controller
    {
        private string _connectionString;

        public AccountController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConStr");
        }

        public IActionResult SignUp()
        {
            if (TempData["InvalidEmail"] != null)
            {
                ViewBag.Message = TempData["InvalidEmail"];
            }
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user, string password)
        {
            var repo = new UserRepository(_connectionString);
            if (!repo.IsEmailAvail(user.Email))
            {
                TempData["InvalidEmail"] = "Invalid Email";
                return Redirect("/account/signup");
            }
            repo.AddUser(user, password);
            return Redirect("/account/login");
        }

        public IActionResult Login()
        {
            if (TempData["InvalidLogin"] != null)
            {
                ViewBag.Message = TempData["InvalidLogin"];
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var repo = new UserRepository(_connectionString);
            var user = repo.Login(email, password);

            if (user == null)
            {
                TempData["InvalidLogin"] = "Invalid Login";
                return Redirect("/account/login");
            }

            var claims = new List<Claim>
            {
                new Claim("user", email)
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

            return Redirect("/");

        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }
    }
}
