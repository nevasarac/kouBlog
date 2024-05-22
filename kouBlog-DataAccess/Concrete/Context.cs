using kouBlog.kouBlog_Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kouBlog.kouBlog_DataAccess.Concrete;

public class Context : IdentityDbContext<AppUser,AppRole,int>
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("");
    }
    
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    
}