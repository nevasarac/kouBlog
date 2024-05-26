using kouBlog.kouBlog_Entity.Concrete;
namespace kouBlog.Models.DTO;

public class ResultPostDto
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public string Header { get; set; }
    public string Content { get; set; }

    public string WriterName { get; set; }
    public int NumberOfLike { get; set; }
    public List<Comment> Comments { get; set; }

    public string NewPostHeader { get; set; }
    public string NewPostContent { get; set; }
}