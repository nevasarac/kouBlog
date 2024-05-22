namespace kouBlog.kouBlog_Entity.Concrete;

public class Post
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public string Header { get; set; }
    public string Content { get; set; }
    public int NumberOfLike { get; set; }
}