using kouBlog.Models.DTO;
using Microsoft.AspNetCore.Mvc;
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
                    ID = post.ID,
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

        [HttpGet]
        public IActionResult PostUpdate(int id)
        {
            var post = _postService.SGetByID(id);
            var postDto = new PostDto()
            {
                PostID = post.ID,
                PostName = post.Header,
                PostContent = post.Content,
                PostWriternumber = post.UserID,
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
                UserID = postDto.PostWriternumber,
                Header = postDto.PostName,
                Content = postDto.PostContent,
                NumberOfLike = postDto.PostNumberofLike
            };
            _postService.SUpdate(updatepost);

            return RedirectToAction("MyPosts", "Admin");
        }

        [HttpGet]
        public IActionResult DeletePost(int id)
        {
            var post = _postService.SGetByID(id);
            _postService.SDelete(post);

            return RedirectToAction("Myposts", "Admin");
        }
        
        public IActionResult LikePostDetail(int id)
        {
            var post = _postService.SGetByID(id);
            post.NumberOfLike++;
            _postService.SUpdate(post);

            return RedirectToAction("Index", "Admin");
        }
        
        public IActionResult LikePostMyPosts(int id)
        {
            var post = _postService.SGetByID(id);
            post.NumberOfLike++;
            _postService.SUpdate(post);

            return RedirectToAction("Index", "Admin");
        }
        
        public IActionResult LikePost(int id)
        {
            var post = _postService.SGetByID(id);
            post.NumberOfLike++;
            _postService.SUpdate(post);

            return RedirectToAction("Index", "Admin");
        }
        
        public IActionResult LikeComment(int id)
        {
            var comment = _commentService.SGetByID(id);
            comment.NumberOfLike++;
            _commentService.SUpdate(comment);
            var postID = comment.PostID;

            return RedirectToAction("Index", "Admin");
        }
        
        public async Task<IActionResult> CommenttoPost(ResultPostDto resultPostDto)
        {
            var userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            var newComment = new Comment()
            {
                PostID = resultPostDto.ID,
                Content = resultPostDto.NewPostContent,
                UserID = Convert.ToInt32(userId),
                UserName = "(ADMİN) " + user.UserName,
                NumberOfLike = 0,
            };
            _commentService.SInsert(newComment);

            var redirectUrl = "/Admin/PostDetail/" + resultPostDto.ID;

            return Json(new { redirectUrl });
        }
    }
}