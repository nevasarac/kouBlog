namespace kouBlog.Models.DTO
{
    public class LoginDto
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
