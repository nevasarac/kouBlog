using kouBlog.kouBlog_DataAccess.Abstract;
using kouBlog.kouBlog_DataAccess.Concrete;
using kouBlog.kouBlog_DataAccess.Repository;
using kouBlog.kouBlog_Entity.Concrete;

namespace kouBlog.kouBlog_DataAccess.EntityFramework;

public class efComment : GenericRepository<Comment>, IComment
{
    public efComment(Context context) : base(context)
    {
    }
}