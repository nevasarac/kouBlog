using kouBlog.kouBlog_Business.Abstract;
using kouBlog.kouBlog_DataAccess.Abstract;
using kouBlog.kouBlog_Entity.Concrete;

namespace kouBlog.kouBlog_Business.Concrete;

public class CommentManager : ICommentService
{
    private readonly IComment _comment;

    public CommentManager(IComment comment) => _comment = comment;
    
    public void SInsert(Comment s)
    {
        _comment.Insert(s);
    }

    public void SDelete(Comment s)
    {
        _comment.Delete(s);
    }

    public void SUpdate(Comment s)
    {
        _comment.Update(s);
    }

    public Comment SGetByID(int ID)
    {
        return _comment.GetByID(ID);
    }

    public List<Comment> SGetList()
    {
        return _comment.GetList();
    }
}