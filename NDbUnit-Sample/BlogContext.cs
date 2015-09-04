using System.Data.Entity;

namespace NDbUnit_Sample
{
        public class BlogContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }
        }

        public class Blog
        {
            public int BlogId { get; set; }
            public string Name { get; set; }
        }
}
