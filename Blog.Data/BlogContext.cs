using System.Data.Entity;
using Blog.Domain;

namespace Blog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext()
            : base("BlogContext")
        {
            
        }
        public DbSet<Blogs> Blogs { get; set; }
    }
}
