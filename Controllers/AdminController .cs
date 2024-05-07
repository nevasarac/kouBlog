using kouBlog.Models;
using kouBlog.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace kouBlog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AdminController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyPosts()
        {
            return View();
        }

        public IActionResult NewPost()
        {
            return View();
        }

        public IActionResult PostDetail()
        {
            return View();
        }

        public IActionResult PostUpdate()
        {
            return View();
        }
    }
}
