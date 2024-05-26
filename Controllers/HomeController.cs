using kouBlog.Models;
using kouBlog.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using kouBlog.kouBlog_Entity.Concrete;
using Microsoft.AspNetCore.Identity;

namespace kouBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var controlUser = await _userManager.FindByEmailAsync(loginDto.Mail);

            if (controlUser != null)
            {
                var result = await _signInManager.PasswordSignInAsync(controlUser, loginDto.Password, loginDto.RememberMe, true);

                if (result.Succeeded)
                {
                    if (await _userManager.IsInRoleAsync(controlUser, "Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Customer");
                    }
                }
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var controlUser = await _userManager.FindByEmailAsync(registerDto.Mail);

            if (controlUser != null)
            {
                return View();
            }

            AppUser newUser = new AppUser()
            {
                UserName = registerDto.Name + " " + registerDto.Surname,
                Email = registerDto.Mail,
            };

            var passwordHasher = new PasswordHasher<AppUser>();
            newUser.PasswordHash = passwordHasher.HashPassword(newUser, registerDto.Password);

            var result = await _userManager.CreateAsync(newUser);

            if (result.Succeeded)
            {
                var adminControl = await _userManager.GetUsersInRoleAsync("Admin");

                if (adminControl.Count == 0)
                {
                    await _userManager.AddToRoleAsync(newUser, "Admin");
                }
                else
                {
                    await _userManager.AddToRoleAsync(newUser, "Customer");
                }
            }

            return RedirectToAction("Login", "Home");
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

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Home");
        }
    }
}