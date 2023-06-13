using MeetUp.CommentsService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.CommentsService.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        protected ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasIndex(e => e.Id);
        }
    }
}
