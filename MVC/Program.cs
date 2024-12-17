using BLL.DAL;
using BLL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Users/Login";
        config.AccessDeniedPath = "/Users/Login";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        config.SlidingExpiration = true;
    });

builder.Services.AddDbContext<DB>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnlineBlogsDB;Trusted_Connection=True;"));

builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITagService, TagService>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
