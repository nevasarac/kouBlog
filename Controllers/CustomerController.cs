using kouBlog.Models;
using kouBlog.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace kouBlog.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public CustomerController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult PostDetail()
        {
            return View();
        }
    }
}
