namespace kouBlog.kouBlog_Entity.Concrete;

public class Comment
{
    public int ID { get; set; }
    public int UserID { get; set; }
    
    public string UserName { get; set; }
    public int PostID { get; set; }
    public string Content { get; set; }
    public int NumberOfLike { get; set; }
}