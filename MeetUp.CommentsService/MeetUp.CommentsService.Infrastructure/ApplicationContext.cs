using MeetUp.CommentsService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.CommentsService.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
