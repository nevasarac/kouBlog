using kouBlog.Models;
using kouBlog.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using kouBlog.kouBlog_Business.Abstract;
using kouBlog.kouBlog_Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace kouBlog.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;

        public CustomerController(ILogger<HomeController> logger, IPostService postService, ICommentService commentService, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _postService = postService;
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allPosts = _postService.SGetList();

            return View(allPosts);
        }

        [HttpGet]
        public async Task<IActionResult> PostDetail(int id)
        {
            var userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            var post = _postService.SGetByID(id);
            var comments = _commentService.SGetList().Where(x => x.PostID == post.ID).ToList();
            var postDto = new ResultPostDto()
            {
                ID = post.ID,
                Content = post.Content,
                Header = post.Header,
                NumberOfLike = post.NumberOfLike,
                WriterName = user.UserName,
                Comments = comments.Where(x => x.PostID == post.ID).ToList()
            };

            return View(postDto);
        }

        public IActionResult LikePost(int id)
        {
            var post = _postService.SGetByID(id);
            post.NumberOfLike++;
            _postService.SUpdate(post);

            return RedirectToAction("Index", "Customer");
        }
        
        public IActionResult LikeComment(int id)
        {
            var comment = _commentService.SGetByID(id);
            comment.NumberOfLike++;
            _commentService.SUpdate(comment);
            var postID = comment.PostID;

            return RedirectToAction("Index", "Customer");
        }

        public async Task<IActionResult> CommenttoPost(ResultPostDto resultPostDto)
        {
            var userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            var newComment = new Comment()
            {
                PostID = resultPostDto.ID,
                Content = resultPostDto.Content,
                UserID = Convert.ToInt32(userId),
                UserName = user.UserName,
                NumberOfLike = 0
            };
            _commentService.SInsert(newComment);

            return RedirectToAction("Index", "Customer");
        }
    }
}