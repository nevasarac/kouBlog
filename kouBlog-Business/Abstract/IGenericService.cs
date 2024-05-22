namespace kouBlog.kouBlog_Business.Abstract;

public interface IGenericService<T> where T : class
{
    void SInsert(T s);
    void SDelete(T s);
    void SUpdate(T s);
    T SGetByID(int ID);
    List<T> SGetList();
}