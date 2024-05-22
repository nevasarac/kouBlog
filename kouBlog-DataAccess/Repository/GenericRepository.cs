using kouBlog.kouBlog_DataAccess.Abstract;
using kouBlog.kouBlog_DataAccess.Concrete;

namespace kouBlog.kouBlog_DataAccess.Repository;

public class GenericRepository<T> : IGeneric<T> where T : class
{
    private readonly Context _context;

    public GenericRepository(Context context) => _context = context; 
    
    public void Insert(T obj)
    {
        _context.Set<T>().Add(obj);
        _context.SaveChanges();
    }

    public void Update(T obj)
    {
        _context.Set<T>().Update(obj);
        _context.SaveChanges();
    }

    public void Delete(T obj)
    {
        _context.Set<T>().Remove(obj);
        _context.SaveChanges();
    }

    public T GetByID(int ID)
    {
        return _context.Set<T>().Find(ID);
    }

    public List<T> GetList()
    {
        return _context.Set<T>().ToList();
    }
}