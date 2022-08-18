using Microsoft.EntityFrameworkCore;
using WebAppMvc.Models.Db;

namespace WebAppMvc.Models
{
    public class BlogContext:DbContext
    {
        public DbSet<User>Users { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("User");
        }
    }
}
