using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppExam.DAL;
using WebAppExam.Models;
using WebAppExam.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<SettingService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
