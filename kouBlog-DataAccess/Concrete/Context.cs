using kouBlog.kouBlog_Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kouBlog.kouBlog_DataAccess.Concrete;

public class Context : IdentityDbContext<AppUser,AppRole,int>
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:demodbsrv.database.windows.net,1433;Initial Catalog=demodb;Persist Security Info=False;User ID=demodb;Password=Furkan..123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
    
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    
}