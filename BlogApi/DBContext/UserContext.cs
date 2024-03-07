using Microsoft.EntityFrameworkCore;
using Post.Models;
using Users.Models;
using Category.Models;
using Comment.Models;

namespace Users.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<CommentModel> Comment { get; set; }
    }
}