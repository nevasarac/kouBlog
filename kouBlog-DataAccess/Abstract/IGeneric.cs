namespace kouBlog.kouBlog_DataAccess.Abstract;

public interface IGeneric<T> where T : class
{
    void Insert(T obj);
    void Update(T obj);
    void Delete(T obj);
    T GetByID(int ID);
    List<T> GetList(); 
}