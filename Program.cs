using kouBlog.kouBlog_Business.Abstract;
using kouBlog.kouBlog_Business.Concrete;
using kouBlog.kouBlog_DataAccess.Abstract;
using kouBlog.kouBlog_DataAccess.Concrete;
using kouBlog.kouBlog_DataAccess.EntityFramework;
using kouBlog.kouBlog_Entity.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Home/Login/";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();
builder.Services.AddScoped<IComment, efComment>();
builder.Services.AddScoped<IPost, efPost>();
builder.Services.AddScoped<ICommentService, CommentManager>();
builder.Services.AddScoped<IPostService, PostManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    var context = services.GetRequiredService<Context>();

    //Tanımlanan rollerin veri tabanına eklenmesi
    if (!context.Roles.Any())
    {
        var adminRole = new AppRole { Name = "Admin" };
        var customerRole = new AppRole { Name = "Customer" };

        roleManager.CreateAsync(adminRole).Wait();
        roleManager.CreateAsync(customerRole).Wait();
    }
}


app.Run();
