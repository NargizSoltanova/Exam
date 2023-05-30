using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppExam.Models;

namespace WebAppExam.DAL
{
    public class AppDbContext :IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
