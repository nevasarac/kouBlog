using kouBlog.kouBlog_Business.Abstract;
using kouBlog.kouBlog_DataAccess.Abstract;
using kouBlog.kouBlog_Entity.Concrete;

namespace kouBlog.kouBlog_Business.Concrete;

public class PostManager : IPostService
{
    private readonly IPost _post;

    public PostManager(IPost post) => _post = post;
    
    public void SInsert(Post s)
    {
        _post.Insert(s);
    }

    public void SDelete(Post s)
    {
        _post.Delete(s);
    }

    public void SUpdate(Post s)
    {
        _post.Update(s);
    }

    public Post SGetByID(int ID)
    {
        return _post.GetByID(ID);
    }

    public List<Post> SGetList()
    {
        return _post.GetList();
    }
}