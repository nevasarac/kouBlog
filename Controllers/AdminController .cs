using kouBlog.Models;
using kouBlog.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using kouBlog.kouBlog_Business.Abstract;
using kouBlog.kouBlog_Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace kouBlog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(ILogger<HomeController> logger,
            ICommentService commentService,
            IPostService postService,
            UserManager<AppUser> userManager)
        {
            _logger = logger;
            _commentService = commentService;
            _postService = postService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var allPosts = _postService.SGetList();
            return View(allPosts);
        }

        [HttpGet]
        public IActionResult MyPosts()
        {
            var userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var myPosts = _postService.SGetList().Where(x => x.UserID.ToString() == userId).ToList();
            var result = new List<ResultPostDto>();
            var allComments = _commentService.SGetList();

            foreach (var post in myPosts)
            {
                result.Add(new ResultPostDto()
                {
                    UserID = post.UserID,
                    Header = post.Header,
                    Content = post.Content,
                    Comments = allComments.Where(x => x.PostID == post.ID).ToList()
                });
            }

            return View(result);
        }

        [HttpGet]
        public IActionResult NewPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewPost(NewPostDto newPostDto)
        {
            var userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            Post newPost = new Post()
            {
                UserID = Convert.ToInt32(userId),
                Content = newPostDto.PostContent,
                Header = newPostDto.PostName,
                NumberOfLike = 0
            };

            _postService.SInsert(newPost);

            return RedirectToAction("MyPosts", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> PostDetail(int ID)
        {
            var post = _postService.SGetByID(ID);
            var userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            PostDto tempPost = new PostDto()
            {
                PostID = post.ID,
                PostName = post.Header,
                PostContent = post.Content,
                PostNumberofLike = post.NumberOfLike,
                PostWriterID = Convert.ToInt32(userId),
                PostWriterName = user.UserName
            };

            return View(tempPost);
        }

        [HttpGet]
        public IActionResult PostUpdate(int ID)
        {
            var post = _postService.SGetByID(ID);
            var postDto = new PostDto()
            {
                PostID = post.ID,
                PostName = post.Header,
                PostContent = post.Content,
                PostWriterID = post.UserID,
                PostNumberofLike = post.NumberOfLike
            };

            return View(postDto);
        }

        [HttpPost]
        public IActionResult PostUpdate(PostDto postDto)
        {
            var post = _postService.SGetByID(postDto.PostID);
            var updatepost = new Post()
            {
                ID = postDto.PostID,
                UserID = postDto.PostWriterID,
                Header = postDto.PostName,
                Content = postDto.PostContent,
                NumberOfLike = postDto.PostNumberofLike
            };
            _postService.SUpdate(updatepost);

            return RedirectToAction("MyPosts", "Admin");
        }

        [HttpGet]
        public IActionResult DeletePost(int ID)
        {
            var post = _postService.SGetByID(ID);
            _postService.SDelete(post);

            return RedirectToAction("Myposts", "Admin");
        }
    }
}