using BackgroundJobs.Models;
using Microsoft.EntityFrameworkCore;

namespace BackgroundJobs
{
    public class AppDbContext : DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
         }  
        public DbSet<Email> Emails { get; set; }
    }
}
